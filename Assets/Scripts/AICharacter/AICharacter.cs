using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{

    Idle,
    Follow,
    Chase,
    Attack

}
[Serializable]

public enum CombatType
{
    Empty,
    Fighter,
    Ranger,
    Mage,
    Enchanter,
    Thief,
    Tank,
    Healer

}

public class AICharacter : Character
{

    Player player;

    public AIState state = AIState.Chase;

    [SerializeField] public LayerMask allyLayerMask;

    [SerializeField] public LayerMask targetLayerMask;


    Seeker seeker;
    public Transform target;
    private float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    [SerializeField] GameObject meleeAttack;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject areaAttack;
    [SerializeField] GameObject heal;
    [SerializeField] GameObject enchant;
    [SerializeField] GameObject stun;
    [SerializeField] GameObject shield;





    //Attack
    private float fireTimer = 5;

    //Backing Up
    private bool canBackup = true;
    private float backUpTimer = 0f;
    private float backUpTimeLimit = 1.0f;

    //Stun
    bool isStunned = false;
    float stunTimer = 0;

    //Shield
    float shieldTimer = 0;

    //Enchant
    bool enchantIsActive = false;

    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();

        target = FindObjectOfType<Player>().transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);

        player = FindObjectOfType<Player>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        fireTimer -= Time.deltaTime;




        if (!isStunned)
        {
            HandleState();

        }
        StunTimer();
        ShieldTimer();
    }

    private void HandleState()
    {

        switch (state)
        {
            case AIState.Idle:
                Detect();

                break;
            case AIState.Follow:
                Detect();
                Follow();

                break;
            case AIState.Chase:
                Detect();
                Chase();

                break;
            case AIState.Attack:
                Strafe();
                Attack();

                break;
        }



    }

    public void Follow()
    {

       

        Move();

        if (Vector2.Distance(target.position, (Vector2)transform.position) <= statModule.detectRange )
        {
            if (target != player.transform)
            {
                state = AIState.Chase;

            }

        }

    }


    public void Chase()
    {



        Move();

        if (Vector2.Distance(target.position, (Vector2)transform.position) <= statModule.attackRange)
        {

            state = AIState.Attack;

        }


    }


    public void Attack()
    {


        if (target != null)
        {


            if (fireTimer <= 0)
            {
                Player targetCheck = target.GetComponent<Player>();
                if (this is Summon && targetCheck != null)
                {

                }
                else
                {
                    switch (statModule.combatType)
                    {
                        case CombatType.Empty:
                            break;
                        case CombatType.Fighter:



                            GameObject newMelee = Instantiate(meleeAttack, target.transform.position, Quaternion.identity);
                            newMelee.GetComponent<MeleeAttack>().SetOwner(this);



                            break;
                        case CombatType.Ranger:
                            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                            newProjectile.GetComponent<Projectile>().SetOwner(this);
                            newProjectile.GetComponent<Projectile>().Shoot(target);


                            break;
                        case CombatType.Mage:
                            GameObject newAreaAttack = Instantiate(areaAttack, transform.position, Quaternion.identity);
                            newAreaAttack.GetComponent<AreaAttack>().SetOwner(this);


                            break;
                        case CombatType.Enchanter:
                            Chase();

                            if (!enchantIsActive)
                            {
                                GameObject newEnchant = Instantiate(enchant, transform.position, Quaternion.identity);
                                newEnchant.GetComponent<Enchant>().SetOwner(this);
                                newEnchant.transform.parent = transform;
                                enchantIsActive = true;
                            }
 


                            break;
                        case CombatType.Thief:
                            GameObject newStun = Instantiate(stun, target.transform.position, Quaternion.identity);
                            newStun.GetComponent<Stun>().SetOwner(this);
                            break;
                        case CombatType.Tank:
                            GameObject newShield = Instantiate(shield, target.transform.position, Quaternion.identity);
                            newShield.GetComponent<Shield>().SetOwner(this);
                            break;
                        case CombatType.Healer:

                            if (target != null)
                            {
                                GameObject newHeal = Instantiate(heal, target.transform.position, Quaternion.identity);
                                newHeal.GetComponent<Heal>().SetOwner(this);
                            }
                            else
                            {
                                Detect();
                            }

                            break;
                        default:
                            break;
                    }
                    fireTimer = statModule.fireInterval;

                }





            }
            else
            {

                fireTimer -= Time.deltaTime;

            }


            CheckDistance();


        }
        else
        {

            Detect();

        }

    }




    public void CheckDistance()
    {

        float distanceToTarget = Vector2.Distance(target.position, transform.position);

        float minimumDistance = statModule.attackRange - 1f;


        // Check if the target is too far
        if (distanceToTarget > statModule.attackRange)
        {
            state = AIState.Chase;
        }
        // Check if the target is too close
        else if (distanceToTarget < minimumDistance)
        {

            if (canBackup)
            {
                Backup();
                backUpTimer += Time.deltaTime;
                if (backUpTimer >= backUpTimeLimit)
                {
                    canBackup = false;
                }
            }
            else 
            { 
            
                backUpTimer -= Time.deltaTime;
                if (backUpTimer <= 0)
                {
                    canBackup = true;
                }

            }
        }
        else
        {
            state = AIState.Attack; // Maintain position or another state if needed
        }

    }

    private void Backup()
    {

        Vector2 directionToTarget = (target.position - transform.position).normalized;
        Vector2 backupDirection = -directionToTarget; // Move away from the target
        Vector2 force = backupDirection * statModule.moveSpeed * Time.deltaTime;

        rb.AddForce(force);


    }
    

    public void Detect()
    {
        LayerMask activeLayerMask = 0;



        switch (statModule.combatType)
        {
            case CombatType.Empty:
                break;
            case CombatType.Fighter:
                activeLayerMask = targetLayerMask;

                break;
            case CombatType.Ranger:
                activeLayerMask = targetLayerMask;

                break;
            case CombatType.Mage:
                activeLayerMask = targetLayerMask;

                break;
            case CombatType.Enchanter:
                activeLayerMask = allyLayerMask;

                break;
            case CombatType.Thief:
                activeLayerMask = targetLayerMask;

                break;
            case CombatType.Tank:
                activeLayerMask = allyLayerMask;

                break;
            case CombatType.Healer:
                activeLayerMask = allyLayerMask;

                break;
            default:
                break;
        }


        RaycastHit2D hit = Physics2D.CircleCast(transform.position, statModule.detectRange, new Vector2(0, 0), 0, activeLayerMask);


        //If Enemy find player/ Overide Follow/ Chase Function

        if (hit)
        {

            if (target == null || target == player.transform)
            {

                if (hit.transform != transform)
                {
                    target = hit.transform;

                }

            }


        }
        else
        {
            if (target == null)
            {
                target = player.transform;
                state = AIState.Follow;
            }
        
        }


    }


    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (target != null)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);

            }
        }

    }

    private void OnPathComplete(Path p)
    {


        if (!p.error) 
        {
            path = p;

            currentWaypoint = 0;
        }


    }



    public void Move()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {



            reachedEndofPath = true;
            return;
        }
        else
        {

            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * statModule.moveSpeed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {


            currentWaypoint++;

        }
    }

    public void Strafe()
    {

        if (target == null)
        {
            return;
        }

       // Calculate the direction to the target
        Vector2 targetDirection = ((Vector2)target.position - rb.position).normalized;

        // Determine the perpendicular direction for strafing
        Vector2 strafeDirection = new Vector2(-targetDirection.y, targetDirection.x);  // Perpendicular to target direction

        // Alternate the direction to strafe back and forth (use a sine wave or other function)
        float randomStrafeSpeed = UnityEngine.Random.Range(1.0f, 1.1f);
        float strafeAmount = Mathf.Sin(Time.time * randomStrafeSpeed) * statModule.moveSpeed;

        // Calculate the force to apply
        Vector2 force = strafeDirection * strafeAmount * Time.deltaTime;

        // Apply the force to the Rigidbody2D
        rb.AddForce(force);


    }



    public void SetElementType(ElementType _type)
    {
        statModule.runeType = _type;

        switch (_type)
        {
            case ElementType.Empty:
                break;
            case ElementType.Fire:
                //Resist
                statModule.damageResistances[ElementType.Ice] = 0.5f;

                //Take Extra Damage
                statModule.damageResistances[ElementType.Lightning] = 2.0f;
                statModule.damageResistances[ElementType.Shadow] = 1.25f;

                break;
            case ElementType.Ice:
                //Resist
                statModule.damageResistances[ElementType.Lightning] = 0.5f;

                //Take Extra Damage
                statModule.damageResistances[ElementType.Fire] = 2.0f;
                statModule.damageResistances[ElementType.Shadow] = 1.25f;


                break;
            case ElementType.Lightning:
                //Resist
                statModule.damageResistances[ElementType.Fire] = 0.5f;

                //Take Extra Damage
                statModule.damageResistances[ElementType.Ice] = 2.0f;
                statModule.damageResistances[ElementType.Shadow] = 1.25f;

                break;
            case ElementType.Earth:
                //Resist
                statModule.damageResistances[ElementType.Steel] = 0.5f;

                //Take Extra Damage
                statModule.damageResistances[ElementType.Crystal] = 2.0f;
                statModule.damageResistances[ElementType.Shadow] = 1.25f;

                break;
            case ElementType.Steel:
                //Resist
                statModule.damageResistances[ElementType.Crystal] = 0.5f;

                //Take Extra Damage
                statModule.damageResistances[ElementType.Earth] = 2.0f;
                statModule.damageResistances[ElementType.Shadow] = 1.25f;

                break;
            case ElementType.Crystal:
                //Resist
                statModule.damageResistances[ElementType.Earth] = 0.5f;

                //Take Extra Damage
                statModule.damageResistances[ElementType.Steel] = 2.0f;

                statModule.damageResistances[ElementType.Shadow] = 1.25f;


                break;
            case ElementType.Shadow:
                //Resist
                statModule.damageResistances[ElementType.Fire] = 0.75f;
                statModule.damageResistances[ElementType.Ice] = 0.75f;
                statModule.damageResistances[ElementType.Lightning] = 0.75f;
                statModule.damageResistances[ElementType.Earth] = 0.75f;
                statModule.damageResistances[ElementType.Steel] = 0.75f;
                statModule.damageResistances[ElementType.Crystal] = 0.75f;


                break;
            default:
                break;
        }


        vfxModule.SetColor(statModule.runeType);


    }

    public void SetCombatType(CombatType _type)
    {

        statModule.IngestStats(GameManager.Instance.masterStatData.GetStatData(_type));
        

    }

    public void SetStats(int _damage, int _health, float _fireInterval)
    {


        statModule.health += _health;
        statModule.maxHealth += _health;

        statModule.damage += _damage;
        statModule.fireInterval = statModule.fireInterval - _fireInterval;

    }

    


    public void Stun(float _duration)
    {




        stunTimer = _duration;

        isStunned = true;


    }

    public void StunTimer()
    {

        if (isStunned) 
        { 
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0)
            {
                vfxModule.StunnedVFX(false);

                isStunned = false;

            }

        }


    }

    public void Shield(float _duration)
    {


        shieldTimer = _duration;

        isShielded = true;

    }


    public void ShieldTimer()
    {


        if (isShielded)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0)
            {
                vfxModule.ShieldVFX(false);

                isShielded = false;

            }

        }

    }


}

using Pathfinding;
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

public enum CombatType
{
    Empty,
    Melee,
    Range,
    Mage

}
public class AICharacter : Character
{

    Player player;

    public AIState state = AIState.Chase;
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


    private float fireTimer = 5;

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

        HandleState();

    }

    private void HandleState()
    {

        switch (state)
        {
            case AIState.Idle:
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

                switch (statModule.combatType)
                {
                    case CombatType.Empty:
                        break;
                    case CombatType.Melee:
                        MeleeAttack();
                        break;
                    case CombatType.Range:
                        
                        RangeAttack();
                        break;
                    case CombatType.Mage:
                        

                        AreaAttack();
                        break;
                    default:
                        break;
                }
                break;
            default:
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


    public void MeleeAttack()
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
                    
                    GameObject newMelee = Instantiate(meleeAttack, target.transform.position, Quaternion.identity);
                    newMelee.GetComponent<MeleeAttack>().SetOwner(this);
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

    public void RangeAttack()
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

                    GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                    newProjectile.GetComponent<Projectile>().SetOwner(this);
                    newProjectile.GetComponent<Projectile>().Shoot(target);
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



    public void AreaAttack()
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

                    GameObject newAreaAttack = Instantiate(areaAttack, transform.position, Quaternion.identity);
                    newAreaAttack.GetComponent<AreaAttack>().SetOwner(this);
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

    bool canBackup = true;
    float backUpTimer = 0f;
    float backUpTimeLimit = 1.0f;

    public void CheckDistance()
    {

        float distanceToTarget = Vector2.Distance(target.position, transform.position);

        float minimumDistance = statModule.attackRange - 1f;

        Debug.Log(distanceToTarget);
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

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, statModule.detectRange, new Vector2(0, 0), 0, targetLayerMask);


        //If Enemy find player/ Overide Follow/ Chase Function

        if (hit)
        {

            if (target == null || target == player.transform)
            {
                target = hit.transform;

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
        float randomStrafeSpeed = Random.Range(1.0f, 1.1f);
        float strafeAmount = Mathf.Sin(Time.time * randomStrafeSpeed) * statModule.moveSpeed;

        // Calculate the force to apply
        Vector2 force = strafeDirection * strafeAmount * Time.deltaTime;

        // Apply the force to the Rigidbody2D
        rb.AddForce(force);


    }



    public void SetElementType(RuneType _type)
    {
        statModule.runeType = _type;

        vfxModule.SetColor(statModule.runeType);


    }

    public void SetCombatType(CombatType _type)
    {

        statModule.combatType = _type;

        switch (statModule.combatType)
        {
            case CombatType.Empty:
                break;
            case CombatType.Melee:
                statModule.attackRange = statModule.meleeAttackRange;
                break;
            case CombatType.Range:
                statModule.attackRange = statModule.rangedAttackRange;

                break;
            case CombatType.Mage:
                statModule.attackRange = statModule.mageAttackRange;

                break;
            default:
                break;
        }

    }

    public void SetStats(int _damage, int _health, float _fireInterval)
    {


        statModule.health += _health;
        statModule.maxHealth += _health;

        statModule.damage += _damage;
        statModule.fireInterval = statModule.fireInterval - _fireInterval;

    }


}

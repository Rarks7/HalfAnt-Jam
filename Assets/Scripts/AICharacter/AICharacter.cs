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

    public void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();

        target = FindObjectOfType<Player>().transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);

        player = FindObjectOfType<Player>();
    }

    protected void FixedUpdate()
    {
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

        fireTimer = statModule.fireInterval;

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

        fireTimer = statModule.fireInterval;

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
        }

        if (target != null)
        {


            if (Vector2.Distance(target.position, (Vector2)transform.position) > statModule.attackRange)
            {

                state = AIState.Chase;

            }
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
        }

        if (target != null)
        {


            if (Vector2.Distance(target.position, (Vector2)transform.position) > statModule.attackRange)
            {

                state = AIState.Chase;

            }
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
        }

        if (target != null)
        {


            if (Vector2.Distance(target.position, (Vector2)transform.position) > statModule.attackRange)
            {

                state = AIState.Chase;

            }
        }
        else
        {

            Detect();
        }

    }


    

    public void Detect()
    {

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, statModule.detectRange, new Vector2(0, 0), 0, targetLayerMask);
        

        if (hit)
        {


            target = hit.transform;


        }
        else
        {

            target = player.transform;
            state = AIState.Follow;
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

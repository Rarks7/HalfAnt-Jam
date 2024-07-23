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



public class AICharacter : Character
{

    AIState state = AIState.Chase;
    [SerializeField] public LayerMask targetLayerMask;


    Seeker seeker;
    public Transform target;
    private float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;


    [SerializeField]GameObject projectile;

    private float fireTimer = 5;

    public void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();

        target = FindObjectOfType<Player>().transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);

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
                break;
            case AIState.Chase:
                Detect();

                Chase();
                break;
            case AIState.Attack:
                Attack();
                break;
            default:
                break;
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

            target = FindObjectOfType<Player>().transform;
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


            target = FindObjectOfType<Player>().transform;
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

}

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


    Seeker seeker;

    [SerializeField] public LayerMask targetLayerMask;


    public Transform target;

    public float nextWaypointDistance = 3f;


    Path path;
    int currentWaypoint = 0;

    bool reachedEndofPath = false;



    private float attackRange = 5;
    private float detectRange = 10;

    private float fireInterval = 5;
    private float fireTimer = 5;



    [SerializeField]GameObject projectile;

    public void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();

        target = FindObjectOfType<Player>().transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);



    }

    protected void FixedUpdate()
    {
        Detect();
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


        if (Vector2.Distance(target.position, (Vector2)transform.position) <= attackRange)
        {

            state = AIState.Attack;

        }


    }

    public void Attack()
    {




        if (fireTimer <= 0)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().Shoot(target);
            fireTimer = fireInterval;
        }
        else 
        { 
        
            fireTimer -= Time.deltaTime;
        
        }

        if (Vector2.Distance(target.position, (Vector2)transform.position) > attackRange)
        {

            state = AIState.Chase;

        }

    }

    public void Detect()
    {

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectRange, new Vector2(0, 0), 0, targetLayerMask);
        

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

            seeker.StartPath(rb.position, target.position, OnPathComplete);
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

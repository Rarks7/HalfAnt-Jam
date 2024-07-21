using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIState
{

    Idle,
    Follow,
    Pursue,
    Attack

}



public class AICharacter : Character
{
    AIDestinationSetter destinationSetter;
    AIPath AIPath;


    [SerializeField] LayerMask targetLayerMask;



    public void Awake()
    {
        base.Awake();
        destinationSetter = GetComponent<AIDestinationSetter>();
        AIPath = GetComponent<AIPath>();

        AIPath.maxSpeed = statModule.moveSpeed;

    }

    protected void Update()
    {
        Detect();

    }

    public void Attack()
    {



    }


    public void Detect()
    {


        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2.0f, new Vector2(0, 0), 0, targetLayerMask);

        if (hit)
        {

            destinationSetter.target = hit.transform;



        }
        else
        {

            destinationSetter.target = FindObjectOfType<Player>().transform;


        }

    }
}

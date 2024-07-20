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

    [SerializeField] LayerMask targetLayerMask;



    public void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detect();

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

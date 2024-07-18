using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Pathfinding;

public class Summon : Character
{


    AIDestinationSetter destinationSetter;

    public RuneType enemyType = RuneType.Empty;
    SpriteRenderer spriteRenderer;

    private Vector2 startingPosition;
    private Vector2 roamPosition;






    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();

        destinationSetter = GetComponent<AIDestinationSetter>();

        destinationSetter.target = FindObjectOfType<Enemy>().transform;
    }


    private Vector2 GetRoamingPosition()
    {

        return startingPosition + GetRandDir() * Random.Range(10f, 70f);
    }


    public Vector2 GetRandDir()
    {

        return new Vector2(Random.Range(-1f, -1f), Random.Range(-1f, -1f)).normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemyType(RuneType _type)
    {

        enemyType = _type;

        switch (enemyType)
        {
            case RuneType.Empty:
                break;
            case RuneType.Fire:

                spriteRenderer.color = Color.red;

                break;
            case RuneType.Ice:

                spriteRenderer.color = Color.white;

                break;
            case RuneType.Lightning:

                spriteRenderer.color = Color.yellow;

                break;
            default:
                break;
        }

    }




}

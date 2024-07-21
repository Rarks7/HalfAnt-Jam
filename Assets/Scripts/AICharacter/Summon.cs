using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Pathfinding;
using Pathfinding.Examples;

public class Summon : AICharacter
{
    public RuneType summonType = RuneType.Empty;
    SpriteRenderer spriteRenderer;



    private void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void SetEnemyType(RuneType _type)
    {

        summonType = _type;

        switch (summonType)
        {
            case RuneType.Empty:
                break;
            case RuneType.Fire:

                spriteRenderer.color = Color.red;

                break;
            case RuneType.Ice:

                spriteRenderer.color = Color.cyan;

                break;
            case RuneType.Lightning:

                spriteRenderer.color = Color.yellow;

                break;
            default:
                break;
        }

    }

}

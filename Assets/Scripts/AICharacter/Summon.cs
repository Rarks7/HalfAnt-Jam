using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Pathfinding;
using Pathfinding.Examples;

public enum CombatType
{
    Empty,
    Melee,
    Range,
    Mage

}


public class Summon : AICharacter
{
    public RuneType summonElementType = RuneType.Empty;
    public CombatType summonCombatType = CombatType.Empty;
    SpriteRenderer spriteRenderer;



    protected override void Awake()
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

    public void SetElementType(RuneType _type)
    {

        summonElementType = _type;

        switch (summonElementType)
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

                spriteRenderer.color = Color.magenta;

                break;
            default:
                break;
        }

    }

    public void SetCombatType(CombatType _type)
    {

        summonCombatType = _type;

        switch (summonCombatType)
        {
            case CombatType.Empty:
                break;
            case CombatType.Melee:
                break;
            case CombatType.Range:
                break;
            case CombatType.Mage:
                break;
            default:
                break;
        }

    }

    public void SetStats(int _damage, int _health,  float _fireInterval)
    {

        
        statModule.health += _health;
        statModule.maxHealth += _health;

        statModule.damage += _damage;
        statModule.fireInterval = statModule.fireInterval - _fireInterval;

    }


}

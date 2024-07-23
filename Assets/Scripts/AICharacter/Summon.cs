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

    public void SetStats(int _damage, int _health,  float _fireInterval)
    {

        
        statModule.health += _health;
        statModule.maxHealth += _health;

        statModule.damage += _damage;
        statModule.fireInterval = statModule.fireInterval - _fireInterval;

    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModule : MonoBehaviour
{
    [Header("Health")]
    public float health = 0;
    public float maxHealth = 0;

    [Header("Movement")]
    public float moveSpeed = 10;
    public float dashCooldown = 1.0f;



    [Header("Abilities")]
    public float recallCooldown = 5.0f;
    public float shuffleCooldown = 5.0f;


    [Header("AI ONLY")]
    public ElementType runeType;
    public CombatType combatType;

    public float attackRange = 0;

    public float detectRange = 10;

    public float fireInterval = 5;


    [Header("Damage")]
    public float damage = 5;
    public float projectileSpeed = 500;

    [Header("Buffs")]
    public float healAmount = 5;
    public float enchantAmount = 5;
    public float stunDuration = 5;
    public float shieldDuration = 5;




    public Dictionary<ElementType, float> damageResistances;



    private void Awake()
    {
        damageResistances = new Dictionary<ElementType, float>();

        damageResistances.Add(ElementType.Fire, 1);
        damageResistances.Add(ElementType.Ice, 1);
        damageResistances.Add(ElementType.Lightning, 1);
        damageResistances.Add(ElementType.Earth, 1);
        damageResistances.Add(ElementType.Steel, 1);
        damageResistances.Add(ElementType.Crystal, 1);
        damageResistances.Add(ElementType.Shadow, 1);




    }

    public void IngestStats(StatData _data)
    {


        health = _data.health;
        maxHealth = _data.maxHealth;


        moveSpeed = _data.moveSpeed;
        dashCooldown = _data.dashCooldown;

        recallCooldown = _data.recallCooldown;
        shuffleCooldown = _data.shuffleCooldown;


        combatType = _data.combatType;

        attackRange = _data.attackRange;

        detectRange = _data.detectRange;

        fireInterval = _data.fireInterval;

        damage = _data.damage;
        projectileSpeed = _data.projectileSpeed;

        healAmount = _data.healAmount;
        enchantAmount = _data.enchantAmount;
        stunDuration = _data.stunDuration;
        shieldDuration  = _data.shieldDuration;

    }

        



 }




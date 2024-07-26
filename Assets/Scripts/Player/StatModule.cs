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


    [Header("AI ONLY")]
    public ElementType runeType;
    public CombatType combatType;

    public float attackRange = 0;

    public float rangedAttackRange = 1;
    public float meleeAttackRange = 5;
    public float mageAttackRange = 2;

    public float detectRange = 10;

    public float fireInterval = 5;


    [Header("Damage")]
    public float damage = 5;
    public float projectileSpeed = 500;


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




}

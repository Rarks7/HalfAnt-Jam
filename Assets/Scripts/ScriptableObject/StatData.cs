using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewStatData", menuName = "Data/StatData")]

[Serializable]
public class StatData : ScriptableObject
{

    [Header("Health")]
    public float health = 20;
    public float maxHealth = 20;

    [Header("Movement")]
    public float moveSpeed = 10;
    public float dashCooldown = 1;

    [Header("Abilities")]
    public float recallCooldown = 3.0f;
    public float shuffleCooldown = 3.0f;



    [Header("AI ONLY")]
    public ElementType runeType;
    public CombatType combatType;

    public float attackRange = 2;

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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModule : MonoBehaviour
{
    [Header("Health")]
    public int health = 0;
    public int maxHealth = 0;

    [Header("Movement")]
    public float moveSpeed = 10;


    [Header("AI ONLY")]
    public RuneType runeType;
    public CombatType combatType;

    public float attackRange = 0;

    public float rangedAttackRange = 1;
    public float meleeAttackRange = 5;
    public float mageAttackRange = 2;

    public float detectRange = 10;

    public float fireInterval = 5;



    [Header("Damage")]
    public int damage = 5;
    public int projectileSpeed = 500;
}

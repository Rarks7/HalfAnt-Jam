using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected Rigidbody2D rb;
    [NonSerialized] public StatModule statModule;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        statModule = GetComponent<StatModule>();
        statModule.health = statModule.maxHealth;
    }

    public void TakeDamage(int _damage)
    {

        statModule.health -= _damage;

        if (statModule.health <= 0)
        {

            Die();

        }

    }

    public void Die()
    {

        Destroy(gameObject);


    }


}

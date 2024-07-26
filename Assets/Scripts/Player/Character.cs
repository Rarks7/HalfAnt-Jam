using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected Rigidbody2D rb;
    [NonSerialized] public StatModule statModule;
    [NonSerialized] public VFXModule vfxModule;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        statModule = GetComponent<StatModule>();
        vfxModule = GetComponent<VFXModule>();

        statModule.health = statModule.maxHealth;
    }

    protected virtual void FixedUpdate(){}

    public void TakeDamage(float _damage, ElementType _element)
    {

        statModule.health -= _damage * statModule.damageResistances[_element];
        vfxModule.StartDamageFlash();

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

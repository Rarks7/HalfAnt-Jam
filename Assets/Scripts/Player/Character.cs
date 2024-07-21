using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected StatModule statModule;


    protected void Awake()
    {

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

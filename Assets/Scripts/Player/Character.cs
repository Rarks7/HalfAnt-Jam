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

    public void TakeDamage(int _damage, RuneType _element)
    {

        switch (_element)
        {
            case RuneType.Empty:
                break;
            case RuneType.Fire:
                if (statModule.runeType == RuneType.Ice)
                {
                    _damage *= 2;
                }
                if (statModule.runeType == RuneType.Lightning)
                {
                    _damage /= 2;
                }
                break;
            case RuneType.Ice:
                if (statModule.runeType == RuneType.Fire)
                {
                    _damage /= 2;
                }
                if (statModule.runeType == RuneType.Lightning)
                {
                    _damage *= 2;
                }
                break;
            case RuneType.Lightning:
                if (statModule.runeType == RuneType.Ice)
                {
                    _damage /= 2;
                }
                if (statModule.runeType == RuneType.Fire)
                {
                    _damage *= 2;
                }
                break;
            default:
                break;
        }

        statModule.health -= _damage;
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

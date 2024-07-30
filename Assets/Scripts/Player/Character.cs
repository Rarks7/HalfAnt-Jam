using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected Rigidbody2D rb;
    [NonSerialized] public StatModule statModule;
    [NonSerialized] public VFXModule vfxModule;

    protected bool isShielded = false;

    public bool isDead { get; protected set; }

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
        if (!isShielded)
        {
            statModule.health -= _damage * statModule.damageResistances[_element];
            vfxModule.CreateFloatingText(transform, (_damage * statModule.damageResistances[_element]).ToString(), TextType.Damage);
            vfxModule.StartDamageFlash();
            if (this is Summon)
            {

                AudioManager.instance.Play("SummonTakeDamage");

            }
            else if(this is Enemy)
            {
                AudioManager.instance.Play("EnemyTakeDamage");

            }
        }
        else
        {

            vfxModule.CreateFloatingText(transform, (_damage * statModule.damageResistances[_element]).ToString(), TextType.Block);


        }



        if (statModule.health <= 0)
        {

            if (this is Summon summon)
            {
                AIManager.Instance.activeSummons.Remove(this as Summon);

                foreach (var rune in summon.runes)
                {
                    Player player = FindAnyObjectByType<Player>();
                        player.deckModule.AddToDeck(rune);

                }
            }
            else if (this is Enemy)
            {


                AIManager.Instance.activeEnemies.Remove(this as Enemy);


            }
            Die();

        }

    }

    public void Heal(float _heal, ElementType _element)
    {


        
        statModule.health += _heal;
        vfxModule.CreateFloatingText(transform, _heal.ToString(), TextType.Heal);





    }


    public void Die()
    {

        Destroy(gameObject);
        isDead = true;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // Start is called before the first frame update

    AICharacter owner;

    Animator animator;

    private void Awake()
    {
        Destroy(gameObject, 3.0f);
        animator = GetComponentInChildren<Animator>();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetOwner(AICharacter _AI)
    {

        owner = _AI;

        switch (owner.statModule.runeType)
        {
            case RuneType.Empty:
                animator.SetTrigger("Fire");

                break;
            case RuneType.Fire:
                animator.SetTrigger("Fire");

                break;
            case RuneType.Ice:
                animator.SetTrigger("Ice");

                break;
            case RuneType.Lightning:
                animator.SetTrigger("Lightning");

                break;
            default:
                animator.SetTrigger("Fire");

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the collision object's layer is included in the targetLayerMask
        if ((owner.targetLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            AICharacter AI = collision.gameObject.GetComponent<AICharacter>();

            // Ensure that the collided object has an AICharacter component
            if (AI != null)
            {
                AI.TakeDamage(owner.statModule.damage, owner.statModule.runeType);
                //Destroy(gameObject);
            }
        }


    }
}

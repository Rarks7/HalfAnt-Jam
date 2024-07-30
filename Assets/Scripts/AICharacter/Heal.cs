using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update

    AICharacter owner;

    Animator animator;

    private void Awake()
    {
        Destroy(gameObject, 3.0f);
        animator = GetComponentInChildren<Animator>();
        AudioManager.instance.Play("Heal");
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the collision object's layer is included in the targetLayerMask
        if ((owner.allyLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            AICharacter AI = collision.gameObject.GetComponent<AICharacter>();

            // Ensure that the collided object has an AICharacter component
            if (AI != null && AI.transform != owner.transform)
            {

                AI.Heal(owner.statModule.healAmount, owner.statModule.runeType);
            }
        }


    }

}

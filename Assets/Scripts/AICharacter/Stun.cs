using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    // Start is called before the first frame update

    AICharacter owner;

    Animator animator;
    
    VFXModule vfxModule;
    private void Awake()
    {
        Destroy(gameObject, 3.0f);
        animator = GetComponentInChildren<Animator>();
        vfxModule = GetComponentInChildren<VFXModule>();
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
        if ((owner.targetLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            AICharacter AI = collision.gameObject.GetComponent<AICharacter>();

            // Ensure that the collided object has an AICharacter component
            if (AI != null)
            {
                AI.Stun(owner.statModule.stunDuration);
                AI.vfxModule.CreateFloatingText(AI.transform, "", TextType.Stun);
                AI.vfxModule.StunnedVFX(true);
                AudioManager.instance.Play("Stun");

            }
        }


    }

}

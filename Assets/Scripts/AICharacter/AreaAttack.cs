using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
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

        switch (owner.statModule.runeType)
        {
            case ElementType.Empty:
                animator.SetTrigger("Fire");
                vfxModule.SetColor(owner.statModule.runeType);
                AudioManager.instance.Play("FireArea");
                break;
            case ElementType.Fire:
                animator.SetTrigger("Fire");
                AudioManager.instance.Play("FireArea");

                break;
            case ElementType.Ice:
                animator.SetTrigger("Ice");
                AudioManager.instance.Play("IceArea");

                break;
            case ElementType.Lightning:
                animator.SetTrigger("Lightning");
                vfxModule.SetColor(owner.statModule.runeType);
                AudioManager.instance.Play("LightningArea");

                break;
            case ElementType.Earth:
                animator.SetTrigger("Earth");
                AudioManager.instance.Play("EarthArea");

                break;
            case ElementType.Steel:
                animator.SetTrigger("Steel");
                AudioManager.instance.Play("SteelArea");

                break;
            case ElementType.Crystal:
                animator.SetTrigger("Crystal");
                AudioManager.instance.Play("CrystalArea");

                break;
            case ElementType.Shadow:
                animator.SetTrigger("Shadow");
                AudioManager.instance.Play("ShadowArea");

                break;
            default:
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



            }
        }


    }

}

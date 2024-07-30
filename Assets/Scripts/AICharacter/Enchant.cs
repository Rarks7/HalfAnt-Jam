using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchant : MonoBehaviour
{


    AICharacter owner;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, 0, 0.1f));

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
            if (AI != null)
            {
                AI.statModule.damage = AI.statModule.damage + owner.statModule.enchantAmount;
                AI.vfxModule.CreateFloatingText(AI.transform, "", TextType.Buff);
                AudioManager.instance.Play("Buff");




            }
        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if ((owner.allyLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            AICharacter AI = collision.gameObject.GetComponent<AICharacter>();

            // Ensure that the collided object has an AICharacter component
            if (AI != null)
            {
                AI.statModule.damage = AI.statModule.damage - owner.statModule.enchantAmount;
                AI.vfxModule.CreateFloatingText(AI.transform, "", TextType.Debuff);
                AudioManager.instance.Play("Debuff");





            }
        }

    }

}

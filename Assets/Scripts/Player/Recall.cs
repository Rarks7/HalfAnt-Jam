using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall : MonoBehaviour
{



    private void Awake()
    {
        Destroy(gameObject, 1.0f);

        AudioManager.instance.Play("Recall");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


            Debug.Log("Recall");
            AICharacter AI = collision.gameObject.GetComponent<AICharacter>();

            
            if (AI != null)
            {

            if (AI is Summon)
            {
                AI.TakeDamage(99999, ElementType.Fire);
            }
                

            }
        


    }
}

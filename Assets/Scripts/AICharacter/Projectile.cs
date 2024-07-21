using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    public void Shoot(Transform _target)
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        Vector2 force = direction * 1000 * Time.deltaTime;
        rb.AddForce(force, ForceMode2D.Impulse);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Summon")) 
        { 
        
            AICharacter AI = collision.gameObject.GetComponent<AICharacter>();

            AI.TakeDamage(5);
            Destroy(gameObject);
        }

    }
}

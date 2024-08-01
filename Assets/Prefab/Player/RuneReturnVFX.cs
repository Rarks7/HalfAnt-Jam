using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneReturnVFX : MonoBehaviour
{
    Rigidbody2D rb;
    TrailRenderer trailRenderer;
    Player player;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Return();
    }



    public void Return()
    {

        Vector3 moveDir = (player.transform.position - transform.position).normalized;

        rb.AddForce(moveDir * 5000 * Time.deltaTime);



    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision != null) 
        { 
            Player player = collision.GetComponent<Player>();


            if (player != null) 
            {

                Destroy(gameObject);
            
            }
        
            
        
        }



    }



}

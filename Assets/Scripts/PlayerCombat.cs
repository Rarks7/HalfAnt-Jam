using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    Rigidbody2D rb;


    RuneModule runeModule;
    StatModule statModule;



    Vector2 moveInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        runeModule = GetComponent<RuneModule>();
        statModule = GetComponent<StatModule>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {

        //Move the player
        rb.MovePosition(rb.position + moveInput * statModule.moveSpeed * Time.fixedDeltaTime);

    }



    public void CastRune(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeModule.Cast();


        }



    }

    public void Move(InputAction.CallbackContext _context)
    {

        moveInput = _context.ReadValue<Vector2>();

        
    }

}

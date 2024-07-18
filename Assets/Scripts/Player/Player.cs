using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    [NonSerialized]
    public SummonModule summonModule;
    RuneModule runeModule;
    StatModule statModule;



    Vector2 moveInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        runeModule = GetComponent<RuneModule>();
        statModule = GetComponent<StatModule>();
        summonModule = GetComponent<SummonModule>();

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



    public void CastRune1(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeModule.Cast(RuneType.Fire);


        }

    }

    public void CastRune2(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeModule.Cast(RuneType.Ice);


        }

    }

    public void CastRune3(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeModule.Cast(RuneType.Lightning);


        }

    }

    public void Summon(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeModule.Summon();


        }

    }

    public void Move(InputAction.CallbackContext _context)
    {

        moveInput = _context.ReadValue<Vector2>();

        
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{


    [NonSerialized]
    public SummonModule summonModule;
    RuneModule runeModule;
    StatModule statModule;
    [SerializeField] Animator ani;


    [SerializeField] LayerMask walkLayerMask;
    float collisionOffset = 0.05f;

    Vector2 moveInput = new Vector2(0,0);
    Vector2 lastVelocity = new Vector2(0,0);
    


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
        AnimateCharacter();
    }


    private void FixedUpdate()
    {

        //Move the player
        MoveCharacter(moveInput);

    }

    private void AnimateCharacter()
    {
        Vector2 norm = moveInput.normalized;

        ani.SetFloat(Constants.AnimationParameters.f_X, norm.x);
        ani.SetFloat(Constants.AnimationParameters.f_Y, norm.y);
        
        if(norm.magnitude > 0)
        {
            if(lastVelocity != norm)
            {
                ani.SetTrigger(Constants.AnimationParameters.t_DirectionChange);
                lastVelocity = norm;
            }
            
            
            ani.SetBool(Constants.AnimationParameters.b_Walking, true);
        }
        else
        {
            ani.SetBool(Constants.AnimationParameters.b_Walking, false);
        }
    }
    private bool MoveCharacter(Vector2 direction)
    {

        if (!Physics2D.Raycast(rb.position, direction, statModule.moveSpeed * Time.fixedDeltaTime + collisionOffset, walkLayerMask))
        {
            rb.MovePosition(rb.position + statModule.moveSpeed * Time.fixedDeltaTime * moveInput);
            return true;
        }
        else
        {
            return false;
        }
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

        if(_context.performed)
        {
            if (moveInput.x > 0)
            {
                EventManager.PlayerPressedRight();
                Debug.Log("Right Pressed");
            }
            else if (moveInput.x < 0)
            {
                EventManager.PlayerPressedLeft();
                Debug.Log("Left Pressed");
            }
            else if (moveInput.y > 0)
            {
                EventManager.PlayerPressedUp();
                Debug.Log("Up Pressed");
            }
            else if (moveInput.y < 0)
            {
                EventManager.PlayerPressedDown();
                Debug.Log("Down Pressed");
            }
        }

        
    }

    public void Interact(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            EventManager.PlayerPressedInteract();
            Debug.Log("Player Pressed Interact");
        }
    }


    private void OnDrawGizmos()
    {
        if(moveInput.magnitude > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(rb.position, moveInput * (statModule.moveSpeed * Time.fixedDeltaTime + collisionOffset));
        }
        
    }

}

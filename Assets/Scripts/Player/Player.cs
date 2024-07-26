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

    [NonSerialized] public InteractModule interactModule;


    [SerializeField] LayerMask walkLayerMask;
    float collisionOffset = 0.05f;

    Vector2 moveInput = new Vector2(0,0);
    Vector2 lastVelocity = new Vector2(0,0);

    public Vector2 Facing { get; private set; } = new Vector2(0,1);

    private BoxCollider2D coll;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = rb.GetComponent<BoxCollider2D>();

        runeModule = GetComponent<RuneModule>();
        statModule = GetComponent<StatModule>();
        summonModule = GetComponent<SummonModule>();
        interactModule = GetComponent<InteractModule>();

        StartCoroutine(LateStart());

    }

    // Update is called once per frame
    void Update()
    {
        AnimateCharacter();
    }


    protected override void FixedUpdate()
    {
        //base.FixedUpdate();
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
            Facing = norm;
            
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
        if (!Physics2D.BoxCast(rb.position + coll.offset, coll.size, 0, direction, statModule.moveSpeed * Time.fixedDeltaTime + collisionOffset, walkLayerMask))
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

            runeModule.Cast(ElementType.Fire, CombatType.Fighter);




        }

    }

    public void CastRune2(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeModule.Cast(ElementType.Ice, CombatType.Ranger);


        }

    }

    public void CastRune3(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeModule.Cast(ElementType.Lightning, CombatType.Mage);


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
                //Debug.Log("Right Pressed");
            }
            else if (moveInput.x < 0)
            {
                EventManager.PlayerPressedLeft();
                //Debug.Log("Left Pressed");
            }
            else if (moveInput.y > 0)
            {
                EventManager.PlayerPressedUp();
                //Debug.Log("Up Pressed");
            }
            else if (moveInput.y < 0)
            {
                EventManager.PlayerPressedDown();
                //Debug.Log("Down Pressed");
            }
        }

        if(GameManager.Instance.CurrentState == GameState.Chatting)
        {
            moveInput = Vector2.zero;
        }
        
    }

    public void Interact(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            EventManager.PlayerPressedInteract();
            //Debug.Log("Player Pressed Interact");

            if(GameManager.Instance.CurrentState == GameState.Overworld)
            {
                interactModule.TryInteract();
            }
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

    private void Spawn()
    {
        Vector3 spawnPoint = SceneController.Instance.GetSpawnPoint();

        transform.position = spawnPoint;
    }


    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();

        Spawn();
    }

}

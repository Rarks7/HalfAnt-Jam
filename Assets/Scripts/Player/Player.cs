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
    public DeckModule deckModule;
    StatModule statModule;
    [SerializeField] Animator ani;

    [NonSerialized] public InteractModule interactModule;


    [SerializeField] LayerMask walkLayerMask;
    float collisionOffset = 0.05f;

    Vector2 moveInput = new Vector2(0,0);
    Vector2 lastVelocity = new Vector2(0,0);

    public Vector2 Facing { get; private set; } = new Vector2(0,1);

    private BoxCollider2D coll;

    //Dash
    float dashTimer;
    bool canDash;
    TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = rb.GetComponent<BoxCollider2D>();

        runeModule = GetComponent<RuneModule>();
        statModule = GetComponent<StatModule>();
        summonModule = GetComponent<SummonModule>();
        deckModule = GetComponent<DeckModule>();


        interactModule = GetComponent<InteractModule>();

        trailRenderer = GetComponentInChildren<TrailRenderer>();

        StartCoroutine(LateStart());
    }

    // Update is called once per frame
    void Update()
    {
        DashTimer();
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
            if (!RuneDeckUI.Instance.runeHandUI[0].selected && runeModule.activeRunes.Count < 3)
            {
                runeModule.Cast(deckModule.runeHand[0]);
                RuneDeckUI.Instance.runeHandUI[0].Select();

            }
            else
            {
                RuneDeckUI.Instance.runeHandUI[0].StartCantSelect();


            }


        }

    }

    public void CastRune2(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            if (!RuneDeckUI.Instance.runeHandUI[1].selected && runeModule.activeRunes.Count < 3)
            {
                runeModule.Cast(deckModule.runeHand[1]);
                RuneDeckUI.Instance.runeHandUI[1].Select();

            }
            else
            {
                RuneDeckUI.Instance.runeHandUI[1].StartCantSelect();


            }


        }

    }

    public void CastRune3(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            if (!RuneDeckUI.Instance.runeHandUI[2].selected && runeModule.activeRunes.Count < 3)
            {
                runeModule.Cast(deckModule.runeHand[2]);
                RuneDeckUI.Instance.runeHandUI[2].Select();

            }
            else
            {
                RuneDeckUI.Instance.runeHandUI[2].StartCantSelect();


            }



        }

    }

    public void CastRune4(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            if (!RuneDeckUI.Instance.runeHandUI[3].selected && runeModule.activeRunes.Count < 3)
            {
                runeModule.Cast(deckModule.runeHand[3]);
                RuneDeckUI.Instance.runeHandUI[3].Select();

            }
            else
            {
                RuneDeckUI.Instance.runeHandUI[3].StartCantSelect();


            }


        }

    }

    public void CastRune5(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {



            if (!RuneDeckUI.Instance.runeHandUI[4].selected && runeModule.activeRunes.Count < 3)
            {
                runeModule.Cast(deckModule.runeHand[4]);
                RuneDeckUI.Instance.runeHandUI[4].Select();

            }
            else
            {
                RuneDeckUI.Instance.runeHandUI[4].StartCantSelect();


            }


        }

    }

    public void Summon(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            if (runeModule.activeRunes.Count > 0)
            {
                deckModule.Cast(RuneDeckUI.Instance.runeHandUI);
                runeModule.Summon();
            }
            


        }

    }

    public void Dash(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            StartCoroutine(Dash());



        }

    }

    public void Recall(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            Recall();



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




    public void DashTimer()
    {

        if (!canDash)
        {

            dashTimer -= Time.deltaTime;


        }

        if (dashTimer <=0)
        {
            canDash = true;
            dashTimer = statModule.dashCooldown;
        }

    }

    IEnumerator Dash()
    {
        if (canDash)
        {

            canDash = false;
            statModule.moveSpeed += 10;
            trailRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);

            statModule.moveSpeed -= 10;
            trailRenderer.enabled = false;

        }
    }

    public void Recall()
    {




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

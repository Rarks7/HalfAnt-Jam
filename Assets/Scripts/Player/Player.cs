using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{

    [NonSerialized]
    public SummonModule summonModule;

    [NonSerialized]
    public RuneDeckModule runeDeckModule;

    StatModule statModule;


    [SerializeField] Animator ani;
    PlayerUI playerUI;

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

    //Recall
    float recallTimer;
    bool canRecall;

    //Shuffle
    bool canShuffle;
    float shuffleTimer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = rb.GetComponent<BoxCollider2D>();

        statModule = GetComponent<StatModule>();
        summonModule = GetComponent<SummonModule>();
        runeDeckModule = GetComponent<RuneDeckModule>();
        playerUI = FindObjectOfType<PlayerUI>();


        if(playerUI != null )
        {
            playerUI.SetRemainingDeckNumber(runeDeckModule.runeDeck.Count);
        }
        
        interactModule = GetComponent<InteractModule>();

        trailRenderer = GetComponentInChildren<TrailRenderer>();

        StartCoroutine(LateStart());
    }

    // Update is called once per frame
    void Update()
    {
        if(playerUI != null)
        {
            DashTimer();
            RecallTimer();
            ShuffleTimer();
            playerUI.SetHealthText(statModule.health);
            playerUI.SetRemainingDeckNumber(runeDeckModule.runeDeck.Count);
        }
        
        



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

            runeDeckModule.Cast(runeDeckModule.runeHand[0], 0);



        }

    }

    public void CastRune2(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            runeDeckModule.Cast(runeDeckModule.runeHand[1], 1);


        }

    }

    public void CastRune3(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            runeDeckModule.Cast(runeDeckModule.runeHand[2], 2);

        }

    }

    public void CastRune4(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            runeDeckModule.Cast(runeDeckModule.runeHand[3], 3);

        }

    }

    public void CastRune5(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeDeckModule.Cast(runeDeckModule.runeHand[4], 4);

        }

    }

    public void Summon(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {

            runeDeckModule.Summon();
            

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

            if (canRecall)
            {
                summonModule.RecallSummon();
                canRecall = false;
                playerUI.SetRecallDulled(true);

            }



        }

    }

    public void Shuffle(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            if (canShuffle)
            {
                //runeDeckModule.ReturnHandToDeck(runeHandUI.runeHandUI);
                canShuffle = false;
                playerUI.SetShuffleDulled(true);
                AudioManager.instance.Play("Shuffle");


            }

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


    public void ShuffleTimer()
    {

        if (!canShuffle)
        {

            shuffleTimer -= Time.deltaTime;


        }

        if (shuffleTimer <= 0)
        {
            canShuffle = true;
            playerUI.SetShuffleDulled(false);
            shuffleTimer = statModule.shuffleCooldown;

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
            playerUI.SetDashDulled(false);
        }

    }

    IEnumerator Dash()
    {
        if (canDash)
        {

            canDash = false;
            playerUI.SetDashDulled(true);

            statModule.moveSpeed += 10;
            trailRenderer.enabled = true;
            AudioManager.instance.Play("Dash");
            yield return new WaitForSeconds(0.1f);

            statModule.moveSpeed -= 10;
            trailRenderer.enabled = false;

        }
    }


    


    public void RecallTimer()
    {

        if (!canRecall)
        {

            recallTimer -= Time.deltaTime;


        }

        if (recallTimer <= 0)
        {
            canRecall = true;
            playerUI.SetRecallDulled(false);
            recallTimer = statModule.recallCooldown;
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



    public override void Die()
    {
        EventManager.PlayerCharacterDied();
        playerUI.SetHealthText(0);
        base.Die();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRB;
    [Range(0f,10f)][SerializeField] float playerSpeed;
    Animator playerAnimator;
    [SerializeField] float jumpSpeed = 5f;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeetCollider;
    [SerializeField] float climbSpeed = 4f;
    [SerializeField] float gravity=8f;
    bool isAlive=true;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
  
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {playerRB.gravityScale = gravity; playerAnimator.SetBool("isClimbing", false); return; }
        
            Vector2 climbingVelocity = new Vector2(playerRB.velocity.x, moveInput.y * climbSpeed);
            playerRB.velocity = climbingVelocity;
            playerRB.gravityScale = 0f;          

        if (Mathf.Abs(moveInput.y) > Mathf.Epsilon)
        {
            playerAnimator.SetBool("isClimbing", true);
        }
        else playerAnimator.SetBool("isClimbing", false);

    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            playerRB.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void FlipSprite()
    {
        if (Mathf.Abs(moveInput.x)>Mathf.Epsilon) {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1f);
        } 
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x* playerSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;
        if (Mathf.Abs(moveInput.x) > Mathf.Epsilon)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else playerAnimator.SetBool("isRunning", false);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();        
    }

    void Die()
    {
        if (playerRB.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRB.velocity = new Vector2(0f, 20f);
            myCapsuleCollider.enabled = !myCapsuleCollider.enabled;
            StartCoroutine(DeadLoad());
            
        }
       

    }
    IEnumerator DeadLoad()
    {
        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}

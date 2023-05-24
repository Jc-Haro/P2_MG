using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    private Rigidbody2D playerRigidBody;
    private float playerInput = 0.0f;
    private float playerSpeed = 150.0f;
    private float jumpForce = 7.5f;
    private float radius;
    private bool isGrounded = false;
    private bool hasWallJump = false;
    private bool isOnWall = false;
    Vector3 scale;
    public Transform origin;
    private float originalScaleX;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        originalScaleX = scale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //playerInput = Input.GetAxis("Horizontal");
        playerInput = ControlInstance.Instanse.GamePlay.Move.ReadValue<float>();

        if (playerInput < 0) { scale.x = -originalScaleX; }
        else if (playerInput>0) { scale.x = originalScaleX; }
        transform.localScale = scale;
       

        if(playerInput != 0 )
        {
            playerAnimator.SetFloat("WalKing_LR_F", playerInput);
            playerAnimator.SetBool("Walking_B", true);
           
  
        }
        else
        {
            playerAnimator.SetBool("Walking_B", false);
        }



        if (ControlInstance.Instanse.GamePlay.Jump.WasPressedThisFrame() && isGrounded )
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            hasWallJump = false;
            playerAnimator.SetTrigger("Jump_T");
        }

        if (ControlInstance.Instanse.GamePlay.Jump.WasPressedThisFrame() && hasWallJump)
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            hasWallJump = false;
            playerAnimator.SetTrigger("Jump_T");
        }
    }

    private void FixedUpdate()
    {
        if (isOnWall)
        {
            playerRigidBody.velocity = new Vector2(0, -1);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(playerInput * playerSpeed * Time.deltaTime, playerRigidBody.velocity.y);
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            hasWallJump = true;
            isOnWall = true;
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = false;
        }
    }



}

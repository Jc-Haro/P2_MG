using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    private Rigidbody2D playerRigidBody;
    private float playerInput = 0.0f;
    private float playerSpeed = 500.0f;
    private float jumpForce = 7.5f;
    private bool isGrounded = false;
    private bool hasWallJump = false;
    private bool isSecondJumReady = false;
    Vector3 scale;
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
            playerRigidBody.AddForce(new Vector2(1,0) * Time.deltaTime * playerSpeed * playerInput);
  
        }
        else
        {
            playerAnimator.SetBool("Walking_B", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && isSecondJumReady)
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            hasWallJump = false;
            isSecondJumReady = false;
            playerAnimator.SetTrigger("Jump_T");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            hasWallJump = false;
            isSecondJumReady = true;
            playerAnimator.SetTrigger("Jump_T");
        }

        if (Input.GetKeyDown(KeyCode.Space) && hasWallJump)
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            hasWallJump = false;
            isSecondJumReady = false;
            playerAnimator.SetTrigger("Jump_T");
        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            hasWallJump = true;
            isSecondJumReady = false;
        }

    }
}

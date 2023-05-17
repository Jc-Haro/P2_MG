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
    private bool isWallJump = false;
    Vector3 scale;
    private float originalScaleX;
    private Vector2 playerVelocity;

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
        playerVelocity.y -= 9.81f * Time.deltaTime * Time.deltaTime;
        playerRigidBody.velocity = playerVelocity;

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

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || isWallJump))
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            isWallJump = false;
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
            isGrounded = true;
        }

    }
}

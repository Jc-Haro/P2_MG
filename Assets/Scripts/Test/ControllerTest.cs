using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    public Animator playerAnimator;
    private Rigidbody2D playerRigidBody;
    public float playerInput = 0.0f;
    public float playerSpeed;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerInput = Input.GetAxis("Horizontal");
        playerInput = ControlInstance.Instanse.GamePlay.Move.ReadValue<float>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


    }
}

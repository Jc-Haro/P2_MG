using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private bool isOnGround = true;
    private bool isSecondJumReady = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isSecondJumReady)
        {
            playerRigidBody.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            isSecondJumReady = false;

        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigidBody.AddForce(Vector2.zero);
            playerRigidBody.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            isSecondJumReady = true;
            isOnGround = false;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}

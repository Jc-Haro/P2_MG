using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerInput;
    public float playerSpeed = 10;
 
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
       
    }

    private void Update()
    {
        playerInput = Input.GetAxis("Horizontal");
        playerRigidBody.AddForce(Vector2.right * playerInput * playerSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        
        
    }
}

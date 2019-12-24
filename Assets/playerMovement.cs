using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private float speed = 5, jumpspeed = 5;
    
    bool jump = false;
    private bool isMoving;
    private bool isJumpPressed;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        isJumpPressed = Input.GetButtonDown("Jump");
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isJumpPressed)
        {
            Debug.Log("jump");
           rb.velocity = Vector2.up * jumpspeed;
        }

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        //horizontalMove = Input.GetAxis("Horizontal") * speed;

        //rb.velocity.x = horizontalMove * Time.fixedDeltaTime;
        

    }
   // private bool isGrounded()
    

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public GameObject player;
    public GameObject corpse;
    float speed = 5;
    float jumpSpeed = 5;
    float horizontalMove;
    bool isMoving;
    bool isJumpPressed;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        
    }

    private void Update()
    {
        isJumpPressed = Input.GetButtonDown("Jump");
        horizontalMove = Input.GetAxis("Horizontal") * speed;
    }
    private void FixedUpdate()
    {
        if (isJumpPressed && _isGrounded())
        {
            rb.velocity = Vector2.up * jumpSpeed;
        }
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }
    private bool _isGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .15f, layerMask);
        return raycasthit2d.collider != null;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("kolizjaaa");
        if (collision.gameObject.tag == "enemy") { 
            Debug.Log("enemy colission");
            Die();
           

        }
        else if (collision.gameObject.tag == "Checkpoint")
        {
            Debug.Log("player got to checkpoint");
        }
    }
    private void Die()
    {
        rb.velocity = Vector2.zero;

        Vector3 deadPosition;
        deadPosition = transform.position;
        transform.position = Vector2.zero; //coordinates should be specific for every level or something it will be something like Scene.StartCoords

        SpawnCorpse(deadPosition);

    }
    private void SpawnCorpse(Vector2 position)
    {
        GameObject newBox = Instantiate(corpse) as GameObject;
        newBox.transform.position = position;
    }



}
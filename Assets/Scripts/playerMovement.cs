using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;



    [Header("Movement Properties")]
    public float speed = 5;
    public float jumpSpeed = 5;
    //public float coyoteDuration = 0;
    public float maxFallSpeed = 10;
    [Header("to Inputs")]//grab whole header to another file
    bool isJumpPressed;
    float horizontalMove;
    [Header("Player Status")]
    bool isGrounded;
    //bool isJumping;

    PlayerInput input;
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;

    //public GameObject player;
    public GameObject corpse;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        isGrounded = false;
        PhysicsCheck();
        isJumpPressed = Input.GetButtonDown("Jump");
        horizontalMove = Input.GetAxis("Horizontal") * speed;
    }
    private void FixedUpdate()
    {
        if (isJumpPressed && isGrounded)
        {
            Debug.Log("grounded");
            rb.velocity = Vector2.up * jumpSpeed;
        }
       // if (rb.velocity.y < maxFallSpeed) 
            rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        //else
         //   rb.velocity = new Vector2(horizontalMove, maxFallSpeed);

    }
    private void PhysicsCheck()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .15f, layerMask);
        if (raycasthit2d.collider != null)
            isGrounded = true;
    }
   /* private bool _isGrounded()
    {
        Debug.Log("grounded check");
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .15f, layerMask);
        return raycasthit2d.collider != null;
    }*/

   
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

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("coin"))
            Destroy(triggerCollider.gameObject);
            ScoreManager.instance.ChangeScore(+1);
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
        GameObject newCorpse = Instantiate(corpse) as GameObject;
        newCorpse.transform.position = position;
    }



}
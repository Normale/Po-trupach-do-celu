using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [Header("Movement Properties")]
    public float speed = 5;
    public float jumpSpeed = 5;
    //public float coyoteDuration = 0;
    //public float maxFallSpeed = 10;

    [Header("Player Status")]
    public bool isGrounded;
    public event Action Died;
    //bool isJumping;
    [Header("Player Properties")]
    public Rigidbody2D RigidBody;
    public BoxCollider2D Collider;
    public SceneController controller;
    public GameObject corpse;

    public TextMeshProUGUI HeartText;

    int lifes = 10;

    private void Start()
    {
        RigidBody = transform.GetComponent<Rigidbody2D>();
        Collider = transform.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        isGrounded = false;
        PhysicsCheck();
        if (transform.position.y < -15) Die();

        //isJumpPressed = input.GetButtonDown("Jump");
        //horizontalMove = Input.GetAxis("Horizontal") * speed;

    }

    private void FixedUpdate()
    {
        //todo: maximal velocity, in case of slopes need or sth. 
        // if (rb.velocity.y < maxFallSpeed) 
        RigidBody.velocity = new Vector2(controller.Horizontal * speed, RigidBody.velocity.y);
        //else
        //   rb.velocity = new Vector2(horizontalMove, maxFallSpeed);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("grounded");
            RigidBody.velocity = Vector2.up * jumpSpeed;
        }

    }
    private void PhysicsCheck()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0f, Vector2.down, .15f, layerMask);
        if (raycasthit2d.collider != null)
            isGrounded = true;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("enemy colission");
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("coin"))
            Destroy(triggerCollider.gameObject);
        ScoreManager.instance.ChangeScore(+1);
    }

     public void Die()
    {
        Debug.Log(lifes);
        lifes--;
        HeartText.text = lifes.ToString();
        if (lifes == 0) Died();
        RigidBody.velocity = Vector2.zero;
        Vector2 deadPosition;
        deadPosition = transform.position;
        transform.position = findStartingPoint(Vector2.zero);//in future: SceneManager.Scene.startingPoint
        SpawnCorpse(deadPosition);

    }
    private void SpawnCorpse(Vector2 position)
    {
        GameObject newCorpse = Instantiate(corpse) as GameObject;
        newCorpse.transform.position = position;
    }
    private Vector2 findStartingPoint(Vector2 position)
    /// <summary>method <c>_findStartingPoint</c> find the possible position to spawn corpse, to prevent spawning on collisions</summary>
    {
        int i = 5;
        while (Physics2D.BoxCast(position, Collider.bounds.size, 0f, Vector2.down, .1f).collider != null)
        {
            Debug.Log("collision at starting point");
            position.y += i;
        }
        return position;
    }

}
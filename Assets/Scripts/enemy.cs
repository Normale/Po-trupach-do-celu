using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    [Header("Enemy parameters")]
    public float speed = 1;
    int direction = 1;
    float timer = 0;
    public float changeTime = 4;
    void Start()
    {

        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

            timer += Time.deltaTime;
            if (timer > changeTime)
            {
                direction = direction == 1 ? -1 : 1;
                speed = speed >= 20 ? 20 : (speed + 0.5f);
                timer = 0f;
            }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, 0);
    }
}

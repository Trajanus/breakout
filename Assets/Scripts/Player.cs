using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float paddleMovementDamperSize = .2f;
    public float moveSpeed = .1F;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        Vector3 gameWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xDiff = rb2D.position.x - gameWorldMousePosition.x;

        if (xDiff > paddleMovementDamperSize)
        {
            rb2D.MovePosition(new Vector2(rb2D.position.x - moveSpeed, rb2D.position.y));
            //rb2D.AddForce(new Vector2(-moveSpeed, 0));
        }
        else if (xDiff < -paddleMovementDamperSize)
        {
            rb2D.MovePosition(new Vector2(rb2D.position.x + moveSpeed, rb2D.position.y));
            //rb2D.AddForce(new Vector2(moveSpeed, 0));
        }
        else
        {
            //rb2D.velocity = Vector2.zero;
        }
    }
}

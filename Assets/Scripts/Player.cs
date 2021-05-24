using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float paddleMovementDamperSize = .2f;
    public float moveSpeed = .1F;
    public double LeftLevelEdge = -4.4;
    public double RightLevelEdge = 11.2;

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
            Vector2 move = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
            if (move.x > LeftLevelEdge)
            {
                rb2D.MovePosition(move);
            }
        }
        else if (xDiff < -paddleMovementDamperSize)
        {
            Vector2 move = transform.position + Vector3.right * moveSpeed * Time.deltaTime;
            if (move.x < RightLevelEdge)
            {
                rb2D.MovePosition(move);
            }
        }
    }
}

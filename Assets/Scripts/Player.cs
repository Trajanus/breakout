using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float paddleMovementDamperSize = .2f;
    public float moveSpeed = .1F;

    public bool hasMagneticPowerup = false;

    public static Player instance = null;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
            if (move.x > GameManager.instance.LeftLevelEdge)
            {
                rb2D.MovePosition(move);
            }
        }
        else if (xDiff < -paddleMovementDamperSize)
        {
            Vector2 move = transform.position + Vector3.right * moveSpeed * Time.deltaTime;
            if (move.x < GameManager.instance.RightLevelEdge)
            {
                rb2D.MovePosition(move);
            }
        }

        if (hasMagneticPowerup && Input.GetKey(KeyCode.Space))
        {
            GameObject ball = GameObject.Find(Ball.Name);
            Vector3 playerBallVector = Vector3.Lerp(transform.position, ball.transform.position, 0f);

            var ballRb2d = ball.GetComponent<Rigidbody2D>();
            ballRb2d.MovePosition(playerBallVector);
        }

    }
}

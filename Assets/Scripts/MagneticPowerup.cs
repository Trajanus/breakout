using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticPowerup : MonoBehaviour
{
    public AudioClip collisionSound;
    private Rigidbody2D rb2D;
    private BoxCollider2D collider;

    public double LeftLevelEdge = -4.4;
    public double RightLevelEdge = 11.2;

    private int moveSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 move = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
        if ((move.x - collider.size.x) < GameManager.instance.LeftLevelEdge || 
            (move.x + collider.size.x) > GameManager.instance.RightLevelEdge)
        {
            moveSpeed = moveSpeed * -1;
        }
        rb2D.MovePosition(move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == Ball.Name)
        {
            SoundManager.instance.PlaySingle(collisionSound);
            Destroy(gameObject);
            Player.instance.hasMagneticPowerup = true;
        }
    }
}

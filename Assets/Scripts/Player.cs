using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float walkVelocity;
    [SerializeField] float jumpHeight;

    Rigidbody2D rigidbody;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalFactor = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(walkVelocity * horizontalFactor, rigidbody.velocity.y);


        if (!isJumping && !Mathf.Approximately(0f, Input.GetAxis("Jump")))
        {
            isJumping = true;
            rigidbody.velocity = new Vector2(
                rigidbody.velocity.x,
                Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y * rigidbody.gravityScale)));
        }

        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Planks")))
        {
            isJumping = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float walkVelocity;
    [SerializeField] float walkAcceleration;
    [SerializeField] float walkDeceleration;
    [SerializeField] float airAcceleration;
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

        var acceleration = isJumping ? airAcceleration : walkAcceleration;
        var deceleration = isJumping ? 0f : walkDeceleration;

        if (!Mathf.Approximately(0f, horizontalFactor))
        {
            var velocity = Mathf.MoveTowards(
                rigidbody.velocity.x,
                walkVelocity * horizontalFactor,
                acceleration * Time.deltaTime);
            rigidbody.velocity = new Vector2(velocity, rigidbody.velocity.y);
        }
        else
        {
            var velocity = Mathf.MoveTowards(
                rigidbody.velocity.x,
                0f,
                deceleration * Time.deltaTime);
            rigidbody.velocity = new Vector2(velocity, rigidbody.velocity.y);
        }

        if (!isJumping && !Mathf.Approximately(0f, Input.GetAxis("Jump")))
        {
            isJumping = true;
            rigidbody.velocity = new Vector2(
                rigidbody.velocity.x,
                Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
        }

        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Planks")))
        {
            isJumping = false;
        }
    }
}

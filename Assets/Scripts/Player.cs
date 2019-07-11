using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float walkVelocity;
    [SerializeField] float jumpHeight;
    [SerializeField] float lowerYBound;
    [SerializeField] float upperYBound;

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
        HandleMove();

        if (!isJumping && !Mathf.Approximately(0f, Input.GetAxis("Jump")))
        {
            HandleJump();
        }
    }

    private void HandleMove()
    {
        var horizontalFactor = Input.GetAxis("Horizontal");

        rigidbody.velocity
            = new Vector2(walkVelocity * horizontalFactor, rigidbody.velocity.y);
    }

    private void HandleJump()
    {
        isJumping = true;
        var gravityFactor = Physics2D.gravity.y * rigidbody.gravityScale;
        var isOverLowerBound = transform.position.y > lowerYBound;
        var height = isOverLowerBound ? (upperYBound - transform.position.y) : jumpHeight;
        Debug.Log("height=" + height + "  distanceToYBound=" + isOverLowerBound);
        rigidbody.velocity = new Vector2(
            rigidbody.velocity.x,
            Mathf.Sqrt(2 * height * Mathf.Abs(gravityFactor)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Planks")
        {
            isJumping = false;
        }
    }
}

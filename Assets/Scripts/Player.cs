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

        rigidbody.velocity
            = new Vector2(walkVelocity * horizontalFactor, rigidbody.velocity.y);


        if (!isJumping && !Mathf.Approximately(0f, Input.GetAxis("Jump")))
        {
            isJumping = true;
            var gravityFactor = Physics2D.gravity.y * rigidbody.gravityScale;
            rigidbody.velocity = new Vector2(
                rigidbody.velocity.x,
                Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(gravityFactor)));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Planks")
        {
            isJumping = false;
        }
    }
}

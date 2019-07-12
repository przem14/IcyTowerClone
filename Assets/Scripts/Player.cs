using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerJumpLocked : UnityEvent<float, float> { }

public class Player : MonoBehaviour
{
    [SerializeField] float walkVelocity;
    [SerializeField] float jumpHeight;
    [SerializeField] float lowerYBound;
    [SerializeField] float upperYBound;

    [Header("Jumping related events")]
    [SerializeField] PlayerJumpLocked playerLockedOnY;
    [SerializeField] UnityEvent playerUnlockedOnY;


    Rigidbody2D rigidbody;
    bool isJumping = false;
    bool isLocked = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();

        if (ShouldLockPositionY())
        {
            StartCoroutine(OnLockPlayerJumping());
        }

        if (ShouldJump())
        {
            HandleJump();
        }
    }

    private bool ShouldJump()
    {
        return !isJumping && Input.GetKeyDown(KeyCode.Space);
    }

    private bool ShouldLockPositionY()
    {
        return isJumping && transform.position.y > upperYBound;
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
        rigidbody.velocity = new Vector2(
            rigidbody.velocity.x,
            Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(gravityFactor)));
    }

    private IEnumerator OnLockPlayerJumping()
    {
        if (!isLocked)
        {
            var lockTime = LockPlayerPositionY();
            yield return new WaitForSeconds(lockTime);
            UnlockPlayerPositionY();
        }
    }

    private void UnlockPlayerPositionY()
    {
        rigidbody.constraints &= (~RigidbodyConstraints2D.FreezePositionY);
    }

    private float LockPlayerPositionY()
    {
        isLocked = true;
        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionY;
        var lockTime = Mathf.Abs(
            (rigidbody.velocity.y) / (rigidbody.gravityScale * Physics2D.gravity.y));
        playerLockedOnY.Invoke(rigidbody.velocity.y, lockTime);
        return lockTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Planks")
        {
            isJumping = false;
            isLocked = false;
        }
    }
}

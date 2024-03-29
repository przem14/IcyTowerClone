﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerJumpLocked : UnityEvent<float, float> { }

public class Player : MonoBehaviour
{
    const string PLANKS_LAYER_NAME = "Planks";

    [SerializeField] float walkVelocity;
    [SerializeField] float idleJumpHeight;
	[SerializeField] float bonusJump;
    [SerializeField] float lowerYBound;
    [SerializeField] float upperYBound;

    [Header("Jumping related events")]
    [SerializeField] PlayerJumpLocked playerLockedOnY;
    [SerializeField] UnityEvent landedOnPlank;


    Rigidbody2D rigidbody;
    CapsuleCollider2D plankDetector;
    bool isJumping = false;
    bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        plankDetector = GetComponent<CapsuleCollider2D>();
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

        ChangeFaceDirection(horizontalFactor);
    }

    private void ChangeFaceDirection(float horizontalFactor)
    {
        var scale = transform.localScale;
        scale.x = Mathf.Sign(horizontalFactor);
        transform.localScale = scale;
    }

    private void HandleJump()
    {
        if (!plankDetector.IsTouchingLayers(LayerMask.GetMask(PLANKS_LAYER_NAME))) return;

        isJumping = true;
		var bonusFactor = Mathf.Abs(Input.GetAxis("Horizontal"));
        var gravityFactor = Physics2D.gravity.y * rigidbody.gravityScale;
        var jumpHeight = (idleJumpHeight + bonusFactor * bonusJump);
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
        if (!IsOnPlanksLayer(collision.collider)) return;

        isJumping = false;
        isLocked = false;
        landedOnPlank.Invoke();
    }

    private bool IsOnPlanksLayer(Collider2D collision)
    {
        return LayerMask.LayerToName(collision.gameObject.layer) == PLANKS_LAYER_NAME;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrollingObjectsController : MonoBehaviour
{
    [SerializeField] Scrolling[] scrollableObjects;
    [SerializeField] float defaultSpeed = 1f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] UnityEvent onScrollingStarted; 

    float currentSpeed = 0f;
    float gravity;

    private void Update()
    {
        DecelerateSpeed();
    }

    private void DecelerateSpeed()
    {
        if (Mathf.Approximately(currentSpeed, 0f) ||
            Mathf.Approximately(currentSpeed, defaultSpeed)) return;
        var deceleration = gravity * Time.deltaTime;
        SetObjectsSpeed(Mathf.Clamp(currentSpeed - deceleration, defaultSpeed, currentSpeed));
    }

    private void SetObjectsSpeed(float speed)
    {
        if (Mathf.Approximately(currentSpeed, 0f) && speed > 0f)
        {
            onScrollingStarted.Invoke();
        }

        currentSpeed = speed;
        foreach (var obj in scrollableObjects)
        {
            obj.SetScrollingSpeed(speed);
        }
    }

    public void OnPlayerLockY(float playerVelocity, float lockTime)
    {
        SetObjectsSpeed(defaultSpeed + playerVelocity);
        gravity = (playerVelocity / lockTime);
    }

    public void IncreaseSpeed()
    {
        defaultSpeed += acceleration;
        if (defaultSpeed > currentSpeed)
        {
            SetObjectsSpeed(defaultSpeed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [Header("Scrolling params")]
    [SerializeField] bool reverseDirection = false;
    [Range(0f, 100f)] [SerializeField] float scrollingSpeed = 1f;

    Vector2 direction = new Vector2(0, -1f);

    void Start()
    {
        if (reverseDirection) direction.y *= -1f;
    }

    private void Update()
    {
        MoveChildren();
    }

    private void MoveChildren()
    {
        var velocity = direction * scrollingSpeed;

        for (int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            var rigidbody = child.GetComponent<Rigidbody2D>();

            if (rigidbody)
            {
                rigidbody.velocity = velocity;
            }
            else
            {
                transform.GetChild(i).Translate(velocity * Time.deltaTime);
            }
            
        }
    }

    public void SetScrollingSpeed(float speed)
    {
        scrollingSpeed = speed;
    }
}

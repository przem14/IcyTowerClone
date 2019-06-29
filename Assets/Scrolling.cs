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

    void Update()
    {
        MoveChildren();
    }

    private void MoveChildren()
    {
        var translation = direction * scrollingSpeed * Time.deltaTime;
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).Translate(translation);
        }
    }
}

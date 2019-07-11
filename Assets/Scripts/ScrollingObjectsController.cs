using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectsController : MonoBehaviour
{
    [SerializeField] Scrolling[] scrollableObjects;

    [SerializeField] Player player;
    [SerializeField] float yBound;

    [SerializeField] float defaultSpeed = 1f;

    float lastPlayerPositionY;
    float currentScrollingSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPositionY = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var playerPositionY = player.transform.position.y;
        var diffY = playerPositionY - lastPlayerPositionY;

        if (diffY > 0f && playerPositionY > yBound)
        {
            currentScrollingSpeed = diffY / Time.deltaTime;
            Debug.Log("Bound exceeded - speed: " + currentScrollingSpeed.ToString());
        }
        else
        {
            currentScrollingSpeed = defaultSpeed;
            Debug.Log("Normal speed = " + currentScrollingSpeed.ToString());
        }

        foreach (var s in scrollableObjects)
        {
            s.SetScrollingSpeed(currentScrollingSpeed);
        }

        lastPlayerPositionY = playerPositionY;
    }
}

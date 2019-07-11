using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanksCollisionController : MonoBehaviour
{
    [SerializeField] GameObject planksParent;

    BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        var player = FindObjectOfType<Player>();
        playerCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var colliders
            = planksParent.transform.GetComponentsInChildren<Collider2D>();
        foreach (var plankCollider in colliders)
        {
            var isPlayerAbovePlank
                = playerCollider.bounds.min.y > plankCollider.bounds.max.y;
            plankCollider.isTrigger = !isPlayerAbovePlank;
        }
    }
}

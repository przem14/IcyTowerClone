using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShredder : MonoBehaviour
{
    int numberOfRemovedPlanks = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isPlank = LayerMask.LayerToName(collision.gameObject.layer) == "Planks";
        numberOfRemovedPlanks += isPlank ? 1 : 0;
        Destroy(collision.gameObject);
    }

    public int GetNumberOfRemovedPlanks()
    {
        return numberOfRemovedPlanks;
    }
}

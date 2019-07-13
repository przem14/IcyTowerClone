using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] ObjectShredder objectShredder;
    [SerializeField] GameObject planksParent;
    [SerializeField] Player player;

    int score = 0;

    // Update is called once per frame
    void Update()
    {
        var planksBelowCount = 0;
        var playerY = player.transform. position.y;
        var childCount = planksParent.transform.childCount;
        for (var i = 0; i < childCount; ++i)
        {
            var plankY = planksParent.transform.GetChild(i).position.y;
            Debug.Log("plankY=" + plankY + " playerY=" + playerY);
            if (plankY < playerY) planksBelowCount++;
        }
        score = objectShredder.GetNumberOfRemovedPlanks() + planksBelowCount;
    }

    public int GetScore()
    {
        return score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] ObjectShredder objectShredder;
    [SerializeField] GameObject planksParent;
    [SerializeField] Player player;

    int score = 0;

    public void UpdateScore()
    {
        var planksBelowCount = 0;
        var playerY = player.transform. position.y;
        var childCount = planksParent.transform.childCount;
        for (var i = 0; i < childCount; ++i)
        {
            var plankY = planksParent.transform.GetChild(i).position.y;
            if (plankY < playerY) planksBelowCount++;
        }
        score = Mathf.Max(objectShredder.GetNumberOfRemovedPlanks() + planksBelowCount, score);
    }

    public int GetScore()
    {
        return score;
    }
}

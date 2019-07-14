using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class OnBonusGainedEvent : UnityEvent<string> { }

public class ScoreCounter : MonoBehaviour
{
    const int MINIMUM_BONUS_TO_SHOW_MSG = 10;

    [SerializeField] ObjectShredder objectShredder;
    [SerializeField] GameObject planksParent;
    [SerializeField] Player player;
    [SerializeField] BonusPointsTimer bonusPointsTimer;
    [SerializeField] OnBonusGainedEvent onBonusGained;

    int score = 0;
    int bonusScore = 0;
    int currentBonusScore = 0;

    private void Start()
    {
        if (bonusPointsTimer)
        {
            bonusPointsTimer.onTimeExpired.AddListener(AddBonus);
        }
    }

    public void UpdateScore()
    {
        var planksBelowCount = 0;
        var playerY = player.transform.position.y;
        var childCount = planksParent.transform.childCount;
        for (var i = 0; i < childCount; ++i)
        {
            var plankY = planksParent.transform.GetChild(i).position.y;
            if (plankY < playerY) planksBelowCount++;
        }

        var newScore = Mathf.Max(objectShredder.GetNumberOfRemovedPlanks() + planksBelowCount, score);
        UpdateBonusPoints(newScore);
        score = newScore;
    }

    private void UpdateBonusPoints(int newScore)
    {
        if (newScore > score + 1)
        {
            currentBonusScore += newScore - score;
            UpdateBonusTimer();
        }
        else
        {
            AddBonus();
        }
    }

    private void UpdateBonusTimer()
    {
        if (!bonusPointsTimer) return;

        if (bonusPointsTimer.IsActive())
        {
            bonusPointsTimer.ProlongTimer();
        }
        else
        {
            bonusPointsTimer.StartTimer();
        }
    }

    private string GetBonusMessae()
    {
        if (currentBonusScore < 50)
        {
            return "Sweet!";
        }
        else if (currentBonusScore < 200)
        {
            return "Great!";
        }

        return "Amazing!";
    }

    private void AddBonus()
    {
        if (currentBonusScore > MINIMUM_BONUS_TO_SHOW_MSG)
        {
            onBonusGained.Invoke(GetBonusMessae());
        }
        bonusScore += currentBonusScore;
        currentBonusScore = 0;

        bonusPointsTimer.StopTimer();
    }

    public int GetScore()
    {
        return score + bonusScore;
    }
}

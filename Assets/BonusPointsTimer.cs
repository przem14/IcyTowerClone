using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BonusPointsTimer : MonoBehaviour
{
    [SerializeField] float bonusTime = 4f;
    public UnityEvent onTimeExpired;

    Slider slider;

    float currentTime = 0f;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            currentTime = Mathf.Clamp(currentTime - Time.deltaTime, 0, bonusTime);
            slider.value = currentTime / bonusTime;
            if (Mathf.Approximately(currentTime, 0f))
            {
                onTimeExpired.Invoke();
                StopTimer();
            }
        }
    }

    public void StartTimer()
    {
        isActive = true;
        currentTime = bonusTime;
        slider.value = 1f;
    }

    public void StopTimer()
    {
        isActive = false;
        currentTime = 0f;
        slider.value = 0f;
    }

    public void ProlongTimer()
    {
        if (!isActive) return;

        currentTime = bonusTime;
        slider.value = 1f;
    }

    public bool IsActive()
    {
        return isActive;
    }
}

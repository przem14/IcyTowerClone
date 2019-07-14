using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockController : MonoBehaviour
{
    [SerializeField] UnityEvent onMinutePassed;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartClock()
    {
        animator.SetBool("isStarted", true);
    }



    public void OnMinutePassed()
    {
        onMinutePassed.Invoke();
    }
}

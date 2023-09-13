using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int number;
    private float timeRemaining;
    private float maxTime;

    #region Events
    public delegate void TargetFailureHandler(Target target);
    public delegate void TargetSuccessHandler(Target target);

    public static event TargetFailureHandler onAnyTargetFailure;
    public static event TargetSuccessHandler onAnyTargetSuccess;

    public event TargetFailureHandler onTargetFailure;
    public event TargetSuccessHandler onTargetSuccess;
    #endregion

    private Movement[] movements;
    public void Setup(int number, float maxTime)
    {
        this.number = number;
        this.maxTime = maxTime;
        timeRemaining = this.maxTime;

        movements = GetComponents<Movement>();

    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Failure();
        }

        if(movements.Length > 0)
        {
            foreach(Movement movement in movements)
            {
                movement.Run();
            }
        }

    }
    public void Failure()
    {
        onAnyTargetFailure?.Invoke(this);
        onTargetFailure?.Invoke(this);
        Destroy(gameObject);
    }

    public void Success()
    {
        onAnyTargetSuccess?.Invoke(this);
        onTargetSuccess?.Invoke(this);
        Destroy(gameObject);
    }

    public int Number => number;
    public float TimeRemaining => timeRemaining;
    public float MaxTime => maxTime;



}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    protected int number;
    protected float timeRemaining;
    protected float maxTime;

    #region Events
    public delegate void TargetFailureHandler(Target target);
    public delegate void TargetSuccessHandler(Target target);

    public static event TargetFailureHandler onAnyTargetFailure;
    public static event TargetSuccessHandler onAnyTargetSuccess;

    public event TargetFailureHandler onTargetFailure;
    public event TargetSuccessHandler onTargetSuccess;
    #endregion
    public abstract void Setup(int number, float maxTime);

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

    public int Number { get { return number; } }
    public float TimeRemaining => timeRemaining;
    public float MaxTime => maxTime;



}

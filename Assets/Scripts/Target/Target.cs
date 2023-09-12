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
    [SerializeField] private TextMeshPro textMeshPro;

    #region Events
    public delegate void TargetFailureHandler(Target target);
    public delegate void TargetSuccessHandler(Target target);

    public static event TargetFailureHandler onAnyTargetFailure;
    public static event TargetSuccessHandler onAnyTargetSuccess;

    public event TargetFailureHandler onTargetFailure;
    public event TargetSuccessHandler onTargetSuccess;
    #endregion
    public void Setup(int number , float maxTimer , Color? color = null)
    {
        this.number = number;
        maxTime = maxTimer;
        timeRemaining = maxTime;
        textMeshPro.text = number.ToString();

        if(color.HasValue)
        {
            GetComponent<SpriteRenderer>().color = color.Value;
        }
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Failure();
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

    public int Number { get { return number; } }
    public float TimeRemaining => timeRemaining;
    public float MaxTime => maxTime;

}

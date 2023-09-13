using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NormalTarget : Target
{
    public override void Setup(int number, float maxTime)
    {
        this.number = number;
        this.maxTime = maxTime;
        timeRemaining = this.maxTime;

    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Failure();
        }
    }
}

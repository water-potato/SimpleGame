using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LinearTarget : Target
{
    Vector3 movementVector;
    float speed;
    public override void Setup(int number, float maxTime)
    {
        this.number = number;
        this.maxTime = maxTime;
        timeRemaining = this.maxTime;

        movementVector = Random.insideUnitCircle.normalized;
        speed = Random.Range(2f, 5f);
    }

    private void Update()
    {

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Failure();
        }

        transform.position += movementVector*speed *Time.deltaTime;
        CheckBorder();
    }

    private void CheckBorder()
    {
        if(transform.position.x > Managers.Vector.MaxX || transform.position.x < Managers.Vector.MinX)
        {
            movementVector.x *= -1;
        }
        if (transform.position.y > Managers.Vector.MaxY || transform.position.y < Managers.Vector.MinY)
        {
            movementVector.y *= -1;
        }

    }
}

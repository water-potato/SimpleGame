using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : Movement
{
    float xsign;
    float ysign;
    bool isVertical;
    float speed = 4f;
    float timer;
    Vector3 movementVector;
    private void Start()
    {
        xsign = transform.position.x > 0 ? 1f : -1f;
        ysign = transform.position.y > 0 ? 1f : -1f;
        isVertical = Random.Range(0, 2) == 1;
    }
    public override void Run()
    {
        timer += Time.deltaTime;
        transform.position += movementVector * speed * Time.deltaTime;
        SetMovementVector();

        Vector3 testVector = transform.position + movementVector * 0.2f;

        if (testVector.x > Managers.Vector.MaxX || testVector.x < Managers.Vector.MinX)
        {
            xsign *= -1;
        }
        if (testVector.y > Managers.Vector.MaxY || testVector.y < Managers.Vector.MinY)
        {
            ysign *= -1;
        }
    }

    private void SetMovementVector()
    {
        float sinValue = 1.5f;
        float x, y;
        if(isVertical)
        {
            x = xsign;
            y = ysign * Mathf.Sin(timer * Mathf.PI * 3f) * sinValue;
        }
        else
        {
            x = xsign * Mathf.Sin(timer * Mathf.PI * 3f) * sinValue;
            y = ysign;

        }

        movementVector = new Vector3 (x, y, 0);

    }

}

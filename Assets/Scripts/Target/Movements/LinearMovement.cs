using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LinearMovement : Movement 
{

    private Vector3 movementVector;
    private float speed;

    private void Start()
    {
        movementVector = Random.insideUnitCircle.normalized;
        speed = Random.Range(2f, 10f);
    }
    public override void Run()
    {
        transform.position += movementVector * speed * Time.deltaTime;

        if (transform.position.x > Managers.Vector.MaxX || transform.position.x < Managers.Vector.MinX)
        {
            movementVector.x *= -1;
        }
        if (transform.position.y > Managers.Vector.MaxY || transform.position.y < Managers.Vector.MinY)
        {
            movementVector.y *= -1;
        }
    }
}

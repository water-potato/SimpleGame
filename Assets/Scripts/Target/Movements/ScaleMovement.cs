using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMovement : Movement
{
    float speed;
    private void Start()
    {
       int ran = Random.Range(0, 2);

        if(ran == 0)
        {
            // 작아지기
            speed = -.3f;
        }
        else
        {
            speed = 3f;
        }


    }


    public override void Run()
    {
        transform.localScale += Vector3.one * speed * Time.deltaTime;
    }
}

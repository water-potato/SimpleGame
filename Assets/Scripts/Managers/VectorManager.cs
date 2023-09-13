using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorManager
{
    private Vector2 minWorldPoint;
    private Vector2 maxWorldPoint;

    private Vector2 minVector;
    private Vector2 maxVector;


    public void Setup()
    {
        float border = .5f;

        minWorldPoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxWorldPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minVector = minWorldPoint + new Vector2(border, border);
        maxVector = maxWorldPoint - new Vector2(border, border);
    }

    public Vector2 GetRandomPositionInViewPort()
    {
        float randomX = Random.Range(minVector.x , maxVector.x);
        float randomY = Random.Range(minVector.y , maxVector.y);


        return new Vector2(randomX, randomY);
    }


    public Vector2 MinVector => minVector;
    public Vector2 MaxVector => maxVector;
    public float MinX => minVector.x;
    public float MaxX => maxVector.x;
    public float MinY => minVector.y;
    public float MaxY => maxVector.y;
}

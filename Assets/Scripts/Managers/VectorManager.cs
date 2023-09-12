using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorManager
{
    private Vector2 minVector;
    private Vector2 maxVector;

    public void Setup()
    {
        minVector = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxVector = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    public Vector2 GetRandomPositionInViewPort()
    {
        float randomX = Random.Range(minVector.x, maxVector.x);
        float randomY = Random.Range(minVector.y, maxVector.y);

        return new Vector2(randomX, randomY);
    }

}

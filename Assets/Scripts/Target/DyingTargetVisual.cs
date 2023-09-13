using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class DyingTargetVisual : MonoBehaviour
{
    private Color startColor;
    private Color endColor;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float destroyTimer;
    private float maxTimer;

    private SpriteRenderer spriteRenderer;

    [SerializeField] TextMeshPro nameText;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Setup();
    }

    float timer = 1f;
    public void Setup()
    {
        // About Color
        Color newColor = spriteRenderer.color;
        startColor = newColor;
        newColor.a = 0;
        endColor = newColor;

        // About Position
        float dist = 3f;
        startPosition = transform.position;
        endPosition = transform.position + Vector3.down*dist;

        nameText.text = GetRandomName();

        maxTimer = timer;
        destroyTimer = 0f;
    }

    private void Update()
    {
        if (destroyTimer >= maxTimer)
        {
            Destroy(gameObject);
            return;
        }
        destroyTimer += Time.deltaTime;

        //Color Lerp

        float t = destroyTimer / maxTimer;
        t = Util.SinLerp(t);
        spriteRenderer.color = Color.Lerp(startColor, endColor, t);

        // Transform Lerp
        float q = destroyTimer / maxTimer;
        q = Util.SmoothIncrease(q);
        transform.position = Vector3.Lerp(startPosition, endPosition, q);

    }


    private string GetRandomName()
    {
        List<string> dyingTargetNames = new List<string> { "NOPE", "LOL", "Oops","EZ"};


        int random = UnityEngine.Random.Range(0, dyingTargetNames.Count);

        return dyingTargetNames[random];
    }
}
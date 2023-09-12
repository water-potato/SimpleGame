using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class DyingTargetUI : MonoBehaviour
{
    private Color startColor;
    private Color endColor;

    private float destroyTimer;
    private float maxTimer;

    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Setup();
    }

    float timer = .5f;
    public void Setup()
    {
        Color newColor = spriteRenderer.color;
        startColor = newColor;
        newColor.a = 0;
        endColor = newColor;

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

        float t = destroyTimer / maxTimer;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        spriteRenderer.color = Color.Lerp(startColor, endColor, t);
    }

}

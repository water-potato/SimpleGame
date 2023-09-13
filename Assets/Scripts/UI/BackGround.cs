using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    //실패하면 진해지고 성공하면 연해짐
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Color endColor;
    private Color startColor = Color.white;
    private float t;
    private float unitValue;
    private int maxFailureCount;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void Init()
    {
        t = 0;
        maxFailureCount = GameBuilder.Instance.MaxFailureCount;
        unitValue = 1f / maxFailureCount;

        ChangeColor();
    }

    private void ChangeColor()
    {
        spriteRenderer.color = Color.Lerp(startColor, endColor, Mathf.Min(t, 1f));
        if(t > 1f)
        {
            GameBuilder.Instance.GameOver();
        }
    }

    public void OnFailure()
    {
        t = t + unitValue;
        ChangeColor();
    }

    public void OnSuccess()
    {
        float recoveryRate = .2f;
        t = Mathf.Max(t - unitValue * recoveryRate, 0);
        ChangeColor();
    }

    public void ResetColor()
    {
        spriteRenderer.color = startColor;
    }
}

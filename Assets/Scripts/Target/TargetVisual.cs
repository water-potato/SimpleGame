using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetVisual : MonoBehaviour
{
    Target target;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro textMeshPro;

    [Header("After Failure")]
    [SerializeField] private GameObject dyingTargetPrefab;
    

    private Color startColor;
    private Color endColor;

    private void OnEnable()
    {
        target = GetComponent<Target>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupColor();


        target.onTargetFailure -= Target_onTargetFailure;
        target.onTargetFailure += Target_onTargetFailure;
    }

    private void Target_onTargetFailure(Target target)
    {
        SummonDyingTarget();
    }
    private void SummonDyingTarget()
    {
        Instantiate(dyingTargetPrefab, transform.position , Quaternion.identity);
    }


    private void SetupColor()
    {
        startColor = spriteRenderer.color;
        startColor.a = 0;
        endColor = spriteRenderer.color;
        endColor.a = 1;

        spriteRenderer.color = startColor;

    }

    private void Update()
    {
        textMeshPro.text = target.Number.ToString();

        float t = Mathf.Min(target.TimeRemaining / target.MaxTime, 1f);
        t = Util.SinLerp(t);
        Color newColor = Color.Lerp(startColor, endColor, t);

        spriteRenderer.color = newColor;
    }

}

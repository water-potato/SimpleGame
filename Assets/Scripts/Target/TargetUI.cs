using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetUI : MonoBehaviour
{
    Target target;
    private SpriteRenderer spriteRenderer;

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
        SummonDyingTarget(target);
    }
    private void SummonDyingTarget(Target target)
    {
        Instantiate(dyingTargetPrefab, transform.position , Quaternion.identity);
        Debug.Log("Dying");
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
            float t = Mathf.Min(target.TimeRemaining / target.MaxTime , 1f);
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            Color newColor = Color.Lerp(startColor, endColor, t);

            spriteRenderer.color = newColor;
    }

}

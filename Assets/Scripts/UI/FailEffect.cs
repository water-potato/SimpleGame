using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailEffect : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(EffectOnEnable());
    }

    IEnumerator EffectOnEnable()
    {
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set;}

    [SerializeField]
    private GameObject failEffect;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        Target.onAnyTargetFailure += OnAnyTargetFailure;
    }

    private void OnAnyTargetFailure(Target target)
    {
        failEffect.SetActive(true);
    }
}

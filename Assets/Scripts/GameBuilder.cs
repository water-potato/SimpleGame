using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    private int currentNumber;
    private float targetTime = 2f;

    [Header("Generate")]
    [SerializeField] private float maxGenerateTime;
    [SerializeField] private float minGenerateTime = .3f;

    [SerializeField] private float generateTime;

    [SerializeField] private int successCount;
    private int failureCount;
    private int maxFailure;

    [Header("Game Over")]
    [SerializeField] private bool canOver;
    private bool isOver;

    private float testTimer;
    // Start is called before the first frame update
    void Start()
    {
        ResetGameBuilder();

    }

    void Update()
    {
        if (canOver && isOver)
            return;

        testTimer += Time.deltaTime;

        if(testTimer > generateTime) 
        {
            GenerateTarget();
            testTimer = 0;
        }

        if(Managers.Instance.Input.CheckTargetForPC(out Target target))
        {
            target.Success();
        }
    }

    private void ResetGameBuilder()
    {
        isOver = false;
        currentNumber = 1;
        
        generateTime = maxGenerateTime;
        Target.onAnyTargetFailure += onTargetFailure;
        Target.onAnyTargetSuccess += onTargetSuccess;

    }

    private void GenerateTarget()
    {
        Managers.Instance.Target.GenerateTarget(currentNumber++, targetTime, targetPrefab ,GetRandomPosition());

    }

    private void onTargetSuccess(Target target)
    {
        int lessTimeLimit = 5;
        successCount++;
        if(successCount >= lessTimeLimit)
        {
            successCount = 0;
            generateTime = Mathf.Max(generateTime - 0.1f, minGenerateTime);
        }

    }
    private void onTargetFailure(Target target)
    {
        failureCount++;

        if(canOver && failureCount >= maxFailure)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Target.onAnyTargetFailure -= onTargetFailure;
        Target.onAnyTargetSuccess -= onTargetSuccess;

        isOver = true;
    }

    private Vector2 GetRandomPosition() => Managers.Instance.Vector.GetRandomPositionInViewPort();
}

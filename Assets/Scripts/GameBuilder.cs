using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    [Header("Target Prefabs")]
    [SerializeField] private GameObject normalTargetPrefab;
    [SerializeField] private GameObject linearTargetPrefab;
    private int currentNumber;
    
    [SerializeField] private float targetLivingTime = 2f;

    [Header("Generate")]
    [SerializeField] private float maxGenperiod;
    [SerializeField] private float minGenperiod = .5f;

    [Header("ReadOnly")]
    [SerializeField] private float generateTime;
    [SerializeField] private int successCount;

    private float periodTime = 5f;


    [Header("Game Over")]
    [SerializeField] private bool canOver;
    private int maxFailureCount;
    private int failureCount;
    private bool isOver;

    private float genTimer;
    private float pTimer;
    // Start is called before the first frame update
    void Start()
    {
        ResetGameBuilder();



    }

    void Update()
    {
        // Draw Border
        Debug.DrawLine(Managers.Vector.MinVector, new Vector2(Managers.Vector.MinX, Managers.Vector.MaxY) , Color.red);
        Debug.DrawLine(Managers.Vector.MinVector, new Vector2(Managers.Vector.MaxX, Managers.Vector.MinY), Color.red);
        Debug.DrawLine(Managers.Vector.MaxVector, new Vector2(Managers.Vector.MinX, Managers.Vector.MaxY), Color.red);
        Debug.DrawLine(Managers.Vector.MaxVector, new Vector2(Managers.Vector.MaxX, Managers.Vector.MinY), Color.red);

        if (canOver && isOver)
            return;

        genTimer += Time.deltaTime;
        pTimer += Time.deltaTime;

        if (genTimer > generateTime) 
        {
            GenerateTarget();
            genTimer = 0;
        }

        if(pTimer >= periodTime)
        {
            generateTime = Mathf.Max(generateTime - 0.2f, minGenperiod);
            pTimer = 0f;
        }

        if(Managers.Input.CheckTargetForPC(out Target target))
        {
            target.Success();
        }
    }

    private void ResetGameBuilder()
    {
        isOver = false;
        currentNumber = 1;
        
        generateTime = maxGenperiod;
        Target.onAnyTargetFailure += onAnyTargetFailure;
        Target.onAnyTargetSuccess += onAnyTargetSuccess;

    }

    private void GenerateTarget()
    {
        Managers.Target.GenerateTarget(currentNumber++, targetLivingTime, normalTargetPrefab ,GetRandomPosition());

    }

    private void onAnyTargetSuccess(Target target)
    {
        successCount++;
    }
    private void onAnyTargetFailure(Target target)
    {
        failureCount++;

        if(canOver && failureCount >= maxFailureCount)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Target.onAnyTargetFailure -= onAnyTargetFailure;
        Target.onAnyTargetSuccess -= onAnyTargetSuccess;

        isOver = true;
    }

    private Vector2 GetRandomPosition() => Managers.Vector.GetRandomPositionInViewPort();
}

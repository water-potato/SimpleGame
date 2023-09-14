using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager
{ 
    private List<Target> activeTargets = new List<Target>();

    public void Setup()
    {
        Target.onAnyTargetFailure += Target_OnAnyTargetFailure;
        Target.onAnyTargetSuccess += Target_OnAnyTargetSuccess;
    }

    public void GenerateTarget(int number , float maxTimer , GameObject targetPrefab , Vector3 position)
    {
        GameObject go = GameObject.Instantiate(targetPrefab, position, Quaternion.identity);
        Target target = go.GetComponent<Target>();
        activeTargets.Add(target);
        target.Setup(number, maxTimer);
    }

    private void Target_OnAnyTargetFailure(Target target)
    {
        activeTargets.Remove(target);
    }

    private void Target_OnAnyTargetSuccess(Target target)
    {
        activeTargets.Remove(target);
    }

    public IReadOnlyList<Target> ActiveTargets { get { return activeTargets; } }
}

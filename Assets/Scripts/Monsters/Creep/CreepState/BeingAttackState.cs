using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAttackState : CreepBaseState
{
    private int previousHp;
    private void Start()
    {
        previousHp = enemyStatus.CurrentHp;
    }
    public override bool EnterState()
    {
        if (enemyStatus.CurrentHp < previousHp)
            return true;
        return false;
    }
    float exitTime;
    float delayTime;
    public override void ExitState()
    {
        if (Time.time > exitTime)
            DoExitState.Invoke();
    }
    private void Update()
    {
        Debug.Log("TO-DO:Hieu ung mat hp");
        ExitState();
    }
    public override void UpdateState()
    {
        exitTime = Time.time + delayTime;
    }
}

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
        {
            exitTime = Time.time + delayTime;
            return true;
        }
        return false;
    }
    float exitTime;
    float delayTime;
    public override void ExitState()
    {
        DoExitState.Invoke(UpdateState);
    }

    public override void UpdateState()
    {
        if (Time.time < exitTime)
            Debug.Log("TO-DO:Hieu ung mat hp");
        else
            ExitState();
    }
}

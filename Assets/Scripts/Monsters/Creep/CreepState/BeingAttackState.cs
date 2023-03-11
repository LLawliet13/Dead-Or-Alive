using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CreepUpgradeController;

public class BeingAttackState : CreepBaseState
{
    private int previousHp;
    float delayTime;
    float exitTime;
    private void Start()
    {
        previousHp = enemyStatus.CurrentHp;
        delayTime = 0.2f;
    }
    public override bool EnterState()
    {
        if (enemyStatus.CurrentHp < previousHp)
        {
            UpdateSkillBaseOnCharacterLv();
            previousHp = enemyStatus.CurrentHp;
            exitTime = Time.time + delayTime;
            return true;
        }
        return false;
    }
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
    public override void UpdateSkillBaseOnCharacterLv()
    {
       
    }
}

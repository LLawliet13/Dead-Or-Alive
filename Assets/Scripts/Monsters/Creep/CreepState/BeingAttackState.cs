using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CreepUpgradeController;

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
            UpdateSkillBaseOnCharacterLv();
            previousHp = enemyStatus.CurrentHp;
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
        GetComponent<EnemyStatus>().beingAttackedEffect();
        ExitState();
    }
   
    public override void UpdateSkillBaseOnCharacterLv()
    {

    }
}

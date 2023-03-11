using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CreepUpgradeController;

public class FollowState : CreepBaseState
{
    private float attackRange;
    // Start is called before the first frame update
    private void Start()
    {
        attackRange = enemyStatus.BaseStats.AttackRange;
    }
    public override bool EnterState()
    {
        if (!FindPlayer()) return false;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > attackRange)
        {
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
        if (!EnterState())
            ExitState();
        else
            try
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyStatus.Speed * Time.deltaTime);
            }
            catch
            {
                Debug.LogError("player not found");
                ExitState();
            }
    }
    public override void UpdateSkillBaseOnCharacterLv()
    {
    }
}

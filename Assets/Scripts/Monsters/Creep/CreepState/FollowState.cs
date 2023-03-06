using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            return true;
        return false;
    }
    public override void ExitState()
    {
        trigger = false;
        DoExitState.Invoke();
    }
    private void Update()
    {
        if (trigger)
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
    }
    private bool trigger = false;
    public override void UpdateState()
    {
        trigger = true;
    }
}

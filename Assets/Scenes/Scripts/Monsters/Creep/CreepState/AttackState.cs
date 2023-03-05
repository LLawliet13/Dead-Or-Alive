using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CreepBaseState
{
    //tam danh cua quai
    private int range;
    public override bool EnterState()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= range)
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
    public void Update()
    {
        Debug.Log("TO-DO:Quet Khoang Cach va Tan cong");
        ExitState();
    }
    public override void UpdateState()
    {
        exitTime = Time.time + delayTime;
    }
    
}

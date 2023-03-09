using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CreepBaseState
{
    //tam danh cua quai
    private float attackRange;
    // Start is called before the first frame update
    private void Start()
    {
        attackRange = enemyStatus.BaseStats.AttackRange;
    }
    /// <summary>
    /// Chinh sua cac thong so cua state va check neu state co the trien khai
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        if (!FindPlayer()) return false;
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            return true;
        return false;
    }
    float attackTime;
    [SerializeField]
    float delayTime;
    public override void ExitState()
    {
        DoExitState.Invoke(UpdateState);
    }
    [SerializeField]
    private LayerMask layerMask;// player layer

    public override void UpdateState()
    {
        if (layerMask == 0)
            throw new System.Exception("Layer not setting for detect player");
        if (Time.time > attackTime)
        {
            Debug.Log("TO-DO:Quet Khoang Cach va Tan cong");
            Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, attackRange, layerMask);
            if (inRange.Length > 0)
                player.GetComponent<CharacterStatus>().TakeDamage(enemyStatus.Atk);
            attackTime = Time.time + delayTime;
        }
        ExitState();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : CreepBaseState
{
    [SerializeField]
    private GameObject bullet;
    private float attackRange;
    // Start is called before the first frame update
    private void Start()
    {
        fireTime = Time.time;
        attackRange = enemyStatus.BaseStats.AttackRange;
    }
    public override bool EnterState()
    {
        if (!FindPlayer()) return false;

        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange && Time.time > fireTime)
        {
            return true;
        }
        return false;
    }

    public override void ExitState()
    {
        DoExitState.Invoke(UpdateState);
    }
    [SerializeField]
    private float delayTime;
    private float fireTime;

    public override void UpdateState()
    {
        if (Time.time > fireTime)
        {
            GameObject a = Instantiate(bullet, transform.position, Quaternion.identity);
            Rock r = a.GetComponent<Rock>();
            r.setVector((player.position - transform.position).normalized);
            r.setSpeed(Random.Range(3f, 7.5f));
            r.SetATK(enemyStatus.Atk);
            fireTime = Time.time + delayTime;
            ExitState();
        }
    }
}

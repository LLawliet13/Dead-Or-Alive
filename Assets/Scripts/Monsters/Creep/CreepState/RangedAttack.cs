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

        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        return false;
    }

    public override void ExitState()
    {
        trigger = false;
        DoExitState.Invoke();
    }
    [SerializeField]
    private float delayTime;
    private float fireTime;

    private void Update()
    {
        if (trigger && Time.time > fireTime)
        {
            Debug.Log("TO-DO: Ban ra dan tu state ranged attack");
            GameObject a = Instantiate(bullet, transform.position, Quaternion.identity);
            Rock r = a.GetComponent<Rock>();
            r.setVector((player.position - transform.position).normalized);
            r.setSpeed(Random.Range(3f, 7.5f));
            r.SetATK(enemyStatus.Atk);
            fireTime = Time.time + delayTime;
            ExitState();
        }
    }
    bool trigger = false;
    public override void UpdateState()
    {
        trigger = true;
    }
}

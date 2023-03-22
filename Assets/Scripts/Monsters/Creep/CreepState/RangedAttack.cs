using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CreepUpgradeController;

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
            UpdateSkillBaseOnCharacterLv();
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

    private int numberOfBullet = 2;
    private float angleRange = 10;

    public override void UpdateState()
    {
        if (Time.time > fireTime)
        {



            Quaternion angle = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);

            for (int i = 0; i < numberOfBullet; i++)
            {
                Quaternion targetAngle = Quaternion.Euler(0, 0, Random.Range(angle.eulerAngles.z - angleRange + 90, angle.eulerAngles.z + angleRange + 90));
                GameObject a = Instantiate(bullet, transform.position, Quaternion.identity);

                EnemyBullet r = a.GetComponent<EnemyBullet>();
                r.setVector(targetAngle * new Vector3(1, 0, 0));
                r.setSpeed(Random.Range(3f, 7.5f));
                r.SetATK(enemyStatus.Atk);

            }
            fireTime = Time.time + delayTime;
            ExitState();



        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public override void UpdateSkillBaseOnCharacterLv()
    {
        SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
        int playerLv = sceneManager.GetPlayerLevel();
        CreepState5 stateBasedLv = creepUpgradeController.creepState5.OrderByDescending<CreepState5, int>(bs => bs.baseLv).Where(b => b.baseLv <= playerLv).First();
        delayTime = stateBasedLv.delayTime;
        numberOfBullet = stateBasedLv.numberOfBullet;
        angleRange = stateBasedLv.angleRange;
        return;
    }
}

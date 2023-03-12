using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CreepUpgradeController;
using UnityEngine.U2D;

public class AttackState : CreepBaseState
{
    //tam danh cua quai
    private float attackRange;
    // Start is called before the first frame update
    private void Start()
    {
        attackRange = enemyStatus.BaseStats.AttackRange;
        attackTime = Time.time;
    }
    /// <summary>
    /// Chinh sua cac thong so cua state va check neu state co the trien khai
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        if (!FindPlayer()) return false;
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            UpdateSkillBaseOnCharacterLv();
            return true;
        }
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
            Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, attackRange, layerMask);
            if (inRange.Length > 0)
                player.GetComponent<CharacterStatus>().TakeDamage(enemyStatus.Atk);
            attackTime = Time.time + delayTime;
        }
        ExitState();
    }
    public override void UpdateSkillBaseOnCharacterLv()
    {
        SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
        int playerLv = sceneManager.GetPlayerLevel();
        CreepState4 stateBasedLv = creepUpgradeController.creepState4.OrderByDescending<CreepState4, int>(bs => bs.baseLv).Where(b => b.baseLv <= playerLv).First();
        delayTime = stateBasedLv.delayTime;
        return;
    }

}

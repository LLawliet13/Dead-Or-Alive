using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static BossUpgradeController;

public class FollowSkill : BaseSkillBoss
{
    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        if (RangeSkill(player.transform.position))
        {

            UpdateSkillBaseOnCharacterLv();
            return true;

        }
        return false;
    }



    public override float CD_Skill()
    {
        return 0;
    }

    public override int LVToUse()
    {
        return 0;
    }

    public override bool RangeSkill(Vector3 position)
    {
        if (Vector3.Distance(transform.position, position) <= BaseRange.Range(1)) return true;
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        firstTimeUse = Time.time + 1;
    }
    [Header("Chi so nang cap")]
    [SerializeField]
    float bossSpeed = 2;


    public override void UpdateSkillBaseOnCharacterLv()
    {
        SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
        int playerLv = sceneManager.GetPlayerLevel();
        BossState0 stateBasedLv = bossUpgradeController.bossState0.OrderByDescending<BossState0,int>(bs => bs.baseLv).Where(b => b.baseLv<=playerLv).First();
        bossSpeed = stateBasedLv.bossSpeed;
        return;
    }

    protected override void SetAtkSkill()
    {
    }

    public override void UpdateState()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (!AbleToTrigger()) ExitState();
            else
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);
        }
    }
}

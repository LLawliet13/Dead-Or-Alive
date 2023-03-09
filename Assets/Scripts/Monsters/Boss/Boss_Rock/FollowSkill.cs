using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class FollowSkill : BaseSkillBoss
{
    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        if (RangeSkill(player.transform.position))
            return true;
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
        return;
    }

    protected override void SetAtkSkill()
    {
    }

    public override void UpdateState()
    {
        UpdateSkillBaseOnCharacterLv();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (!AbleToTrigger()) ExitState();
            else
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);
        }
    }
}

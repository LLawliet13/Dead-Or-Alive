using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControlPlayer : BaseSkillBoss
{
    public override bool AbleToTrigger()
    {
        return true;
    }

    public override bool AbleToTriggerWithOtherSkill()
    {
        return true;
    }

    public override float CD_Skill()
    {
        return 10;
    }

    public override float FirstTimeUse()
    {
        return Time.time + 1;
    }
    float EndTime;
    public override bool isSkillEnd()
    {
        if (Time.time > EndTime)
            return true;
        return false;
    }

    public override int LVToUse()
    {
        throw new System.NotImplementedException();
    }

    public override bool RangeSkill(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public override void RunSkill(GameObject Boss)
    {
        EndTime = Time.time + CD_Skill();
    }

    public override void UpdateSkillBaseOnCharacterLv()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetAtkSkill()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

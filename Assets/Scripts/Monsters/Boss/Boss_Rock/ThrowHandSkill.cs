using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHandSkill : BaseSkillBoss
{
    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        return true;
    }

    

    public override float CD_Skill()
    {
        throw new System.NotImplementedException();
    }

   
    public override int LVToUse()
    {
        throw new System.NotImplementedException();
    }

    public override bool RangeSkill(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

 

    public override void UpdateSkillBaseOnCharacterLv()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
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

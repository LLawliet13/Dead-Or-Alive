using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseSkillBoss
{
    //moi skill se co 1 cooldown de boss co the dung 1 luc nhieu skill hoac lan luot( vi du skill co cd cao se dung sau cd thap)
    public float CD_Skill();
    //viec boss se tien hoa theo thoi gian choi co the xay ra vi co ban khong thiet ke nhieu boss( lv nhan vat cang cao boss co? cang nhieu ki nang)
    public int LVToUse();

    //thoi gian khi khoi tao bat dau dung ki nang
    // return Time.time+Xs;
    public float FirstTimeUse();

    //cac thong bao cho boss biet luc nao nen dung skill tiep theo
    public bool isSkillEnd();
    // 1 so skill khong ho tro khi skill khac dang chay

    public bool AbleToTriggerWithOtherSkill();

    //vi du nhu boss phai o 1 trang thai nhat dinh moi dc trigger

    public bool AbleToTrigger();
    public bool RangeSkill(Vector3 position);
    public void RunSkill(GameObject Boss);
}

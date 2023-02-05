using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NOTE:
//skill bi dong  khong co kha nang kich hoat, cach ham chi dien cho co, chu yeu la viet run_skill
// vs skill bi dong thi AbleToTrigger luon phai tra ve false, con lai cac ham khac tra ve gi cung dc
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
    //NOTE: khi lv nhan vat cang cao rangeSkill se thay doi . vi ki nang duoc them nhieu hon
    public bool RangeSkill(Vector3 position);
    public void RunSkill(GameObject Boss);
    //nang cap cac skill khi nhan vat len cap
    public void UpdateSkillBaseOnCharacterLv();
}

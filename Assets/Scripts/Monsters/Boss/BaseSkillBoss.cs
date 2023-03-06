using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NOTE:
//skill bi dong  khong co kha nang kich hoat, cach ham chi dien cho co, chu yeu la viet run_skill
// vs skill bi dong thi AbleToTrigger luon phai tra ve false, con lai cac ham khac tra ve gi cung dc
public abstract class BaseSkillBoss : MonoBehaviour
{
    //moi skill se co 1 cooldown de boss co the dung 1 luc nhieu skill hoac lan luot( vi du skill co cd cao se dung sau cd thap)
    public abstract float CD_Skill();
    //viec boss se tien hoa theo thoi gian choi co the xay ra vi co ban khong thiet ke nhieu boss( lv nhan vat cang cao boss co? cang nhieu ki nang)
    public abstract int LVToUse();

    //thoi gian khi khoi tao bat dau dung ki nang
    // return Time.time+Xs;
    public abstract float FirstTimeUse();

    //cac thong bao cho boss biet luc nao nen dung skill tiep theo
    public abstract bool isSkillEnd();
    // 1 so skill khong ho tro khi skill khac dang chay

    public abstract bool AbleToTriggerWithOtherSkill();

    //vi du nhu boss phai o 1 trang thai nhat dinh moi dc trigger

    public abstract bool AbleToTrigger();
    //NOTE: khi lv nhan vat cang cao rangeSkill se thay doi . vi ki nang duoc them nhieu hon
    public abstract bool RangeSkill(Vector3 position);
    public abstract void RunSkill(GameObject Boss);
    //nang cap cac skill khi nhan vat len cap
    public abstract void UpdateSkillBaseOnCharacterLv();

    public int AtkSkill { get; protected set; }
    protected abstract void SetAtkSkill();
   
    protected EnemyStatus bossStatus;
    private void Awake()
    {
        bossStatus = GetComponent<BossStatus>();
    }
}

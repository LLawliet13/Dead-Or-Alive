using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// skill bi dong  khong co kha nang kich hoat, cach ham chi dien cho co, chu yeu la viet run_skill vs skill bi dong thi AbleToTrigger luon phai tra ve false, con lai cac ham khac tra ve gi cung dc
/// </summary>
 
public abstract class BaseSkillBoss : MonoBehaviour
{
    /// <summary>
    /// moi skill se co 1 cooldown de boss co the dung 1 luc nhieu skill hoac lan luot( vi du skill co cd cao se dung sau cd thap)
    /// </summary>
    /// <returns></returns>
    public abstract float CD_Skill();
    /// <summary>
    /// viec boss se tien hoa theo thoi gian choi co the xay ra vi co ban khong thiet ke nhieu boss( lv nhan vat cang cao boss co? cang nhieu ki nang)
    /// </summary>
    /// <returns></returns>
    public abstract int LVToUse();

    /// <summary>
    /// thoi gian khi khoi tao bat dau dung ki nang return Time.time+Xs;
    /// </summary>
    /// <returns></returns>
     
    public abstract float FirstTimeUse();

    /// <summary>
    /// cac thong bao cho boss biet luc nao nen dung skill tiep theo
    /// </summary>
    /// <returns></returns>
    public abstract bool isSkillEnd();
    /// <summary>
    /// 1 so skill khong ho tro khi skill khac dang chay
    /// </summary>
    /// <returns></returns>

    public abstract bool AbleToTriggerWithOtherSkill();

    /// <summary>
    /// vi du nhu boss phai o 1 trang thai nhat dinh moi dc trigger( khong lien quan toi thoi gian hoi chieu)
    /// </summary>
    /// <returns></returns>

    public abstract bool AbleToTrigger();
    /// <summary>
    /// dieu kien dinh huong chon skill: khoang cach tu quai toi nguoi choi cho phep ki nang nay duoc phep kich hoat
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public abstract bool RangeSkill(Vector3 position);
    public abstract void RunSkill(GameObject Boss);
    /// <summary>
    /// nang cap cac skill khi nhan vat len cap
    /// </summary>
    public abstract void UpdateSkillBaseOnCharacterLv();

    public int AtkSkill { get; protected set; }
    protected abstract void SetAtkSkill();
   
    protected EnemyStatus bossStatus;
    private void Awake()
    {
        bossStatus = GetComponent<BossStatus>();
    }
}

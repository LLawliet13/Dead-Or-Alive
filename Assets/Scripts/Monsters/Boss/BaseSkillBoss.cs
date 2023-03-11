using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



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
    /// moc thoi gian cho phep dung ki nang(ke tu luc boss duoc tao ra) return Time.time+Xs;
    /// </summary>
    /// <returns></returns>

    protected float firstTimeUse;
    public float FirstTimeUse()
    {
        return firstTimeUse;
    }

   

    /// <summary>
    /// vi du nhu boss phai o 1 trang thai nhat dinh moi dc trigger( khong lien quan toi thoi gian hoi chieu) va phai trien khai cac kieu kien enter state
    /// </summary>
    /// <returns></returns>

    public abstract bool AbleToTrigger();
    /// <summary>
    /// dieu kien dinh huong chon skill: khoang cach tu quai toi nguoi choi cho phep ki nang nay duoc phep kich hoat
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public abstract bool RangeSkill(Vector3 position);
    /// <summary>
    /// nang cap cac skill khi nhan vat len cap
    /// </summary>
    public abstract void UpdateSkillBaseOnCharacterLv();

    public int AtkSkill { get; protected set; }

    /// <summary>
    /// set gia tri sat thuong cua state cho boss status(optional)
    /// </summary>
    protected abstract void SetAtkSkill();

    protected EnemyStatus bossStatus;
    private void Awake()
    {
        bossStatus = GetComponent<BossStatus>();
    }
    protected float availableSkillTime;
    public bool isSkillCoolDown()
    {
        return Time.time < availableSkillTime;
    }
    /// <summary>
    /// cac thong so cac state se nang cap theo level nhan vat
    /// </summary>
    
    public BossUpgradeController bossUpgradeController;
    //dinh danh state khi chon state cho creep type
    public int state_id;
    //Luon trigger khi dieu kien enter dien ra
    public bool AbleToTriggerWithOther;
    //xu ly truong hop nhieu state co kha nang trigger trong cung 1 thoi diem, se chon theo do uu tien
    public int priority;

    protected EnemyStatus enemyStatus;
    /// <summary>
    /// ckeck dieu kien xem state nay kich hoat duoc khong
    /// </summary>
    /// <returns></returns>
    public bool EnterState()
    {
        SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
        int playerLv = sceneManager.GetPlayerLevel();
        //Khong dc vi tri 2 ham( vi ham able trigger trien khai cac dieu kien de enter state)
        if (((playerLv>=LVToUse())&&Time.time > FirstTimeUse()) && !isSkillCoolDown() && AbleToTrigger())
        {
            availableSkillTime = Time.time + CD_Skill();
            SetAtkSkill();
            return true;
        };
        return false;
    }

    //thuc thi hanh dong cua state
    public abstract void UpdateState();

    //thong bao exitState
    public UnityEvent<UnityAction> DoExitState;
    public void ExitState()
    {
        DoExitState.Invoke(UpdateState);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;
using static BossUpgradeController;

public class Dashingskill : BaseSkillBoss
{

    public override float CD_Skill()
    {
        return 6;
    }

    public override int LVToUse()
    {
        return 0;
    }
    public ObjectLookUp ObjectLookUp;// ve ra 1 duong line , khi diem cuoi cua line cham vao nhan vat thi notify cho skill nay

    public float bossSpeed = 30;
    Vector3 playerPosition;
    bool dashing = false;
    public void Dashing(Vector3 target)
    {
        focusTarget = false;//tien hanh dash thi khong warning nua
        dashing = true;
        this.playerPosition = target;
    }
    // Start is called before the first frame update
    void Start()
    {
        firstTimeUse = Time.time + 2;
        UnityEvent<Vector3> dashEvent = new UnityEvent<Vector3>();
        dashEvent.AddListener(Dashing);
        ObjectLookUp.Action = dashEvent;
    }
    bool focusTarget = false;
    float timeToRepeatSkill;
   


    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        if (RangeSkill(player.transform.position))
        {
            UpdateSkillBaseOnCharacterLv();
            focusTarget = true;
            timeToRepeatSkill = Time.time;
            return true;
        }
        return false;
    }

    public override bool RangeSkill(Vector3 position)
    {
        //vs lv thap (BaseRange.Range(1) <= Vector3.Distance(transform.position, position)) - khi chua co JumpSkill
        if (Vector3.Distance(transform.position, position) <= BaseRange.Range(2) && BaseRange.Range(1) < Vector3.Distance(transform.position, position)) return true;
        return false;
    }
    [Header("Chi so nang cap")]
    public int ableToDo = 1;//so lan nay se gia tang neu nhan vat len cap
    int timeRunSkill = 0;
    public override void UpdateSkillBaseOnCharacterLv()
    {
        SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
        int playerLv = sceneManager.GetPlayerLevel();
        BossState1 stateBasedLv = bossUpgradeController.bossState1.OrderByDescending<BossState1, int>(bs => bs.baseLv).Where(b => b.baseLv <= playerLv).First();
        ableToDo = stateBasedLv.ableToDo;
        return;
    }

    protected override void SetAtkSkill()
    {
        AtkSkill = Mathf.RoundToInt(bossStatus.Atk * 1.5f);
        GetComponent<BossStatus>().AtkState = AtkSkill;
    }

    public override void UpdateState()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (timeToRepeatSkill < Time.time)
        {
            if (focusTarget && player != null)
            {

                ObjectLookUp.gameObject.transform.LookAt(player.transform.position);
                ObjectLookUp.DrawLineWarning(player.transform.position);
            }
            if (dashing)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, bossSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, playerPosition) <= 0.2)
                {
                    dashing = false;
                    timeRunSkill++;
                    if (timeRunSkill == ableToDo)
                    {
                        timeRunSkill = 0;
                        GetComponent<BossStatus>().AtkState = bossStatus.Atk; //tra ve atk base
                        ExitState();
                    }
                    else
                    {
                        focusTarget = true;// thuc hien run skill lai cho den khi du so luot
                        timeToRepeatSkill = Time.time + 1f;
                    }
                }
            }
        }
    }
}

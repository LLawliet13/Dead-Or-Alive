using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static BossUpgradeController;

public  class JumpSkill : BaseSkillBoss
{

    // Start is called before the first frame update

    void Start()
    {
        SupportJumpHead = transform.Find("SupportJumpHead").gameObject;
        firstTimeUse = Time.time;
    }
    Vector3 target;
    // Update is called once per frame
    public float speed = 10;
    GameObject SupportJumpHead;
    GameObject player;
    bool jump = false;
    bool createEarthQuake = false;
    bool detectTarget = false;
    [Header("Vung rung chan gay sat thuong delay sau moi lan gay sat thuong, nen nho hon 1")]
    public float DelayCollisionDamageTime;
    GameObject targetO;

    void DestroyEarthQuake()
    {
        ExitState();
    }
    public GameObject EarthQuake;
    [Header("Chi so update ban kinh vung rung chan, nen de lon hon 5")]
    public float EarthQuakeRadiusRatio = 20;
    public GameObject TargetObject;
    void DetectTarget()
    {
       
        float radius = EarthQuake.GetComponent<CircleCollider2D>().radius * EarthQuakeRadiusRatio*0.1f;
        target = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(radius, 0, 0) + player.transform.position;//giong voi Ox
        transform.position = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(30, 0, 0) + target;
        transform.localScale *= 2.5f;


    }
    public override float CD_Skill()
    {
        return 5;
    }

    public override int LVToUse()
    {
        return 0;
    }




   

    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        if (RangeSkill(player.transform.position))
        {
            UpdateSkillBaseOnCharacterLv();
            detectTarget = true;
            runSkill = true;
            createEarthQuake = true;
            targetO = null;
            return true;
        }
        return false;
    }

    public override bool RangeSkill(Vector3 position)
    {
        //vs lv cao (Vector3.Distance(transform.position, position) <= BaseRange.Range(3) && BaseRange.Range(2) < Vector3.Distance(transform.position, position)
        if (BaseRange.Range(2) < Vector3.Distance(transform.position, position)) return true;
        return false;
    }
    bool runSkill = false;


    public override void UpdateSkillBaseOnCharacterLv()
    {
        SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
        int playerLv = sceneManager.GetPlayerLevel();
        BossState2 stateBasedLv = bossUpgradeController.bossState2.OrderByDescending<BossState2, int>(bs => bs.baseLv).Where(b => b.baseLv <= playerLv).First();
        DelayCollisionDamageTime = stateBasedLv.delayCollisionDamge;
        EarthQuakeRadiusRatio = stateBasedLv.EarthQuakeRatio;
        return;
    }

    protected override void SetAtkSkill()
    {
        AtkSkill = Mathf.RoundToInt(bossStatus.Atk * 1.2f);
        GetComponent<BossStatus>().AtkState = AtkSkill;
    }

    public override void UpdateState()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && detectTarget)
        {
            DetectTarget();
            targetO = Instantiate(TargetObject, target, Quaternion.identity);
            jump = true;
            detectTarget = false;
            createEarthQuake = true;
        }
        if (Vector3.Distance(target, transform.position) < 0.2 && createEarthQuake)
        {
            Destroy(targetO);
            transform.localScale /= 2.5f;
            GameObject a = Instantiate(EarthQuake, target, Quaternion.identity);
            a.GetComponent<EarthQuakeRockBoss>().setScaleTarget(EarthQuakeRadiusRatio);
            a.GetComponent<EarthQuakeRockBoss>().atk = Mathf.RoundToInt(bossStatus.Atk * 2.5f);
            a.GetComponent<EarthQuakeRockBoss>().DelayCollisionDamageTime = DelayCollisionDamageTime;
            UnityEvent destroyEvent = new UnityEvent();
            destroyEvent.AddListener(DestroyEarthQuake);
            a.GetComponent<EarthQuakeRockBoss>().DestroyEvent = destroyEvent;
            jump = false;
            createEarthQuake = false;
            return;
        }
        if (jump)
            transform.position = Vector3.MoveTowards(transform.position, target, speed * 2f * Time.deltaTime);
    }
}

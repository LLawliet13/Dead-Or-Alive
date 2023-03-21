using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BossUpgradeController;

public class ThrowRockSkill : BaseSkillBoss
{
    //skill bi dong nen khong co kha nang kich hoat, cach ham khac chi viet cho co, chu yeu la viet run_skill
    // vs skill bi dong thi AbleToTrigger luon phai tra ve false, con lai cac ham khac tra ve gi cung dc
    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        UpdateSkillBaseOnCharacterLv();
        return true;
    }



    public override float CD_Skill()
    {
        return 5;
    }
    public override int LVToUse()
    {
        return 0;
    }

    public override bool RangeSkill(Vector3 position)
    {
        //all range
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        firstTimeUse = Time.time;
    }
    // Update is called once per frame
    [SerializeField]
    [Header("Chi so nang cap")]
    private float angleRange = 30;// sai lech 10 do ve moi phai va goc cua player hien tai 

    [SerializeField]
    [Header("Chi so nang cap")]
    private int numberOfRock = 8;

    [SerializeField]
    [Header("Chi so nang cap")]
    private bool ableToBounceBack = false;
    [SerializeField]

    [Header("Chi so nang cap")]
    private float timeDestroy = 10;

    [Header("Chi so nang cap")]
    private float minSpeed = 3;

    [Header("Chi so nang cap")]
    private float maxSpeed = 7.5f;
    public GameObject rock;
    void Update()
    {


    }

    public override void UpdateSkillBaseOnCharacterLv()
    {
        SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
        int playerLv = sceneManager.GetPlayerLevel();
        BossState3 stateBasedLv = bossUpgradeController.bossState3.OrderByDescending<BossState3, int>(bs => bs.baseLv).Where(b => b.baseLv <= playerLv).First();
        angleRange = stateBasedLv.angleRange;
        numberOfRock = stateBasedLv.numberOfRock;
        timeDestroy = stateBasedLv.existTime;
        ableToBounceBack = stateBasedLv.ableToBounceBack;
        minSpeed = stateBasedLv.minSpeed;
        maxSpeed = stateBasedLv.maxSpeed;
        return;
    }

    protected override void SetAtkSkill()
    {
        AtkSkill = bossStatus.Atk;
    }

    public override void UpdateState()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Quaternion angle = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);

            for (int i = 0; i < numberOfRock; i++)
            {
                Quaternion targetAngle = Quaternion.Euler(0, 0, Random.Range(angle.eulerAngles.z - angleRange + 90, angle.eulerAngles.z + angleRange + 90));
                GameObject a = Instantiate(rock, transform.position, Quaternion.identity);
                Rock r = a.GetComponent<Rock>();
                r.setVector(targetAngle * new Vector3(1, 0, 0));
                r.setSpeed(Random.Range(minSpeed, maxSpeed));
                r.SetATK(Mathf.RoundToInt(bossStatus.Atk * 0.5f));
                r.ableToBounceBack = ableToBounceBack;
                r.timeDestroy = timeDestroy;
            }
            ExitState();
        }
    }
}

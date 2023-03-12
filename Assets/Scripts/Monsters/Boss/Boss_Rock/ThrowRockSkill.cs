using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRockSkill : BaseSkillBoss
{
    //skill bi dong nen khong co kha nang kich hoat, cach ham khac chi viet cho co, chu yeu la viet run_skill
    // vs skill bi dong thi AbleToTrigger luon phai tra ve false, con lai cac ham khac tra ve gi cung dc
    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
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
        if (Vector3.Distance(transform.position, position) > BaseRange.Range(3) && Vector3.Distance(transform.position, position) < BaseRange.Range(4)) return true;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        firstTimeUse = Time.time;
    }
    // Update is called once per frame
    [SerializeField]
    [Header("Chi so nang cap")]
    float angleRange = 30;// sai lech 10 do ve moi phai va goc cua player hien tai - chi so se nang cap

    [SerializeField]
    [Header("Chi so nang cap")]
    int numberOfRock = 8;//- chi so se nang cap
    public GameObject rock;
    void Update()
    {


    }

    public override void UpdateSkillBaseOnCharacterLv()
    {
        //angleRange
        //numberOfRock
        return;
    }

    protected override void SetAtkSkill()
    {
        AtkSkill = bossStatus.Atk;
    }

    public override void UpdateState()
    {
        UpdateSkillBaseOnCharacterLv();
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
                r.setSpeed(Random.Range(3f, 7.5f));
                r.SetATK(Mathf.RoundToInt(bossStatus.Atk * 0.5f));
                ExitState();
            }
            
        }
    }
}

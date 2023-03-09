using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRockSkill : BaseSkillBoss
{
    //skill bi dong nen khong co kha nang kich hoat, cach ham khac chi viet cho co, chu yeu la viet run_skill
    // vs skill bi dong thi AbleToTrigger luon phai tra ve false, con lai cac ham khac tra ve gi cung dc
    public override bool AbleToTrigger()
    {
        return true;
    }

    public override bool AbleToTriggerWithOtherSkill()
    {
        return true;
    }

    public override float CD_Skill()
    {
        return 5;
    }

    public override float FirstTimeUse()
    {
        return Time.time + 1;
    }

    public override bool isSkillEnd()
    {
        if (AbleToTrigger()) return false;
        else
        {
            runSkill = false;
            return true;
        }

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

    public override void RunSkill(GameObject Boss)
    {
        runSkill = true;
        nextTimeThrow = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    bool runSkill = true;
    // Update is called once per frame
    [SerializeField]
    [Header("Chi so nang cap")]
    float angleRange = 30;// sai lech 10 do ve moi phai va goc cua player hien tai - chi so se nang cap
    float nextTimeThrow;
    float delay_throw = 5f;
    [SerializeField]
    [Header("Chi so nang cap")]
    int numberOfRock = 8;//- chi so se nang cap
    public GameObject rock;
    void Update()
    {
        UpdateSkillBaseOnCharacterLv();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Quaternion angle = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            if (runSkill)
            {
                if (nextTimeThrow < Time.time)
                {
                    for (int i = 0; i < numberOfRock; i++)
                    {
                        Quaternion targetAngle = Quaternion.Euler(0, 0, Random.Range(angle.eulerAngles.z - angleRange + 90, angle.eulerAngles.z + angleRange + 90));
                        GameObject a = Instantiate(rock, transform.position, Quaternion.identity);
                        Rock r = a.GetComponent<Rock>();
                        r.setVector(targetAngle * new Vector3(1, 0, 0));
                        r.setSpeed(Random.Range(3f, 7.5f));
                        r.SetATK(Mathf.RoundToInt(bossStatus.Atk * 0.5f));
                        nextTimeThrow = Time.time + delay_throw;
                    }
                }

            }
        }

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
}

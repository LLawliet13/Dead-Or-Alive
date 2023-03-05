using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpSkill : MonoBehaviour, BaseSkillBoss
{

    // Start is called before the first frame update

    void Start()
    {
        SupportJumpHead = transform.Find("SupportJumpHead").gameObject;
    }
    Vector3 target;
    // Update is called once per frame
    public float speed = 10;
    GameObject SupportJumpHead;
    GameObject player;
    bool jump = false;
    bool createEarthQuake = false;
    bool detectTarget = false;
    GameObject targetO;
    void Update()
    {
        if (runSkill)
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
            if (Vector3.Distance(target, transform.position) < 0.2&&createEarthQuake)
            {
                Destroy(targetO);
                transform.localScale /= 2.5f;
                GameObject a = Instantiate(EarthQuake, target, Quaternion.identity);
                a.GetComponent<EarthQuakeRockBoss>().setScaleTarget(EarthQuakeRadiusRatio);
                UnityEvent destroyEvent = new UnityEvent();
                destroyEvent.AddListener(DestroyEarthQuake);
                a.GetComponent<EarthQuakeRockBoss>().DestroyEvent = destroyEvent;
                jump = false;
                createEarthQuake = false;
                return;
            }
            if (jump)
                //transform.position += MovementSetting.CalculateSlopeMoveVector(target, SupportJumpHead) * speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, speed *2f* Time.deltaTime);

        }
    }
    void DestroyEarthQuake()
    {
        skillEnd = true;
    }
    public GameObject EarthQuake;
    [Header("Chi so update ")]
    public float EarthQuakeRadiusRatio = 20;
    public GameObject TargetObject;
    void DetectTarget()
    {
        //SupportJumpHead.transform.rotation = Quaternion.Euler(0,0,Quaternion.LookRotation(Vector3.forward, player.transform.position).eulerAngles.z+180);
       
        float radius = EarthQuake.GetComponent<CircleCollider2D>().radius * EarthQuakeRadiusRatio*0.1f;
        //Debug.Log(radius);
        target = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(radius, 0, 0) + player.transform.position;//giong voi Ox
        transform.position = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(30, 0, 0) + target;
        transform.localScale *= 2.5f;


    }
    public float CD_Skill()
    {
        return 5;
    }

    public int LVToUse()
    {
        return 0;
    }

    public float FirstTimeUse()
    {
        return Time.time;
    }
    bool skillEnd = true;
    public bool isSkillEnd()
    {
        return skillEnd;
    }

    public bool AbleToTriggerWithOtherSkill()
    {
        return false;
    }

    public bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        if (RangeSkill(player.transform.position))
            return true;
        return false;
    }

    public bool RangeSkill(Vector3 position)
    {
        //vs lv cao (Vector3.Distance(transform.position, position) <= BaseRange.Range(3) && BaseRange.Range(2) < Vector3.Distance(transform.position, position)
        if (BaseRange.Range(2) < Vector3.Distance(transform.position, position)) return true;
        return false;
    }
    bool runSkill = false;
    public void RunSkill(GameObject Boss)
    {
        detectTarget = true;
        runSkill = true;
        createEarthQuake = true;
        skillEnd = false;
        targetO = null;
    }

    public void UpdateSkillBaseOnCharacterLv()
    {
        return;
    }
}

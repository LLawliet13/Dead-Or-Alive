using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;

public class Dashingskill : MonoBehaviour, BaseSkillBoss
{

    public float CD_Skill()
    {
        return 4;
    }

    public float FirstTimeUse()
    {
        return Time.time + 2;
    }

    public int LVToUse()
    {
        return 0;
    }
    public ObjectLookUp ObjectLookUp;
    public void RunSkill(GameObject Boss)
    {
        runSkill = true;
        SkillEnd = false;
    }
    public GameObject Boss;
    public float bossSpeed = 20;
    Vector3 playerPosition;
    bool dashing = false;
    public void Dashing()
    {
        runSkill = false;//tien hanh dash thi khong warning nua
        dashing = true;
        this.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityEvent dashEvent = new UnityEvent();
        dashEvent.AddListener(Dashing);
        ObjectLookUp.Action = dashEvent;
    }
    bool runSkill = false;
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (runSkill && player != null)
        {

            ObjectLookUp.gameObject.transform.LookAt(player.transform.position);
            ObjectLookUp.DrawLineWarning();
        }
        if (dashing)
        {
            Boss.transform.position = Vector3.MoveTowards(Boss.transform.position, playerPosition, bossSpeed * Time.deltaTime);
            if (Vector3.Distance(Boss.transform.position, playerPosition) <= 0.1)
            {
                dashing = false;
                SkillEnd = true;
            }
        }
    }

    public bool AbleToTriggerWithOtherSkill()
    {
        return false;
    }
    bool SkillEnd = true;
    public bool isSkillEnd()
    {
        return SkillEnd;
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
        if (Vector3.Distance(transform.position, position) <= 10) return true;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class FollowSkill : MonoBehaviour,BaseSkillBoss
{
    public bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        if (RangeSkill(player.transform.position))
            return true;
        return false;
    }

    public bool AbleToTriggerWithOtherSkill()
    {
        return false;
    }

    public float CD_Skill()
    {
        return 1;
    }

    public float FirstTimeUse()
    {
        return Time.time + 1;
    }

    public bool isSkillEnd()
    {
        if (AbleToTrigger()) return false;
        else
        {
            runSkill = false;
            return true;
        }
        
    }

    public int LVToUse()
    {
        return 0;
    }

    public bool RangeSkill(Vector3 position)
    {
        if (Vector3.Distance(transform.position, position) <= BaseRange.Range(1)) return true;
        return false;
    }

    public void RunSkill(GameObject Boss)
    {
        runSkill = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool runSkill = false;
    [Header("Chi so nang cap")]
    [SerializeField]
    float bossSpeed = 2;
    // Update is called once per frame
    void Update()
    {
        UpdateSkillBaseOnCharacterLv();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (runSkill && player != null)
        {

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);
        }
        
    }

    public void UpdateSkillBaseOnCharacterLv()
    {
        return;
    }
}

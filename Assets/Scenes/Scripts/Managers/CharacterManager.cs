using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//dang ki cac skill vao nguoi choi
public class CharacterManager : MonoBehaviour
{
    //tam thoi do chua them tinh nang luu trang thai nguoi choi,
    //nen can test skill nao cu tao class va them ten class vo day
    string[] skill_usings = { "MultipleShot" };
    // Start is called before the first frame update
    void Start()
    {
        loadData();//load thong tin tu file luu tru len day
        addSkill();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        saveData();
    }
    void saveData()
    {

    }
    void loadData()
    {

    }
    void addSkill()
    {
        Character_Skill cs = GameObject.FindGameObjectWithTag("Character").GetComponent<Character_Skill>();
        BaseSkill[] skills = (BaseSkill[])gameObject.GetComponents<BaseSkill>();
        if (skills == null)
        {
            Debug.Log("no Skill");
            return;
        }
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetName().Contains(skill_usings[i]))
            {
                cs.AddSkillListener(skills[i].RunSkill);
            }
        }
        
    }
}

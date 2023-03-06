using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//dang ki cac skill vao nguoi choi
public class CharacterManager : MonoBehaviour
{
    //tam thoi do chua them tinh nang luu trang thai nguoi choi,
    //nen can test skill nao cu tao class va them ten class vo day

    string[] skill_usings;
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
        string path = Application.dataPath;
        string jsonFilePathListSkill = $"{path}/Scripts/Skills/SelectSkill/SkillChosen.json";
        string json = File.ReadAllText(jsonFilePathListSkill);
        skill_usings = JsonConvert.DeserializeObject<string[]>(json);
    }
    void addSkill()
    {
        Character_Skill cs = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Skill>();
        UIManager uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        BaseSkill[] skills = (BaseSkill[])gameObject.GetComponents<BaseSkill>();
        if (skills == null)
        {
            Debug.Log("no Skill");
            return;
        }
        if (skills.Length < skill_usings.Length)
        {
            Debug.LogError("Missing Skill In GameObject GameMaster");
            return;
        }
        skills.OrderBy<BaseSkill, int>(s => s.getButtonIndex());
        for (int i = 0; i < skills.Length; i++)
        {
            string nameOfSkill = skills[i].GetName();
            for (int j = 0; j < skill_usings.Length; j++)
            {
                if (nameOfSkill.Contains(skill_usings[j]))
                {
                    //using anonymous method : (para) =>{}

                    cs.AddSkillListener(skills[i].RunSkill, (c) =>
                    {
                        Debug.Log(nameOfSkill + " Actived");
                        //Thong bao UI o day
                    });
                    uIManager.AddSkillListener(skills[i].getPathOfImage(), skills[i].RunSkill, (c) =>
                    {
                        Debug.Log(nameOfSkill + " Actived");
                        //Thong bao UI o day
                    });
                    break;
                }
            }
        }

    }

}

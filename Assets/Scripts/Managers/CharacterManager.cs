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


    public string[] skill_usings = { "name1", "name2", "name3" };
    /// <summary>
    /// viec load skill se uu tien doc tu game truoc khi bien nay duoc scenemanager set bang true
    /// </summary>
    public bool loadFromLastGame = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!loadFromLastGame)
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
    public void ReSignUpSkillls()
    {
        loadData();
        addSkill();
    }
    void loadData()
    {
        string path = Application.dataPath;
        string jsonFilePathListSkill = $"{path}/Scripts/Skills/SelectSkill/SkillChosen.json";
        string json = File.ReadAllText(jsonFilePathListSkill);
        skill_usings = JsonConvert.DeserializeObject<string[]>(json);
    }
    public void addSkill()
    {
        Character_SkillController cs = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_SkillController>();
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
                    uIManager.AddSkillListener(skills[i].getPathOfImage(), skills[i].GetCD(), skills[i], (c) =>
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

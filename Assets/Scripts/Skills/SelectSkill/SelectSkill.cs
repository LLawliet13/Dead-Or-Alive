using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    private List<string> savedSkills;
    private List<Skill> skillList = new List<Skill>();
    private int countChosenSkills;
    public UnityEvent eventSaveSkills;
    string SaveChosenSkillName = "SaveSkillList";

    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        buttonTemplate.GetComponent<Image>().color = Color.yellow;
        GameObject buttonClone;

        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        BaseSkill[] skills = (BaseSkill[])gameMaster.GetComponents<BaseSkill>();
        for (int i = 0; i < skills.Length; i++)
        {
            Skill skill = new Skill(skills[i].getPathOfImage(), skills[i].GetName(), skills[i].description(), skills[i].GetCD());
            skillList.Add(skill);
        }

        string json = PlayerPrefs.GetString(SaveChosenSkillName) ?? "";
        if (String.IsNullOrEmpty(json))
            savedSkills = new List<string>();
        else
            savedSkills = JsonConvert.DeserializeObject<List<string>>(json);
        if (savedSkills.Count == 3)
        {
            countChosenSkills = savedSkills.Count;
        }

        for (int i = 0; i < skillList.Count; i++)
        {
            buttonClone = Instantiate(buttonTemplate, transform);
            buttonClone.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(skillList[i].image);
            buttonClone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = skillList[i].name;
            buttonClone.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = skillList[i].description;
            buttonClone.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "CD: " + skillList[i].coolDown;
            foreach (string skillName in savedSkills)
            {
                if (skillList[i].name.Equals(skillName))
                {
                    buttonClone.GetComponent<Image>().color = Color.red;
                }
            }

            buttonClone.GetComponent<Button>().AddEventListener(buttonClone, ItemClicked);
        }
        Destroy(buttonTemplate);
    }
    void ItemClicked(GameObject button)
    {
        if (countChosenSkills <= 2 || button.GetComponent<Image>().color == Color.red)
        {
            if (button.GetComponent<Image>().color == Color.yellow)
            {
                button.GetComponent<Image>().color = Color.red;
                countChosenSkills++;
            }
            else
            {
                button.GetComponent<Image>().color = Color.yellow;
                countChosenSkills--;
            }
        }
    }

    public void SaveSkill()
    {
        if (countChosenSkills == 3)
        {
            savedSkills.Clear();
            Transform[] allChild = transform.GetComponentsInChildren<Transform>();

            //Debug.Log(allChild.Length);
            for (int i = 1; i < allChild.Length; i += 5)
            {
                if (allChild[i].GetComponent<Image>().color == Color.red)
                {
                    savedSkills.Add(allChild[i + 2].GetComponent<TextMeshProUGUI>().text);
                    string path = Application.dataPath;
                    var Json = JsonConvert.SerializeObject(savedSkills, Formatting.Indented);
                    PlayerPrefs.SetString(SaveChosenSkillName, Json);
                    PlayerPrefs.Save();
                }
            }
            GameObject.FindGameObjectWithTag("GameMaster").GetComponent<CharacterManager>().ReSignUpSkillls();
            eventSaveSkills.Invoke();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}

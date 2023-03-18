using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    private List<string> savedSkills;
    private List<Skill> skillList = new List<Skill>();
    private int countChosenSkills;
    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        buttonTemplate.GetComponent<Image>().color = Color.yellow;
        GameObject buttonClone;

        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        BaseSkill[] skills = (BaseSkill[])gameMaster.GetComponents<BaseSkill>();
        for(int i = 0; i < skills.Length; i++)
        {
            Skill skill = new Skill(skills[i].getPathOfImage(), skills[i].GetName(), skills[i].description());
            skillList.Add(skill);
        }

        string path = Application.dataPath;
        string jsonFilePathChosenSkill = $"{path}/Scripts/Skills/SelectSkill/SkillChosen.json";
        string json = File.ReadAllText(jsonFilePathChosenSkill);
        savedSkills = JsonConvert.DeserializeObject<List<string>>(json);

        for (int i = 0; i < skillList.Count; i++)
        {
            buttonClone = Instantiate(buttonTemplate, transform);
            buttonClone.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(skillList[i].image);
            buttonClone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = skillList[i].name;
            buttonClone.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = skillList[i].description;

            buttonClone.GetComponent<Button>().AddEventListener(buttonClone, ItemClicked);
        }
        Destroy(buttonTemplate);
    }
    void ItemClicked(GameObject button)
    {
        if(countChosenSkills <= 2 || button.GetComponent<Image>().color == Color.red)
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
        savedSkills.Clear();
        Transform[] allChild = transform.GetComponentsInChildren<Transform>();
        //Debug.Log(allChild.Length);
        for(int i = 1; i <= allChild.Length; i+=4)
        {
            //Debug.Log(allChild[i] + "////" + i);
            if (allChild[i].GetComponent<Image>().color == Color.red)
            {
                savedSkills.Add(allChild[i + 2].GetComponent<TextMeshProUGUI>().text);
                string path = Application.dataPath;
                var Json = JsonConvert.SerializeObject(savedSkills, Formatting.Indented);
                File.WriteAllText($"{path}/Scripts/Skills/SelectSkill/SkillChosen.json", Json);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
public static class ButtonExtension{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}

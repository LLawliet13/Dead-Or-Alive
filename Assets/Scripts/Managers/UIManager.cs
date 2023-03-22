using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> skillButtons;
    private LinkedList<BaseSkill> skills;
    public Button SelectSkills;
    SceneManager sceneManager;
    public GameObject canvasSelectSkills;
    void Awake()
    {
        skills = new LinkedList<BaseSkill>();
        sceneManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<SceneManager>();
        sceneManager.GameOverEvent = new UnityEvent();
        sceneManager.GameOverEvent.AddListener(GameOver);
        canvasSelectSkills.transform.Find("Scroll").Find("Panel").GetComponent<SelectSkill>().eventSaveSkills = new UnityEvent();
        canvasSelectSkills.transform.Find("Scroll").Find("Panel").GetComponent<SelectSkill>().eventSaveSkills.AddListener(CloseCanvasSelectSkills);
    }

    

    // Update is called once per frame
    void Update()
    {
        CheckSelectSkillsAvailable();
    }
    public void Button1Onclick()
    {
        ClickButton(0);
    }
    public void Button2Onclick()
    {
        ClickButton(1);
    }
    public void Button3Onclick()
    {
        ClickButton(2);
    }
    private void ClickButton(int index)
    {
        skills.ElementAt(index).RunSkill(GameObject.FindGameObjectWithTag("Player"));
        if (!skills.ElementAt(index).IsActive())
        {
            skillButtons[index].interactable = false;
            StartCoroutine(CoolDownControl(skillButtons[index], skills.ElementAt(index).GetCD() + Time.time, Time.time));
        }


    }

    IEnumerator CoolDownControl(Button button, float endtime, float startTime)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if (endtime <= Time.time)
        {
            button.interactable = true;
            button.image.fillAmount = 1;
        }
        else
        {
            button.interactable = false;
            button.image.fillAmount = (Time.time - startTime) / (endtime - startTime);
            StartCoroutine(CoolDownControl(button, endtime, startTime));
        }
    }
    public void AddSkill(string imageSkill, float CD, BaseSkill skill, params UnityAction<GameObject>[] action)
    {
        //if (skills.Count > 5) throw new System.Exception("So luong skill duoc su dung vuot qua gioi han");
        UnityEvent<GameObject> unityEvent = new UnityEvent<GameObject>();
        foreach (UnityAction<GameObject> unityAction in action)
            unityEvent.AddListener(unityAction);
        if(skills.Count >= 3)
        {
            skills.Clear();
        }
        skills.AddLast(skill);
        //"Sprites/Skills/For Bow/NameOfImage"
        //ref: https://docs.unity3d.com/ScriptReference/Resources.Load.html
        skillButtons.ElementAt(skills.Count - 1).GetComponent<Image>().sprite = Resources.Load<Sprite>(imageSkill);
    }
    public GameObject GameOverUI;
    public TextMeshProUGUI textMeshProUGUI;
    private void GameOver()
    {
        Time.timeScale = 0f;
        GameOverUI.SetActive(true);
        textMeshProUGUI.text += " " + sceneManager.Point + "";
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    /// <summary>
    /// them dich vu chay quang cao
    /// </summary>
    public void Continue()
    {

    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void CheckSelectSkillsAvailable()
    {
        if(!AreSkillsCoolDown())
        {
            SelectSkills.interactable = true;
        }
        else
        {
            SelectSkills.interactable = false;
        }
    }
    public void TurnOnButtonSelectSkills()
    {
        canvasSelectSkills.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseCanvasSelectSkills()
    {
        canvasSelectSkills.SetActive(false);
        Time.timeScale = 1f;
    }
    public bool AreSkillsCoolDown()
    {

        // skill chua dc click lan nao
        bool isAllSkillReady = false;
        //check skill chi bam 1 lan la cd
        foreach(var b in skillButtons)
            if (b.interactable == false) { 
                isAllSkillReady = true;
                break;
            }
        //check cac skill bam 2 lan moi cd
        foreach(var s in skills)
            if(s.IsActive() == true)
            {
                isAllSkillReady = true;
                break;
            }
        return isAllSkillReady;
    }
    private void Start()
    {
        TurnOnButtonSelectSkills();
    }
}

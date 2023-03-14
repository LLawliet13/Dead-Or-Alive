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
    // Start is called before the first frame update
    [HideInInspector]
    public List<Button> buttons;
    SceneManager sceneManager;
    void Awake()
    {
        skills = new LinkedList<BaseSkill>();
        sceneManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<SceneManager>();
        sceneManager.GameOverEvent = new UnityEvent();
        sceneManager.GameOverEvent.AddListener(GameOver);
    }

    // Update is called once per frame
    void Update()
    {

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
            buttons[index].interactable = false;
            //StartCoroutine(CheckCoolDown(buttons[index], skills.ElementAt(index).GetCD()));
            StartCoroutine(CoolDownControl(buttons[index], skills.ElementAt(index).GetCD() + Time.time, Time.time));
        }


    }
    IEnumerator CheckCoolDown(Button button, float cd)
    {
        float timeToStop = Time.time + cd;
        yield return new WaitForSeconds(cd);
        if (timeToStop <= Time.time)
        {
            button.interactable = true;
        }
    }
    IEnumerator CoolDownControl(Button button, float endtime, float startTime)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if (endtime <= Time.time)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
            button.image.fillAmount = (Time.time - startTime) / (endtime - startTime);
            StartCoroutine(CoolDownControl(button, endtime, startTime));
        }
    }
    LinkedList<BaseSkill> skills;
    public void AddSkillListener(string imageSkill, float CD, BaseSkill skill, params UnityAction<GameObject>[] action)
    {
        //if (skills.Count > 5) throw new System.Exception("So luong skill duoc su dung vuot qua gioi han");
        UnityEvent<GameObject> unityEvent = new UnityEvent<GameObject>();
        foreach (UnityAction<GameObject> unityAction in action)
            unityEvent.AddListener(unityAction);
        skills.AddLast(skill);
        //"Sprites/Skills/For Bow/NameOfImage"
        //ref: https://docs.unity3d.com/ScriptReference/Resources.Load.html
        buttons.ElementAt(skills.Count - 1).GetComponent<Image>().sprite = Resources.Load<Sprite>(imageSkill);
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

}

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public GameObject OptionMenuUI;
    public GameObject highScoreUI;
    public GameObject GameOverUI;
    public GameObject TutorialUI;
    public GameObject AudioCanvas;



    public void ResumeOptionMenu()
    {
        Debug.Log("Resume");
        Time.timeScale = 1f;
        OptionMenuUI.SetActive(false);

    }
    public void PauseOptionMenu()
    {
        OptionMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Pause");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenHighScore()
    {
        Debug.Log("HighScore");
        highScoreUI.SetActive(true);

    }
    public void CloseHighScore()
    {
        Debug.Log("HighScore");
        highScoreUI.SetActive(false);

    }
    public void OpenAudioSetting()
    {
        AudioCanvas.SetActive(true);
    }
    public void CloseAudioSetting()
    {
        AudioCanvas.SetActive(false);
    }
    public void OpenTutorial()
    {
        Debug.Log("Tutorial");
        TutorialUI.SetActive(true);
    }
    public void CloseTutorial()
    {
        Debug.Log("Tutorial");
        TutorialUI.SetActive(false);
    }
}

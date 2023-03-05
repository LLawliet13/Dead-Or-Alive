using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    public void Playgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Dung_Boss_Rock 1");
    }
    public void Quitgame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("");
    }
    public void Option()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Option");
    }
    public void BacktoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    UnityEvent AllowCreepSpawn, TriggerBossSpawn;// turn on - off viec spawn boss
    //TO-DO check dieu kien level sinh boss 
}

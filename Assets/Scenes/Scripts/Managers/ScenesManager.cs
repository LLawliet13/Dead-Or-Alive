using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene("Scene_Dung_Boss_Rock 1");
    }
    public void Quitgame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("");
    }
    public void Option()
    {
        SceneManager.LoadScene("Option");
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

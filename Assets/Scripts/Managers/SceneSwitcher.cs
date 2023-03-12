using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SceneSwitcher : MonoBehaviour
{

    public void Playgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void BacktoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

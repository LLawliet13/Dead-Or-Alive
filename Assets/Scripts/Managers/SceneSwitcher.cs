using Assets.Scenes.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonLoadGame;
    private void Start()
    {
        CanLoadGame();// kiem tra xem co lich su game khong , co thi hien thi nut load game
    }
    public void Playgame()
    {
        GetComponent<SaveGameManager>().ClearAllData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        PlayerPrefs.SetInt("LoadGame", 1);
        PlayerPrefs.Save();
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
    public void CanLoadGame()
    {
        try
        {
            if (GetComponent<SaveGameManager>().CheckIfDataExist())
                ButtonLoadGame.SetActive(true);
            else
                ButtonLoadGame.SetActive(false);
        }
        catch
        {
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Slider _musicSlider;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    //private void Update()
    //{
    //    if (GameIsPaused)
    //    {
    //        Resume();
    //    }
    //    else
    //    {
    //        Pause();
    //    }
    //}
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }
    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void HighScore()
    {
        pauseMenuUI.SetActive(true);

    }
}

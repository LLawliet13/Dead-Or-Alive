using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Slider _musicSlider;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void ToggleMusic()
    {
        Debug.Log("Pause Music");
        AudioManager.instance.ToggleMusic();
    }
    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }
    public void Resume()
    {
        Debug.Log("Resume");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;//m? bi?n vi?t ra cho c� � ?? m� m

    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);//h m?i scene ph?i t?o c�i uimanagwer ?? nems h?t script v�o canvas thi c� sao k th�i t l�m m? lu�n
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Pause");
    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void HighScore()
    {
        Debug.Log("HighScore");
        pauseMenuUI.SetActive(true);

    }
    public void Tutorial()
    {
        Debug.Log("Tutorial");
        pauseMenuUI.SetActive(true);
    }
    public void PlayerDeadUI()
    {
        pauseMenuUI.SetActive(true);
    }
}

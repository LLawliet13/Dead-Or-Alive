using System;
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
    private int PlayerLevel;
    public void SetPlayerLevel(int playerLevel)
    {
        PlayerLevel = playerLevel;
    }
    public int GetPlayerLevel()
    {
        return PlayerLevel;
    }
    private UnityEvent LevelUpEffectEvent;
    public void NotifyPlayerDie()
    {
        IsPlayerDie = true;
    }

    public void PlayerLevelUp()
    {
        LevelUpEffectEvent.Invoke();
    }
    internal void AddLevelUpCharacterEffect(UnityEvent levelUpEffectEvent)
    {
        LevelUpEffectEvent = levelUpEffectEvent;
    }
    private float SimulateTime;
    private bool IsPlayerDie;
    private void Awake()
    {
        PlayerLevel = -1;
        IsPlayerDie = false;
        SimulateTime = Time.time;
        creepSpawner = Instantiate(creepSpawner);
        bossSpawner = Instantiate(bossSpawner);
    }
    [SerializeField]
    private CreepSpawner creepSpawner;
    [SerializeField]
    private BossSpawner bossSpawner;

    private int LevelTriggerBoss = 0;
    private void Update()
    {
        //Debug.Log(PlayerLevel);
        Debug.Log("TO-DO: Them ham tinh kinh nghiem va cho nguoi choi len cap");
        Debug.Log("Hien tai gia lap nguoi choi len level moi 2s");
        if (Time.time >= SimulateTime && PlayerLevel < 4)
        {
            PlayerLevel += 1;
            SimulateTime = Time.time + 2f;
            PlayerLevelUp();
        }
        //spawn boss moi khi nhan vat tang 5 level
        if (PlayerLevel % 5 == 0)
        {
            creepSpawner.SettingController(BaseSpawner.Controller.TurnOff);
            if (LevelTriggerBoss != PlayerLevel)
            {
                bossSpawner.SettingController(BaseSpawner.Controller.TurnOn);
                LevelTriggerBoss = PlayerLevel;
            }
        }
        else
        {
            creepSpawner.SettingController(BaseSpawner.Controller.TurnOn);
            bossSpawner.SettingController(BaseSpawner.Controller.TurnOff);
        }
        if (IsPlayerDie)
        {
            Debug.Log("TO-DO: Them hanh dong cho viec nguoi choi die");
        }
    }
}

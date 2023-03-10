using Assets.Scenes.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public void NotifyPlayerDie()
    {
        IsPlayerDie = true;
    }

    public void PlayerLevelUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().LevelEffect();
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
        CheckIfLoadGame();
    }
    bool loadDataFromLastGame;
    [SerializeField]
    private CreepSpawner creepSpawner;
    [SerializeField]
    private BossSpawner bossSpawner;
    private void Start()
    {

    }
    private int LevelTriggerBoss = 0;
    /// <summary>
    /// khi tat game co kha nang nhan vat bi disable dan toi khong lay duoc hp cua nhan vat
    /// </summary>
    private int currentPlayerHp;
    private void Update()
    {
        CharacterStatus characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
        currentPlayerHp = characterStatus.CurrentHp;
        //Debug.Log(PlayerLevel);
        Debug.Log("TO-DO: Them ham tinh kinh nghiem va cho nguoi choi len cap");
        Debug.Log("Hien tai gia lap nguoi choi len level moi 2s");
        if (Time.time >= SimulateTime && PlayerLevel < 5)
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
    /// <summary>
    /// check xem nguoi choi chon load game hay choi moi o main menu sau do thuc hien hanh dong tuong ung
    /// recommend man 1 luu xuong PlayerPrefs 1 bien ten isLoadGame
    /// </summary>
    private void CheckIfLoadGame()
    {
        SaveGameManager saveGameManager = GetComponent<SaveGameManager>();
        CharacterStatus characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
        CharacterManager character_Skill = GetComponent<CharacterManager>();
        if (saveGameManager != null)
            //if (PlayerPrefs.HasKey("LoadGame"))
            //{
            //    if (PlayerPrefs.GetInt("LoadGame") == 1 && saveGameManager.CheckIfDataExist())
            //    {
            if (saveGameManager.CheckIfDataExist())//fake cu co du lieu la load
            {
                CharacterSaveGame data = saveGameManager.LoadGameFromFile();
                PlayerLevel = data.level;
                PlayerLevelUp();
                characterStatus.loadFromLastGame = true;
                characterStatus.SetCurrentHp(data.currentHp);
                character_Skill.loadFromLastGame = true;
                character_Skill.skill_usings = data.skillList;

            }
        //}
    }
    private void SaveData()
    {
        CharacterManager character_Skill = GetComponent<CharacterManager>();
        SaveGameEvent.Invoke(PlayerLevel, currentPlayerHp, character_Skill.skill_usings);

    }
    private void SaveHighScore()
    {
        Debug.Log("TO-DO:Setting Lay so diem va luu");
        SaveHighscoreEvent.Invoke(DateTime.Now, 10);
    }

    public UnityEvent<DateTime, int> SaveHighscoreEvent;
    public UnityEvent<int, int, string[]> SaveGameEvent;

    private void OnDisable()
    {
        if (!IsPlayerDie)
            SaveData();
        else
            SaveHighScore();
    }
}

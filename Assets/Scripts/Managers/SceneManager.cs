using Assets.Scenes.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    private int CurrentExp;
    private int TotalExpToNextLevel;
    public int Point { get; private set; }
    private int PlayerLevel;

    private List<IPlayerObserver> observers = new List<IPlayerObserver>();
    public void AddObserver(IPlayerObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(IPlayerObserver observer)
    {
        observers.Remove(observer);
    }

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
        Debug.Log("TO-DO: Them function cho nhan vat die");
        IsPlayerDie = true;
    }

    public void PlayerLevelUp()
    {
        PlayerLevel += 1;
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerLevelChanged(PlayerLevel);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().LevelEffect();
        getTotalExpToLevelUp();
        Debug.Log("PlayerLevel: " + PlayerLevel + "      TotalExp: " + TotalExpToNextLevel);
    }

    private float SimulateTime;
    private bool IsPlayerDie = false;
    private void Awake()
    {
        Debug.Log("Scenemanger");
        Time.timeScale = 1f;
        PlayerLevel = 0;
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
        PlayerLevelUp();
        getTotalExpToLevelUp();
        AddExp(0);
    }
    private int LevelTriggerBoss = 0;
    /// <summary>
    /// khi tat game co kha nang nhan vat bi disable dan toi khong lay duoc hp cua nhan vat
    /// </summary>
    private int currentPlayerHp;
    public UnityEvent GameOverEvent;
    public bool isNotifyDie = false;
    private void Update()
    {
        //CharacterStatus characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
        //currentPlayerHp = characterStatus.CurrentHp;
        //Debug.Log(PlayerLevel);


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
        if (IsPlayerDie && !isNotifyDie)
        {
            isNotifyDie = true;
            Debug.Log("TO-DO: Them hanh dong cho viec nguoi choi die");
            GameOverEvent.Invoke();
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
            if (PlayerPrefs.HasKey("LoadGame"))
            {
                if (PlayerPrefs.GetInt("LoadGame") == 1 && saveGameManager.CheckIfDataExist())
                {

                    CharacterSaveGame data = saveGameManager.LoadGameFromFile();
                    PlayerLevel = data.level;
                    PlayerLevelUp();
                    characterStatus.loadFromLastGame = true;
                    characterStatus.SetCurrentHp(data.currentHp);
                    character_Skill.loadFromLastGame = true;
                    character_Skill.skill_usings = data.skillList;
                    if (data.currentHp <= 0) IsPlayerDie = true;
                    PlayerPrefs.DeleteKey("LoadGame");
                    PlayerPrefs.Save();
                }
            }
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
        if (IsPlayerDie)
            SaveData();
        else
            SaveHighScore();
    }

    internal void AddExp(int value)
    {
        Debug.Log("TO-DO: Them kha nang tang exp tu drop item");
        CurrentExp += value;
        if (CurrentExp >= TotalExpToNextLevel)
        {
            CurrentExp = CurrentExp - TotalExpToNextLevel;
            PlayerLevelUp();
        }
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerExperienceGained(CurrentExp);
        }
    }

    internal int getTotalExpToLevelUp()
    {
        Debug.Log("TO-DO: Them ham tra ve tong exp de len level tiep theo");
        int solveForRequiredExp = 0;
        for (int levelCylce = 1; levelCylce <= PlayerLevel; levelCylce++)
        {
            solveForRequiredExp += (int)Mathf.Floor(levelCylce + 300 * Mathf.Pow(2, levelCylce / 7));
        }
        TotalExpToNextLevel = solveForRequiredExp / 4;
        Debug.Log("total level:" + TotalExpToNextLevel);
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerTotalExperienceChanged(TotalExpToNextLevel);

        }
        return TotalExpToNextLevel;
    }
    public void increaseExpPointForEnemy(EnemyStatus enemyStatus)
    {
        int numberOfEnemy = 0;
        int expForEachEnemy;
        Debug.Log("To-do: tinh exp cho quai tuy loai");
        bool isBoss = enemyStatus.GetType() == typeof(BossStatus);
        if (isBoss)
        {
            expForEachEnemy = TotalExpToNextLevel;
            Point += 100;
        }
        else
        {
            if (enemyStatus.BaseStats.EnemyType == 1)
            {
                numberOfEnemy = PlayerLevel + 30 * PlayerLevel / (PlayerLevel + 1);
                Point += 1;
            }
            else if (enemyStatus.BaseStats.EnemyType == 2)
            {
                numberOfEnemy = PlayerLevel + 32 * PlayerLevel / (PlayerLevel + 1);
                Point += 2;
            }
            expForEachEnemy = TotalExpToNextLevel / numberOfEnemy;
        }
        AddExp(expForEachEnemy);
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerScoreChanged(Point);
        }
    }

}

using Assets.Scenes.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    private int CurrentExp = 0;
    private int TotalExpToNextLevel;
    public int Point { get; private set; } = 0;
    private int PlayerLevel = 1;
    private GameObject Player;
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
        IsPlayerDie = true;
    }
    public void PlayerLevelUIUpdate()
    {
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerLevelChanged(PlayerLevel);
        }
    }
    public void HpUIUpdate()
    {

    }
    public void ExpUIUpdate()
    {
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerExperienceGained(CurrentExp);
        }
    }

    public void PlayerLevelUp()
    {
        PlayerLevel += 1;

        PlayerLevelUIUpdate();
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().LevelEffect();
        getTotalExpToLevelUp();
    }

    private float SimulateTime;
    private bool IsPlayerDie = false;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
        PlayerLevelUIUpdate();
        getTotalExpToLevelUp();
        AddExp(0);
        PointUiUpdate();
        if (PlayerPrefs.GetInt("BossAppear") == 1)
        {
            forceToSummonBoss = true;
        }
    }
    /// <summary>
    /// khi tat game co kha nang nhan vat bi disable dan toi khong lay duoc hp cua nhan vat
    /// </summary>
    private int currentPlayerHp;
    ItemDropSaveGame[] allItems;
    public UnityEvent GameOverEvent;
    public bool isNotifyDie = false;
    public bool isBossStageEnd = false;
    private int spawnBossLevel = 0;
    private int levelPerBoss = 5;
    private bool forceToSummonBoss = false;//trong truong hop ng choi o level khong % levelPerBoss = 0 va thoat game
    private void Update()
    {
        CharacterStatus characterStatus = Player.GetComponent<CharacterStatus>();
        currentPlayerHp = characterStatus.CurrentHp;

        //spawn boss moi khi nhan vat tang 5 level
        if (isBossStageEnd)
        {
            if (PlayerLevel % levelPerBoss == 0||forceToSummonBoss)
            {

                if (PlayerLevel != spawnBossLevel)
                {
                    creepSpawner.SettingController(BaseSpawner.Controller.TurnOff);
                    bossSpawner.SettingController(BaseSpawner.Controller.TurnOn);
                    isBossStageEnd = false;
                }
                if (forceToSummonBoss)//sau khi da trieu hoi r
                    forceToSummonBoss = !forceToSummonBoss;
            }
            else
            {
                creepSpawner.SettingController(BaseSpawner.Controller.TurnOn);
                bossSpawner.SettingController(BaseSpawner.Controller.TurnOff);
            }
        }
        else
        {
            //cover truong hop danh xong boss level van o level spawn boss khien cho boss lai xuat hien // giai phap la sau khi giet boss tang luon len 1 level// hoi tuan ham tang 1 level
        }
        if (IsPlayerDie && !isNotifyDie)
        {
            isNotifyDie = true;
            GameOverEvent.Invoke();
        }

        ItemDropSaveGame[] expItems = GameObject.FindObjectsOfType<ExpItem>().Select(e => new ItemDropSaveGame { itemName = e.Name, value = e.value, position = e.transform.position - Player.transform.position }).ToArray();
        ItemDropSaveGame[] healItems = GameObject.FindObjectsOfType<HealItem>().Select(e => new ItemDropSaveGame { itemName = e.Name, value = e.value, position = e.transform.position - Player.transform.position }).ToArray();
        allItems = expItems.Concat(healItems).ToArray();
    }
    [SerializeField]
    private GameObject ExpItem, HealItem;

    /// <summary>
    /// check xem nguoi choi chon load game hay choi moi o main menu sau do thuc hien hanh dong tuong ung
    /// recommend man 1 luu xuong PlayerPrefs 1 bien ten isLoadGame
    /// </summary>
    private void CheckIfLoadGame()
    {
        SaveGameManager saveGameManager = GetComponent<SaveGameManager>();
        CharacterStatus characterStatus = Player.GetComponent<CharacterStatus>();
        CharacterManager character_Skill = GetComponent<CharacterManager>();
        if (saveGameManager != null)

            if (PlayerPrefs.HasKey("LoadGame"))
            {
                if (PlayerPrefs.GetInt("LoadGame") == 1 && saveGameManager.CheckIfDataExist())
                {

                    CharacterSaveGame data = saveGameManager.LoadGameFromFile();
                    PlayerLevel = data.level;
                    CurrentExp = data.currentExp;
                    Point = data.CurrentPoint;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().LevelEffect();
                    characterStatus.loadFromLastGame = true;
                    characterStatus.SetCurrentHp(data.currentHp);
                    character_Skill.loadFromLastGame = true;
                    character_Skill.skill_usings = data.skillList;
                    ItemDropSaveGame[] allItems = data.items;

                    if (allItems != null)
                        foreach (var i in allItems)
                        {
                            if (i.itemName == "ExpItem")
                            {
                                var item = Instantiate(ExpItem, i.position, Quaternion.identity);
                                item.GetComponent<ExpItem>().value = i.value;
                                item.GetComponent<ExpItem>().DropPlace = i.position;
                                item.GetComponent<ExpItem>().DestroyEvent = null;
                            }
                            else
                            {
                                var item = Instantiate(HealItem, i.position, Quaternion.identity);
                                item.GetComponent<HealItem>().value = i.value;
                                item.GetComponent<HealItem>().DropPlace = i.position;
                                item.GetComponent<HealItem>().DestroyEvent = null;
                            }
                        }
                    if (data.currentHp <= 0) IsPlayerDie = true;
                    PlayerPrefs.DeleteKey("LoadGame");
                    PlayerPrefs.Save();
                }
            }
    }
    private void SaveData()
    {
        CharacterManager character_Skill = GetComponent<CharacterManager>();
        SaveGameEvent.Invoke(PlayerLevel, currentPlayerHp + "," + CurrentExp + "," + Point, character_Skill.skill_usings, allItems);

    }
    private void SaveHighScore()
    {
        SaveHighscoreEvent.Invoke(DateTime.Now, Point);
    }

    public UnityEvent<DateTime, int> SaveHighscoreEvent;
    public UnityEvent<int, string, string[], ItemDropSaveGame[]> SaveGameEvent;

    private void OnDisable()
    {
        if (!IsPlayerDie) { 
            SaveData();
            if (!isBossStageEnd)
            {
                PlayerPrefs.SetInt("BossAppear", 1);
            }
            else
            {
                PlayerPrefs.SetInt("BossAppear", 0);
            }
            PlayerPrefs.Save();
        }
        else
            SaveHighScore();
    }

    internal void AddExp(int value)
    {
        CurrentExp += value;
        if (CurrentExp >= TotalExpToNextLevel)
        {
            CurrentExp = CurrentExp - TotalExpToNextLevel;
            PlayerLevelUp();
        }
        ExpUIUpdate();
    }
    private void TotalExpUIUpdate()
    {
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerTotalExperienceChanged(TotalExpToNextLevel);

        }
    }
    private void PointUiUpdate()
    {
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerScoreChanged(Point);
        }
    }
    internal int getTotalExpToLevelUp()
    {
        int solveForRequiredExp = 0;
        for (int levelCylce = 1; levelCylce <= PlayerLevel; levelCylce++)
        {
            solveForRequiredExp += (int)Mathf.Floor(levelCylce + 300 * Mathf.Pow(2, levelCylce / 7));
        }
        TotalExpToNextLevel = solveForRequiredExp / 4;

        TotalExpUIUpdate();
        return TotalExpToNextLevel;
    }
    public void increaseExpPointForEnemy(EnemyStatus enemyStatus)
    {
        int numberOfEnemy = 0;
        int expForEachEnemy;
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

        PointUiUpdate();
    }

}

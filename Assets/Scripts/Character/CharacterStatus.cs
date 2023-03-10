using Assets.Scenes.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour
{
    public int MaxHP { get; private set; }
    public int CurrentHp { get; private set; }
    public int Def { get; private set; }
    public int Atk { get; private set; }

    /// <summary>
    /// Subject
    /// </summary>
    private List<IPlayerObserver> observers = new List<IPlayerObserver>();
    private int experience;
    private int score;

    public void AddObserver(IPlayerObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(IPlayerObserver observer)
    {
        observers.Remove(observer);
    }

    /// <summary>
    /// //////////////////////////////////
    /// </summary>
    [SerializeField]
    private PlayerBaseStatus baseStatus;
    public int playerLevel;
    private void ConfigStatus()
    {
        SceneManager sceneManager = FindObjectOfType<SceneManager>();
        if (sceneManager == null)
            throw new System.Exception("Missing Scene Manager in this Scene");
        playerLevel = sceneManager.GetPlayerLevel();
        MaxHP = CurrentHp = Mathf.RoundToInt(baseStatus.MaxHp * Mathf.Pow(baseStatus.HeSoLevelUpMaxHp, playerLevel));
        Def = Mathf.RoundToInt(baseStatus.Def * Mathf.Pow(baseStatus.HeSoLevelUpDef, playerLevel));
        Atk = Mathf.RoundToInt(baseStatus.Atk * Mathf.Pow(baseStatus.HeSoLevelUpAtk, playerLevel));

        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerMaxHpChanged(MaxHP);
            observer.OnPlayerLevelChanged(playerLevel);
        }
    }
    /// <summary>
    /// viec load skill se uu tien doc tu game truoc khi bien nay duoc scenemanager set bang true
    /// </summary>
    public bool loadFromLastGame = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!loadFromLastGame)
            ConfigStatus();
    }
    /// <summary>
    /// ham duoc goi de chinh lai sao lieu nguoi choi khi thang cap
    /// </summary>
    public void LevelEffect()
    {
        ConfigStatus();
    }
    public void TakeDamage(float Damage)
    {
        Debug.Log("TO-DO:Them hieu ung Take Damaged cho nhan vat");
        CurrentHp -= Mathf.RoundToInt((Damage * (1 - Def / 100f)));
        //Debug.Log(CurrentHp);

        CheckIfPlayerDie();
        SetCurrentHp(CurrentHp);
    }
    public void SetCurrentHp(int currentHp)
    {
        this.CurrentHp = currentHp;
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerDamaged(CurrentHp);
        }
    }
    public void CheckIfPlayerDie()
    {
        if (CurrentHp <= 0)
        {
            Debug.Log("TO-DO: Them function cho nhan vat die");
            SceneManager sceneManager = FindObjectOfType<SceneManager>();
            sceneManager.NotifyPlayerDie();
            foreach (IPlayerObserver observer in observers)
            {
                observer.OnPlayerKilled();
            }
        }

    }
    private void Update()
    {
        CheckIfPlayerDie();
    }
    /*    public void GainExperience(int experience)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.experience += experience;
                foreach (IPlayerObserver observer in observers)
                {
                    observer.OnPlayerExperienceGained(experience);
                }
            }

        }
        public void IncreaseScore(int score)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.score += score;
                foreach (IPlayerObserver observer in observers)
                {
                    observer.OnPlayerScoreChanged(score);
                }
            }

        }*/

}

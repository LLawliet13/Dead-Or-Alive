using Assets.Scenes.Scripts.Managers;
using System;
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
    public float Speed { get; private set; }

    /// <summary>
    /// Subject
    /// </summary>
    private List<IPlayerObserver> observers = new List<IPlayerObserver>();

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
    [HideInInspector]
    public int playerLevel;
    private SpriteRenderer sr;
    private Color originColor;
    private void ConfigStatus()
    {
        SceneManager sceneManager = FindObjectOfType<SceneManager>();
        if (sceneManager == null)
            throw new System.Exception("Missing Scene Manager in this Scene");
        playerLevel = sceneManager.GetPlayerLevel();
        MaxHP = CurrentHp = Mathf.RoundToInt(baseStatus.MaxHp * Mathf.Pow(baseStatus.HeSoLevelUpMaxHp, playerLevel));
        Def = Mathf.RoundToInt(baseStatus.Def * Mathf.Pow(baseStatus.HeSoLevelUpDef, playerLevel));
        Atk = Mathf.RoundToInt(baseStatus.Atk * Mathf.Pow(baseStatus.HeSoLevelUpAtk, playerLevel));
        Speed = baseStatus.Speed;
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerMaxHpChanged(MaxHP);
        }
    }

    /// <summary>
    /// viec load skill se uu tien doc tu game truoc khi bien nay duoc scenemanager set bang true
    /// </summary>
    [HideInInspector]
    public bool loadFromLastGame = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        originColor = sr.color;
        if (!loadFromLastGame)
            ConfigStatus();

        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerMaxHpChanged(MaxHP);
        }
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerDamaged(CurrentHp);
        }
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
        CurrentHp -= Mathf.RoundToInt((Damage * (1 - Def / 100f)));

        CheckIfPlayerDie();
        SetCurrentHp(CurrentHp);
        beingAttackedEffect();
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

    internal void AddHp(int value)
    {
        CurrentHp += value;
        if (CurrentHp > MaxHP)
            CurrentHp = MaxHP;
        foreach (IPlayerObserver observer in observers)
        {
            observer.OnPlayerDamaged(CurrentHp);
        }
    }
    protected  void beingAttackedEffect()
    {
        StartCoroutine(DamageEffectSequence(sr, originColor, Color.red, 0.7f, 0));
    }
    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color originColor, Color dmgColor, float duration, float delay)
    {

        // tint the sprite with damage color
        sr.color = dmgColor;

        // you can delay the animation
        yield return new WaitForSeconds(delay);

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
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

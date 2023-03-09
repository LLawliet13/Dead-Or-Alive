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
    }
    private UnityEvent LevelUpEffectEvent;

    // Start is called before the first frame update
    void Awake()
    {
        ConfigStatus();
        if (LevelUpEffectEvent == null)
        {
            LevelUpEffectEvent = new UnityEvent();
            LevelUpEffectEvent.AddListener(LevelEffect);
            SceneManager sceneManager = FindObjectOfType<SceneManager>();
            if (sceneManager == null)
                throw new System.Exception("Missing Scene Manager in this Scene");
            sceneManager.AddLevelUpCharacterEffect(LevelUpEffectEvent);
        }
    }
    public void LevelEffect()
    {
        ConfigStatus();
    }
    public void TakeDamage(float Damage)
    {
        Debug.Log("TO-DO:Them hieu ung Take Damaged cho nhan vat");
        CurrentHp -= Mathf.RoundToInt((Damage * (1 - Def / 100f)));
        if (CurrentHp <= 0)
        {
            Debug.Log("TO-DO: Them function cho nhan vat die");
            SceneManager sceneManager = FindObjectOfType<SceneManager>();
            sceneManager.NotifyPlayerDie();
        }
    }
    
}

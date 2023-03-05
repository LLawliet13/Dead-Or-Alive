using Assets.Scenes.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    private int MaxHP, CurrentHp, Def, Atk;
    [SerializeField]
    private BaseStatus baseStatus;
    private void autoConfigHP()
    {
        StatusManager.getHP(gameObject);
    }
    private void autoConfigDef()
    {
        StatusManager.getDef(gameObject);
    }
    private void autoConfigAtk()
    {
        StatusManager.getATK(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        MaxHP = CurrentHp = baseStatus.MaxHp;
        Def = baseStatus.DEF;
        Atk = baseStatus.ATK;
        autoConfigHP();
        autoConfigDef();
        autoConfigAtk();
    }
    public void TakeDamage(float Damage)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

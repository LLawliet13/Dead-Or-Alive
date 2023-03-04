using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStatus : MonoBehaviour
{
    
    public MonsterType BaseStats { get; set; }//apply type object
    protected int MaxHp, CurrentHp,Atk,Def,Speed;
    protected int currentPlayerLevel;
    protected int typeEnemy;// dung de thong bao cho Scene Manager biet tinh toan exp cho nhan vat
    //tinh level nhan vat de tang chi so cho quai dc goi khi quai enable
    protected void detectPlayerLevel()
    {
        Debug.Log("TO-DO:Thiet Lap Level Nhan vat, mac dinh = 1");
        currentPlayerLevel = 1;
    }
    protected abstract void caculateStatus();
    public void caculateDamageTaken(int damage)
    {
        CurrentHp -= (int)(damage * (1 - Def / 100f));
    }
    
}

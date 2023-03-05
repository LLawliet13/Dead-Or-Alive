using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyStatus : MonoBehaviour
{

    public MonsterType BaseStats;//apply type object
    public int MaxHp { get; protected set; }
    public int CurrentHp { get; protected set; }
    public int Atk { get; protected set; }

    public int Def { get; protected set; }

    public int Speed { get; protected set; }

    protected int currentPlayerLevel;
    protected int typeEnemy;// dung de thong bao cho Scene Manager biet tinh toan exp cho nhan vat
    //tinh level nhan vat de tang chi so cho quai dc goi khi quai enable
    protected void detectPlayerLevel()
    {
        Debug.Log("TO-DO:Thiet Lap Level Nhan vat, mac dinh = 1");
        currentPlayerLevel = 1;
    }
    public abstract void caculateStatus();
    public void caculateDamageTaken(int damage)
    {
        CurrentHp -= (int)(damage * (1 - Def / 100f));
    }
    public UnityEvent<EnemyStatus> onDestroy;

    public void DestroyMySelf()
    {
        onDestroy.Invoke(this);
    }
}

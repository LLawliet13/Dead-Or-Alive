using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyStatus : MonoBehaviour
{
    /// <summary>
    /// duoc load tu folder resource boi factory, khong can assign gia tri
    /// </summary>
    [Header("Tu dong load trong factory")]
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
        SceneManager sceneManager = FindObjectOfType<SceneManager>();
        if (sceneManager == null)
            throw new System.Exception("Missing Scene Manager in this Scene");
        currentPlayerLevel = sceneManager.GetPlayerLevel();
    }
    public abstract void caculateStatus();
    public void caculateDamageTaken(int damage)
    {
        CurrentHp -= Mathf.RoundToInt((damage * (1 - Def / 100f)));
        beingAttackedEffect();
    }
    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation;
    }
    public void ResetRotation()
    {
        transform.rotation = initialRotation;
    }
    //ham de tra creep ve pool or destroy hoan toan
    public UnityEvent<EnemyStatus> onDestroy;
    

    public void DestroyMySelf()
    {
        DropManager dropManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<DropManager>();
        bool isBoss = this.GetType() == typeof(BossStatus);
        dropManager.DropMechanism(transform.position, isBoss);
        onDestroy.Invoke(this);
    }
    protected abstract void beingAttackedEffect();
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Events;
using UnityEngine.Pool;

public class DropItemSpawner : MonoBehaviour
{
    [HideInInspector]
    public DropManager DropManager;
    public DropItemSpawnerController dropItemSpawnerController;
    [SerializeField]
    private DropItemFactory<ExpItem> expFactory;
    [SerializeField]
    private DropItemFactory<HealItem> healFactory;
    [SerializeField]
    private BaseDropItem ExpItemPrefab, HealItemPrefab;

    protected Vector3 RandomLocation(Vector3 enemy, float RangeFromEnemy)
    {
        if (enemy == null)
            throw new System.Exception("vi tri enemy chet chua duoc thiet lap");
        float randomAngle = Random.Range(0f, 360f);
        Vector3 position = enemy + Quaternion.Euler(0, 0, randomAngle) * new Vector3(RangeFromEnemy, 0, 0);
        return position;
    }
    private ObjectPool<BaseDropItem> healItemPool;
    private ObjectPool<BaseDropItem> expItemPool;
    public bool collectionChecks = false;
    [SerializeField]

    private int maxPoolSize;
    private void Start()
    {
        expFactory = Instantiate(expFactory);
        healFactory = Instantiate(healFactory);
        if (ExpDestroyEvent == null || HealDestroyEvent == null)
        {
            ExpDestroyEvent = new UnityEvent<BaseDropItem>();
            ExpDestroyEvent.AddListener(DieExpEvent);
            HealDestroyEvent = new UnityEvent<BaseDropItem>();
            HealDestroyEvent.AddListener(DieHealEvent);
        }
        expItemPool = new ObjectPool<BaseDropItem>(CreatePooledExpItem, OnTakeFromExpPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize, maxPoolSize);
        healItemPool = new ObjectPool<BaseDropItem>(CreatePooledHealItem, OnTakeFromHealPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize, maxPoolSize);
    }


    public void SpawnDropItemForBoss(Vector3 boss)
    {
        int healItemAmount = dropItemSpawnerController.totalHealItemNumberToHealFull;
        int expItemAmount = dropItemSpawnerController.totalExpItemNumberToLevelUp;
        for (int i = 0; i < expItemAmount; i++)
        {
            SpawnExpItem(boss);
        }
        for (int i = 0; i < healItemAmount; i++)
        {
            SpawnHealItem(boss);
        }
    }
    public void SpawnDropItemForCreep(Vector3 creep)
    {
        bool generateExp = Random.Range(0f, 101f) < dropItemSpawnerController.ExpRateDrop;
        if (generateExp)
        {
            SpawnExpItem(creep);
        }
        bool generateHeal = Random.Range(0f, 101f) < dropItemSpawnerController.HealRateDrop;
        if (generateHeal)
        {
            SpawnHealItem(creep);
        }
    }

    void SpawnHealItem(Vector3 enemy)
    {
        BaseDropItem item = healItemPool.Get();
        item.transform.position = enemy;
        item.DropPlace = RandomLocation(enemy, dropItemSpawnerController.DistanceFromEnemyForHeal);
    }
    void SpawnExpItem(Vector3 enemy)
    {
        BaseDropItem item = expItemPool.Get();
        item.transform.position = enemy;
        item.DropPlace = RandomLocation(enemy, dropItemSpawnerController.DistanceFromEnemyForExp);
    }

    private UnityEvent<BaseDropItem> ExpDestroyEvent;
    private UnityEvent<BaseDropItem> HealDestroyEvent;
    BaseDropItem CreatePooledExpItem()
    {
        BaseDropItem go = expFactory.GetNewInstance();
        go.DestroyEvent = ExpDestroyEvent;
        go.gameObject.SetActive(false);
        return go;
    }
    private void DieExpEvent(BaseDropItem item)
    {
        expItemPool.Release(item);
    }

    void OnReturnedToPool(BaseDropItem item)
    {
        item.gameObject.SetActive(false);
    }
    void OnTakeFromExpPool(BaseDropItem item)
    {
        item.gameObject.SetActive(true);
        item.value = DropManager.CaculateTotalExpToLevelUp() / dropItemSpawnerController.totalExpItemNumberToLevelUp;

    }


    BaseDropItem CreatePooledHealItem()
    {
        BaseDropItem go = healFactory.GetNewInstance();
        go.DestroyEvent = HealDestroyEvent;
        go.gameObject.SetActive(false);
        return go;
    }
    private void DieHealEvent(BaseDropItem item)
    {
        expItemPool.Release(item);
    }

    void OnTakeFromHealPool(BaseDropItem item)
    {
        item.gameObject.SetActive(true);
        item.value = DropManager.CaculateMaxPlayerHp() / dropItemSpawnerController.totalHealItemNumberToHealFull;

    }

    void OnDestroyPoolObject(BaseDropItem item)
    {
        Destroy(item);
    }
}

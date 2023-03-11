using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class CreepSpawner : BaseSpawner
{
    // Start is called before the first frame update
    void Start()
    {
        if (CreepDestroyEvent == null)
        {
            CreepDestroyEvent = new UnityEvent<EnemyStatus>();
            CreepDestroyEvent.AddListener(DieEvent);
        }
        //factory = Instantiate(factory);
        factory.TotalGenerateMonster = maxPoolSize;
        factory.Enable();
        pool = new ObjectPool<EnemyStatus>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize, maxPoolSize);
        timeToSpawn = Time.time;
        status = Controller.TurnOff;
    }
    [SerializeField]
    private GenericFactory<CreepStatus> factory;
    private ObjectPool<EnemyStatus> pool;
    public bool collectionChecks = false;
    [SerializeField]

    private int maxPoolSize;

    private UnityEvent<EnemyStatus> CreepDestroyEvent;

    EnemyStatus CreatePooledItem()
    {
        EnemyStatus go = factory.GetNewInstance();
        go.onDestroy = CreepDestroyEvent;
        go.gameObject.SetActive(false);
        return go;
    }
    private void DieEvent(EnemyStatus creep)
    {
        pool.Release(creep);
    }

    void OnReturnedToPool(EnemyStatus creep)
    {
        creep.gameObject.SetActive(false);
    }
    void OnTakeFromPool(EnemyStatus creep)
    {
        creep.gameObject.SetActive(true);
        creep.transform.position = RandomLocation();
        creep.caculateStatus();
        creep.GetComponent<CreepStateManager>().value = Assets.Scripts.Managers.BaseStateManager.Controller.TurnOn;

    }

    void OnDestroyPoolObject(EnemyStatus creep)
    {
        Destroy(creep);
    }
    
    public float delayTime = 0.5f;
    private float timeToSpawn;
    // Update is called once per frame
    void Update()
    {
        if (status == Controller.TurnOn)
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch
            {
                Debug.LogError("Player Not Found");
                return;
            }
            if (Time.time > timeToSpawn && pool.CountActive < maxPoolSize)
            {
                EnemyStatus es = pool.Get();
                es.transform.position = RandomLocation();
                timeToSpawn = Time.time + delayTime;
            }
        }
        else
        {
            CreepStatus[] creeps = FindObjectsOfType<CreepStatus>();
            foreach (var creep in creeps)
                try
                {
                    creep.DestroyMySelf();
                }
                catch
                {
                    Debug.Log("Creep is already destroyed");
                }
        }
    }

}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class CreepSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CreepDestroyEvent == null) { 
            CreepDestroyEvent = new UnityEvent<EnemyStatus>();
            CreepDestroyEvent.AddListener(DieEvent);
        }
        //factory = Instantiate(factory);
        factory.Enable();
        factory.TotalGenerateMonster = maxPoolSize;
        pool = new ObjectPool<EnemyStatus>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize, maxPoolSize);
        timeToSpawn = Time.time;
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
        creep.caculateStatus();
        creep.gameObject.SetActive(true);
        creep.transform.position = RandomLocation();
    }


    void OnDestroyPoolObject(EnemyStatus creep)
    {
        Destroy(creep);
    }
    private Vector3 RandomLocation()
    {
        Debug.Log("TO-DO:Random vi tri quai xuat hien");
        return Vector3.zero;
    }
    public float delayTime = 0.5f;
    private float timeToSpawn;
    // Update is called once per frame
    void Update()
    {
        if(Time.time> timeToSpawn&&pool.CountActive<maxPoolSize)
        {
            pool.Get();
            timeToSpawn = Time.time + delayTime;
        }
    }
}

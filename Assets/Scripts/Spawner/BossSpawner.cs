using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BossSpawner : BaseSpawner
{
    // Start is called before the first frame update
    void Start()
    {
        factory.Enable();
    }
    [SerializeField]
    private GenericEnemyFactory<BossStatus> factory;
    // Update is called once per frame
    void Update()
    {
        if (status == Controller.TurnOn)
            TriggerSpawn();
    }
    //status: hien tai sau khi sinh boss spawner se duoc tat
    public void TriggerSpawn()
    {
        SceneManager sceneManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<SceneManager>();
        BossStatus bossStatus = factory.GetNewInstance();
        bossStatus.caculateStatus();
        UnityEvent<EnemyStatus> unityEvent = new UnityEvent<EnemyStatus>();
        unityEvent.AddListener(sceneManager.increaseExpPointForEnemy);
        bossStatus.onDestroy = unityEvent;
        status = Controller.TurnOff;
        
    }
}

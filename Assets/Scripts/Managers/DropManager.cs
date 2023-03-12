using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropManager : MonoBehaviour
{

    private void Start()
    {
        dropItemSpawner = Instantiate(dropItemSpawner);
        dropItemSpawner.DropManager = this;
    }
    [SerializeField]
    private DropItemSpawner dropItemSpawner;

    public void DropMechanism(Vector3 position, bool isBoss)
    {
        if (isBoss)
        {
            dropItemSpawner.SpawnDropItemForBoss(position);
        }
        else
        {
            dropItemSpawner.SpawnDropItemForCreep(position);
        }
    }

    public int CaculateTotalExpToLevelUp()
    {
        SceneManager sceneManager = GetComponent<SceneManager>();
        return sceneManager.getTotalExpToLevelUp();

    }

    public int CaculateMaxPlayerHp()
    {
        CharacterStatus characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
        return characterStatus.MaxHP;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TriggerSpawn();
    }
    [SerializeField]
    private GenericFactory<BossStatus> factory;
    // Update is called once per frame
    void Update()
    {

    }
    public void TriggerSpawn()
    {
        Debug.Log("TO-DO: Viet dieu kien check level nhan vat de goi boss");
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
        factory.GetNewInstance();
    }
}

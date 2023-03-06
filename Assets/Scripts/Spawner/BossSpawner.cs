using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : BaseSpawner
{
    // Start is called before the first frame update
    void Start()
    {
        factory.Enable();
        status = Controller.TurnOff;
    }
    [SerializeField]
    private GenericFactory<BossStatus> factory;
    // Update is called once per frame
    void Update()
    {
        if (status == Controller.TurnOn)
            TriggerSpawn();
    }
    //status: hien tai sau khi sinh boss spawner se duoc tat
    public void TriggerSpawn()
    {
        Debug.Log("TO-DO: Viet dieu kien check level nhan vat de goi boss");
        
        factory.GetNewInstance().caculateStatus();
        status = Controller.TurnOff;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : BaseDropItem
{
    private void Start()
    {
        Name = "HealItem";
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, DropPlace) > 0.09)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPlace, 5 * Time.deltaTime);
        }
    }


    protected override void Action()
    {
        CharacterStatus characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
        characterStatus.AddHp(value);
        if (DestroyEvent != null)
        {
            DestroyEvent.Invoke(this);
        }
        else {
            //truong hop load tu save game len se khong tra ve pool
            Debug.Log("Destroy Event chua duoc thiet lap");
            Destroy(gameObject);
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : BaseDropItem
{
    private void Update()
    {
        if (Vector3.Distance(transform.position, DropPlace) > 0.09)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPlace, 5 * Time.deltaTime);
        }
    }


    public int healAmount;
    protected override void Action()
    {
        if (DestroyEvent != null)
        {

            CharacterStatus characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
            characterStatus.AddHp(value);
            DestroyEvent.Invoke(this);
        }
        else
            Destroy(gameObject);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : BaseDropItem
{
   

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

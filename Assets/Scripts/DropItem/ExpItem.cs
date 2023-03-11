using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : BaseDropItem
{


    public int ExpAmount;

    protected override void Action()
    {
        if (DestroyEvent != null)
        {
            SceneManager sceneManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<SceneManager>();
            sceneManager.AddExp(value);
            DestroyEvent.Invoke(this);
        }
        else
            Destroy(gameObject);

    }


}

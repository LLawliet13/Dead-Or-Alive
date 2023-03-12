using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : BaseDropItem
{
    private void Update()
    {
        if (Vector3.Distance(transform.position, DropPlace) > 0.09)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPlace, 5 * Time.deltaTime);
        }
    }

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

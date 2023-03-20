using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : BaseDropItem
{
    private void Start()
    {
        Name = "ExpItem";
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

        SceneManager sceneManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<SceneManager>();
        sceneManager.AddExp(value);
        if (DestroyEvent != null)
        {
            DestroyEvent.Invoke(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }


}

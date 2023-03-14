using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GarenSword : MonoBehaviour
{
    bool run = false;
    Vector3 targetPosition;
    private SpriteRenderer sprite; 
    internal void Trigger(Vector3 targetPosition)
    {

        this.targetPosition = targetPosition;
        run = true;
    }

    // Start is called before the first frame update
    public GameObject earthQuake;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); 
    }

    [HideInInspector]
    public int atk;


    // Update is called once per frame
    public float speed = 20;
    void Update()
    {
        if (run)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                run = false;
                EarthQuake a = Instantiate(earthQuake, new Vector3(transform.position.x, transform.position.y - sprite.bounds.extents.y), Quaternion.identity).GetComponent<EarthQuake>();
                a.sword = gameObject;
                a.atk = atk;
            }
        }
    }
}

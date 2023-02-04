using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GarenSword : MonoBehaviour
{
    bool run = false;
    Vector3 targetPosition;
    private SpriteRenderer sprite; //Declare a SpriteRenderer variable to holds our SpriteRenderer component
    internal void Trigger(Vector3 targetPosition)
    {

        this.targetPosition = targetPosition;
        run = true;
    }

    // Start is called before the first frame update
    public GameObject earthQuake;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); //Set the reference to our SpriteRenderer component
    }


    //sprite.bounds.extents.x; //Distance to the right side, from your center point
    //-sprite.bounds.extents.x //Distance to the left side
    //sprite.bounds.extents.y //Distance to the top
    //- sprite.bounds.extents.y //Distance to the bottom


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
                GameObject a = Instantiate(earthQuake, new Vector3(transform.position.x, transform.position.y - sprite.bounds.extents.y), Quaternion.identity);
                a.GetComponent<EarthQuake>().sword = gameObject;
            }
        }
    }
}

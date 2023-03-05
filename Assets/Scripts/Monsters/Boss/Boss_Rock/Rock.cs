using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Vector3 moveVector;
    internal void setVector(Vector3 vector3)
    {
        moveVector = vector3;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }
    [Header("Chi so nang cap")]
    public float speedAddition = 0;
    public float speed;
    [Header("Chi so nang cap")]
    public float timeDestroy = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position += moveVector.normalized * (speed+ speedAddition) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    // ki nang nang cap
    public void reboundSkill()
    {

    }
    internal void setSpeed(float v)
    {
        speed = v;
    }
}

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
        transform.rotation = Quaternion.LookRotation(Vector3.forward,moveVector);
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
    private int atk;
    public void SetATK(int atk)
    {
        this.atk = atk;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterStatus>().TakeDamage(atk);
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

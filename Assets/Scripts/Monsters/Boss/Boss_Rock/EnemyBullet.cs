using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEditor;
using UnityEngine;
using static AudioManager;

public class EnemyBullet : MonoBehaviour
{
    Vector3 moveVector;
   
    internal void setVector(Vector3 vector3)
    {
        moveVector = vector3;
    }
    Vector3 objectPosition;
    Vector3 objectDimensions;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        Destroy(gameObject, timeDestroy);
    }
    [Header("Chi so nang cap")]
    public float speedAddition = 0;
    public float speed;
    [Header("Chi so nang cap")]
    public float timeDestroy = 7;

    // Update is called once per frame
    void Update()
    {
        
        transform.position += moveVector.normalized * (speed + speedAddition) * Time.deltaTime;
    }
    private int atk;
    public bool ableToBounceBack = false;
    public void SetATK(int atk)
    {
        this.atk = atk;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            if (ableToBounceBack) checkBound(collision.contacts[0].normal);
        }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<CharacterStatus>().TakeDamage(atk);
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Collider2D[] contracts;
    //    collision.GetContacts(contracts);
        
    //}
    // ki nang nang cap
    public void reboundSkill()
    {

    }
    internal void setSpeed(float v)
    {
        speed = v;
    }
    private void checkBound(Vector3 inNormalVector)
    {
        moveVector = Vector3.Reflect(moveVector, inNormalVector);
    }
}

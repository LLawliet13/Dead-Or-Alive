using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WeightArrow : MonoBehaviour
{
    bool fire;
    Vector3 baseScale;
    Vector3 moveVector;
    float timeExist;
    [SerializeField]
    float moveSpeed;
    GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        timeExist = 1;
        fire = false;
        startTime = Time.time;
        baseScale = transform.localScale;
    }
    public void Fire(GameObject character)
    {
        this.character = character;
        fire = true;
    }
    float startTime;
    float ScaleOfArrow;
    float timeToCharge;
    public void SetScaleOfArrow(float size)
    {
        ScaleOfArrow = size;
    }
    // Update is called once per frame
    void Update()
    {
        if(fire == true)
        {
            moveVector = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").transform.position);
            transform.position += moveVector * moveSpeed * Time.deltaTime;
        }
        else
        {
            Debug.Log(transform.localScale.x / baseScale.x);
            //tang kich co bow khi hold
            if(transform.localScale.x / baseScale.x <= ScaleOfArrow)
            {
                float ratio = (Time.time - startTime)/ timeToCharge;
                if (ratio > 1) ratio = 1;
                timeExist = timeExist+0.2f;
                transform.localScale = new Vector3(ScaleOfArrow * ratio,
                    ScaleOfArrow * ratio, 0);
            }
        }
    }
   
    // Update is called once per frame

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fire == true)
            if (collision.CompareTag("Enemy"))
                //mui ten bi pha huy sau 1 khoang thoi gian sau khi cham vao 1 dot quai dau tien
                Destroy(gameObject, timeExist);
    }
    internal void SetChargeTime(float timeCharge)
    {
        timeToCharge = timeCharge;
    }
}

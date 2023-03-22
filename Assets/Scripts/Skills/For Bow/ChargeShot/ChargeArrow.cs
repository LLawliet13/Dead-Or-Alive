using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ChargeArrow : MonoBehaviour
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
    public int atk;
    public void Fire(GameObject character)
    {
        this.character = character;
        fire = true;
    }
    float startTime;
    float ScaleOfArrow;
    float maxMultipleDamage;
    float timeToCharge;
    public void SetScaleOfArrow(float size, float maxMultipleDamage)
    {
        ScaleOfArrow = size;
        this.maxMultipleDamage = maxMultipleDamage;
    }
    float ratioCharge;
    // Update is called once per frame
    void Update()
    {
        if (fire == true)
        {
            transform.position += transform.rotation* new Vector3(1,0,0) * moveSpeed * Time.deltaTime;
        }
        else
        {
            //tang kich co bow khi hold
            if (ratioCharge < 1)
            {
                timeExist = timeExist + 0.2f;
                ratioCharge = (Time.time - startTime) / timeToCharge;
                if (ratioCharge > 1) ratioCharge = 1;// gioi han do to cua chum anh sang
                timeExist = timeExist + 0.2f;
                transform.localScale = new Vector3(ScaleOfArrow * ratioCharge,
                    ScaleOfArrow * ratioCharge, 0);
            }
        }
    }

    // Update is called once per frame

    bool isTriggerDestroy = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Enemy"))
            {
                //mui ten bi pha huy sau 1 khoang thoi gian sau khi cham vao 1 dot quai dau tien
                if (!isTriggerDestroy)
                {
                    Destroy(gameObject, timeExist);
                    isTriggerDestroy = true;
                }
                GetComponent<BasePlayerWeaponStatus>().AttackEnemy(Mathf.RoundToInt(atk * maxMultipleDamage * ratioCharge), collision.GetComponent<EnemyStatus>());
            }
    }
    public void SetChargeTime(float timeCharge)
    {
        timeToCharge = timeCharge;
    }
}

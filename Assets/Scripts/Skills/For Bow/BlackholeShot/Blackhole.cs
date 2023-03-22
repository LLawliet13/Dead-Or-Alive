using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

/// <summary>
/// Skill nay rat de gay lag voi so luong quai trung chieu tu 50 con tro len vi so luong phep tinh chuyen dong nhieu
/// </summary>
public class Blackhole : MonoBehaviour
{
    // Start is called before the first frame update
    CircleCollider2D cc;
    float worldRadius;
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        Destroy(gameObject, 2);
        timeToDamage = Time.time;
        worldRadius = cc.radius * transform.lossyScale.x;

    }
    private float speed = 1f;
    private float timeToDamage;
    private float delayTime = 0.7f;
    private int atk;
    public void SetAtk(int atk)
    {
        this.atk = atk;
    }
    // Update is called once per frame
    public LayerMask enemy;
    Collider2D[] inRange;
    private float speedAngle = 360;
    bool attacked = false;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, worldRadius);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
           transform.rotation.eulerAngles.y,
           transform.rotation.eulerAngles.z + 50 * Time.deltaTime);
        inRange = Physics2D.OverlapCircleAll(transform.position, worldRadius, enemy);
        

        foreach (var c in inRange)
        {
            if (c.GetComponent<BossStatus>() != null)
            {
                if (isAffectBoss)
                    c.transform.position = MovementSetting.CalculateCircleMoveVector(transform.position, c.transform.position, speedAngle*Time.deltaTime, speed * Time.deltaTime,1f);
            }
            else {
                c.GetComponent<BaseStateManager>().status = BaseStateManager.Controller.TurnOff;//tam dung state cua quai nam trong vung anh huong ki nang
                c.transform.position = MovementSetting.CalculateCircleMoveVector(transform.position, c.transform.position, speedAngle * Time.deltaTime, speed * Time.deltaTime, 1f);
            }
            if (Time.time > timeToDamage)
            {
                attacked = true;
                GetComponent<BasePlayerWeaponStatus>().AttackEnemy(atk, c.GetComponent<EnemyStatus>());
            }

        }
        if (inRange.Length > 0 && attacked) { 
            timeToDamage = Time.time +  delayTime;
            attacked = false;
        }
    }
    public bool isAffectBoss = false;
    private void OnDisable()
    {
        try
        {
            foreach (var c in inRange)
            {
                c.GetComponent<EnemyStatus>().ResetRotation();
                c.GetComponent<BaseStateManager>().status = BaseStateManager.Controller.TurnOn;
            }
        }
        catch
        {
            Debug.Log("Object da bi disabled");
        }

    }

}

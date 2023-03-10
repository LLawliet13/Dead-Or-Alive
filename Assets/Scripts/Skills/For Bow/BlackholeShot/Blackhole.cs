using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class Blackhole : MonoBehaviour
{
    // Start is called before the first frame update
    CircleCollider2D cc;
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        Destroy(gameObject, 2);
        timeToDamage = Time.time;
    }
    private float speed = 4;
    private float timeToDamage;
    private float delayTime = 0.5f;
    private int atk;
    public void SetAtk(int atk)
    {
        this.atk = atk;
    }
    // Update is called once per frame
    public LayerMask enemy;
    Collider2D[] inRange;
    private float speedAngle = 6;
    private float speedAngleChange = 10;

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
           transform.rotation.eulerAngles.y,
           transform.rotation.eulerAngles.z + 20 * Time.deltaTime);
        inRange = Physics2D.OverlapCircleAll(transform.position, cc.radius / 2, enemy);


        foreach (var c in inRange)
        {
            if (c.GetComponent<BossStatus>() != null)
            {
                if (isAffectBoss)
                    c.transform.position += MovementSetting.CalculateSlopeMoveVector(transform.position, c.gameObject, speedAngle) * speed * Time.deltaTime;
            }
            else
                c.transform.position += MovementSetting.CalculateSlopeMoveVector(transform.position, c.gameObject, speedAngle) * speed * Time.deltaTime;
            if (Time.time > timeToDamage)
            {
                GetComponent<BasePlayerWeaponStatus>().AttackEnemy(atk, c.GetComponent<EnemyStatus>());
            }

        }
        if (inRange.Length > 0)
            timeToDamage += delayTime;
    }
    public bool isAffectBoss = false;
    private void OnDisable()
    {
       
        foreach (var c in inRange)
            try { 
            c.GetComponent<EnemyStatus>().ResetRotation();
            }
            catch
            {
                Debug.Log("Object da bi disabled");
            }

    }

}

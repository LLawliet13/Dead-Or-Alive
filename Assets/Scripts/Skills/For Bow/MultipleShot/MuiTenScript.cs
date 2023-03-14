using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MuiTenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5);
    }
    GameObject nearestEnemy;
    [SerializeField]
    [Header("speed di chuyen ve huong muc tieu")]
    public float speed;
    private float speedAngle = 8;
    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if ((nearestEnemy == null||nearestEnemy.activeSelf == false)  && enemies.Length > 0 )
        {
            float nearestDistance = Vector3.Distance(transform.position, enemies[0].transform.position);
            nearestEnemy = enemies[0];
            foreach (GameObject enemy in enemies)
                if (Vector3.Distance(transform.position, enemy.transform.position) < nearestDistance)
                {
                    nearestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                    nearestEnemy = enemy;
                }
        }
        if (nearestEnemy != null&& nearestEnemy.activeSelf == true)
        {
            if (Vector3.Distance(transform.position, nearestEnemy.transform.position)>2f)
            {

                transform.position += MovementSetting.CalculateSlopeMoveVector(nearestEnemy.transform.position, gameObject, speedAngle) * speed * Time.deltaTime;
            }
            else
            {
                transform.position += MovementSetting.CalculateStraightMoveVector(transform.gameObject,nearestEnemy.transform.position)* speed * Time.deltaTime;
            }

        }
    }
    public int atk;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            GetComponent<BasePlayerWeaponStatus>().AttackEnemy(atk, collision.GetComponent<EnemyStatus>());
            Destroy(gameObject);

        }
    }

}

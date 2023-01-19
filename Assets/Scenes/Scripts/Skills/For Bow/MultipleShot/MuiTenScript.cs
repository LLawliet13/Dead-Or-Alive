using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MuiTenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    GameObject nearestEnemy;
    [SerializeField]
    float speed;
    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            float nearestDistance = Vector3.Distance(transform.position, enemies[0].transform.position);
            nearestEnemy = enemies[0];
            foreach (GameObject enemy in enemies)
                if (Vector3.Distance(transform.position, enemy.transform.position) > nearestDistance)
                {
                    nearestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                    nearestEnemy = enemy;
                }
        }
        if (nearestEnemy != null) {
            if (transform.position.x != nearestEnemy.transform.position.x && transform.position.y != nearestEnemy.transform.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, speed * Time.deltaTime);
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

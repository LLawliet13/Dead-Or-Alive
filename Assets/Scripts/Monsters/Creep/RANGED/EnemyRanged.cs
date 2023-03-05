using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyRanged : MonoBehaviour
{
    public float moveSpeed;
    public Transform player;
    public Transform shotPoint;
    public Transform gun;
    public GameObject EnemyProjectile;
    public float followPlayerRange;
    private bool inRange;

    public float attackRange;

    public float startTimeBtwnShots;
    private float timeBtwnShots;


    // Update is called once per frame
    void Update()
    {
        Vector3 differance = player.position - gun.transform.position;
        float rotZ = Mathf.Atan2(differance.y, differance.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        if (Vector2.Distance(transform.position, player.position) <= followPlayerRange && Vector2.Distance(transform.position, player.position) > attackRange)
        {
            inRange = true;
        }
        else

        {

            inRange = false;
        }
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (timeBtwnShots <= 0)
            {
                Instantiate(EnemyProjectile, shotPoint.position, shotPoint.transform.rotation);
                timeBtwnShots = startTimeBtwnShots;
            }
            else

            {

                timeBtwnShots -= Time.deltaTime;
            }
        }

    }
    void FixedUpdate()
    {
        if (inRange)
        {

            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followPlayerRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    public float distance;
    public float enemyspeed;
    private Animator anm;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        anm = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, currentPos) <= distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, enemyspeed * Time.deltaTime);
            anm.SetBool("moving", true);
        }
        else
        {
            if (Vector2.Distance(transform.position, currentPos) <= 0)
            {
               
                anm.SetBool("moving", false);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPos, enemyspeed * Time.deltaTime);
                anm.SetBool("moving", true);
            }

        }
    }
}

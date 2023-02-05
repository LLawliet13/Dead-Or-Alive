using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyFollow : MonoBehaviour
{
    //public GameObject player;
    //private Transform playerPos;
    //private Vector2 currentPos;
    //public float distance;
    //public float enemyspeed;
    //private Animator anm;
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //playerPos = player.GetComponent<Transform>();
        //currentPos = GetComponent<Transform>().position;
        //anm = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector2.Distance(transform.position, currentPos) <= distance)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, playerPos.position, enemyspeed * Time.deltaTime);
        //    anm.SetBool("moving", true);
        //}
        //else
        //{
        //    if (Vector2.Distance(transform.position, currentPos) <= 0)
        //    {

        //        anm.SetBool("moving", false);
        //    }
        //    else
        //    {
        //        transform.position = Vector2.MoveTowards(transform.position, currentPos, enemyspeed * Time.deltaTime);
        //        anm.SetBool("moving", true);
        //    }

        //}
        Vector3 direction = player.position - transform.position;
        //Debug.Log(direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate()
    {
        EnemyMove(movement);
    }
    void EnemyMove(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}

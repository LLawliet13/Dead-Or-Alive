using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BlackholeBullet : MonoBehaviour
{
    public void SetAtk(int atk)
    {
        this.atk = atk;
    }
    private int atk;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    private float moveSpeed = 10;
    private Vector3 moveVector;
    void Update()
    {
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
    [SerializeField]
    private GameObject blackhole;
    private bool isBlackHoleCreated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.GetComponent<EnemyStatus>() != null && !isBlackHoleCreated)
        {
            GameObject b = Instantiate(blackhole, transform.position, Quaternion.identity);
            b.GetComponent<Blackhole>().SetAtk(atk);
            isBlackHoleCreated = true;
            Destroy(gameObject);

        }
    }
    private void OnDisable()
    {
        if (!isBlackHoleCreated)
        {
            try
            {
                Instantiate(blackhole, transform.position, Quaternion.identity).GetComponent<Blackhole>().SetAtk(atk);
            }
            catch
            {
                Debug.Log("khong the tao doi tuong vi game end");
            }
        }
    }

    internal void setMoveVector(Vector3 moveVector)
    {
        this.moveVector = moveVector;
    }
}

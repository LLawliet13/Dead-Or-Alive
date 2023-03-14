using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TornadoArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }
    Vector3 moveVector;
    public void SetVector(Vector3 moveVector)
    {
        this.moveVector = moveVector.normalized;
    }
    [SerializeField]
    float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += moveVector* speed * Time.deltaTime;
    }
    [HideInInspector]
    public int atk;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GetComponent<BasePlayerWeaponStatus>().AttackEnemy(atk, collision.GetComponent<EnemyStatus>());
            Destroy(gameObject);

        }
    }
}

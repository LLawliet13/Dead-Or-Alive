using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    //private Health playerHealth;
    // private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        ////Attack only when player in sight?
        //if (PlayerInSight())
        //{
        //    if (cooldownTimer >= attackCooldown)
        //    {
        //        cooldownTimer = 0;
        //        anim.SetTrigger("attack");
        //    }
        //}

        //if (enemyPatrol != null)
        //    enemyPatrol.enabled = !PlayerInSight();
    }

    //private bool PlayerInSight()
    //{
    //    RaycastHit2D hit =
    //        Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    //        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
    //        0, Vector2.left, 0, playerLayer);

    //    if (hit.collider != null)
    //        //playerHealth = hit.transform.GetComponent<Health>();

    //    return hit.collider != null;
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collisionObject = coll.gameObject;
        if (collisionObject.CompareTag("bullet"))
        {
            // take damage from bullet
            //BulletScript script = collisionObject.GetComponent<BulletScript>();
            //// update score
            //HUD hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
            //hud.AddPoints(100);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("bullet"))
        {
            // update score
            //HUD hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
            //hud.AddPoints(100);
        }
    }
    //private void DamagePlayer()
    //{
    //    if (PlayerInSight())
    //        playerHealth.TakeDamage(damage);
    //}
}

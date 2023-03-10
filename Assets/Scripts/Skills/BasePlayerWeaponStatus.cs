using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gan class nay vao cac vu khi hoac arrow, spear, effect,... sau do goi ham AttackEnemy trong collision function
/// </summary>
public class BasePlayerWeaponStatus : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// enemy status = collision.GetComponent<EnemyStatus>(); nho check xem collision.CompareTag("Enemy") = true thi moi call ham nay
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="enemyStatus"></param>
    public void AttackEnemy(int atk,EnemyStatus enemyStatus)
    {
        enemyStatus.caculateDamageTaken(atk);
    }

    
}

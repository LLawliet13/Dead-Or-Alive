using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepStatus : EnemyStatus
{
    SpriteRenderer spriteRenderer;
    private BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        typeEnemy = BaseStats.EnemyType;
        spriteRenderer.sprite = Resources.Load<Sprite>(BaseStats.AvatarPath);
        myCollider = GetComponent<BoxCollider2D>();
        if (myCollider != null && spriteRenderer != null)
        {
            myCollider.size = spriteRenderer.bounds.size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void caculateStatus()
    {
        detectPlayerLevel();
        Atk = (int)(BaseStats.Atk * Mathf.Pow(BaseStats.HeSoNangCapAtk, currentPlayerLevel));
        MaxHp = CurrentHp = (int)(BaseStats.Hp * Mathf.Pow(BaseStats.HeSoNangCapHp, currentPlayerLevel));
        Speed = (int)(BaseStats.Speed * Mathf.Pow(BaseStats.HeSoNangCapSpeed, currentPlayerLevel));
    }

    
}

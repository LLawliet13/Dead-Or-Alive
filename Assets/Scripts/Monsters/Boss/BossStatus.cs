using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : EnemyStatus
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        typeEnemy = BaseStats.EnemyType;
        spriteRenderer = transform.Find("Head").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(BaseStats.AvatarPath);
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

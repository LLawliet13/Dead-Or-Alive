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
        if (CurrentHp <= 0)
        {
            Destroy(gameObject);
            DestroyMySelf();//chi de cho co vi o day khong co pool quan ly boss
        }
    }
    public override void caculateStatus()
    {
        detectPlayerLevel();
        Atk = (int)(BaseStats.Atk * Mathf.Pow(BaseStats.HeSoNangCapAtk, currentPlayerLevel));
        MaxHp = CurrentHp = (int)(BaseStats.Hp * Mathf.Pow(BaseStats.HeSoNangCapHp, currentPlayerLevel));
        Speed = (int)(BaseStats.Speed * Mathf.Pow(BaseStats.HeSoNangCapSpeed, currentPlayerLevel));
    }

    public int AtkState = 0;
    private float nextCollisionDamageTime;
    [SerializeField]
    private float DelayCollisionDamageTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time > nextCollisionDamageTime)
        {
            CharacterStatus cs = collision.GetComponent<CharacterStatus>();
            if (AtkState == 0)
                cs.TakeDamage(Atk);
            else
                cs.TakeDamage(AtkState);
            nextCollisionDamageTime = Time.time + DelayCollisionDamageTime;

        }

    }
}

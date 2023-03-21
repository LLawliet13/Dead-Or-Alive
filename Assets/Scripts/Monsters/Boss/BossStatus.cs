using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class BossStatus : EnemyStatus
{
    SpriteRenderer spriteRenderer;
    Color originColor;
    // Start is called before the first frame update
    void Start()
    {
        typeEnemy = BaseStats.EnemyType;
        spriteRenderer = transform.Find("Head").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(BaseStats.AvatarPath);
        originColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHp <= 0)
        {
            NotifyBossStageEnd();
            Destroy(gameObject);
            DestroyMySelf();//chi de cho co vi o day khong co pool quan ly boss
        }
    }
    public override void caculateStatus()
    {
        detectPlayerLevel();
        Atk = (int)(BaseStats.Atk * Mathf.Pow(BaseStats.HeSoNangCapAtk, currentPlayerLevel - 1));
        MaxHp = CurrentHp = (int)(BaseStats.Hp * Mathf.Pow(BaseStats.HeSoNangCapHp, currentPlayerLevel - 1));
        Def = (int)(BaseStats.Def * Mathf.Pow(BaseStats.HeSoNangCapSpeed, currentPlayerLevel - 1));
        Speed = BaseStats.Speed;
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
    private void OnEnable()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = originColor;
    }
    protected override void beingAttackedEffect()
    {
        StartCoroutine(DamageEffectSequence(spriteRenderer, originColor, Color.red, 0.7f, 0));
    }
    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color originColor, Color dmgColor, float duration, float delay)
    {

        // tint the sprite with damage color
        sr.color = dmgColor;

        // you can delay the animation
        yield return new WaitForSeconds(delay);

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
    }
    private void NotifyBossStageEnd()
    {
        GameObject.FindGameObjectWithTag("GameMaster").GetComponent<SceneManager>().isBossStageEnd = true;

    }
}

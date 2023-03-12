using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepStatus : EnemyStatus
{
    SpriteRenderer spriteRenderer;
    Color originColor;
    private BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        typeEnemy = BaseStats.EnemyType;
        spriteRenderer.sprite = Resources.Load<Sprite>(BaseStats.AvatarPath);
        myCollider = GetComponent<BoxCollider2D>();
        if (myCollider != null && spriteRenderer != null)
        {
            myCollider.size = spriteRenderer.bounds.size;
        }
    }

    
    private void OnEnable()
    {
        // co the xay ra gimbal lock ??
        //ResetRotation();
        isFixRotation = false;
        if (spriteRenderer != null)
            spriteRenderer.color = originColor;
    }
    bool isFixRotation = false;
   
    // Update is called once per frame
    void Update()
    {
        ////fix bug gimbal lock
        if (!isFixRotation) { 
            ResetRotation();
            isFixRotation = true;
        }
        if (CurrentHp <= 0)
        {
            //viet ham dang ki ui, scenemanger o day
            DestroyMySelf();
        }
    }

    public override void caculateStatus()
    {
        detectPlayerLevel();
        Atk = (int)(BaseStats.Atk * Mathf.Pow(BaseStats.HeSoNangCapAtk, currentPlayerLevel));
        MaxHp = CurrentHp = (int)(BaseStats.Hp * Mathf.Pow(BaseStats.HeSoNangCapHp, currentPlayerLevel));
        Speed = (int)(BaseStats.Speed * Mathf.Pow(BaseStats.HeSoNangCapSpeed, currentPlayerLevel));
    }

    protected override void beingAttackedEffect()
    {
        StartCoroutine(DamageEffectSequence(spriteRenderer, originColor, Color.red, 0.5f, 0));
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
}

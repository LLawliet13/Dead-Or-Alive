using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP_EXP_SCORE_Manager : MonoBehaviour, IPlayerObserver
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public Image frontHealthBar;
    public Image backHealthBar;

    private int maxHp;
    private float currentHp;
    private int level;
    private float lerpTimer;
    private float chipSpeed = 2f;
    private bool start;

    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<CharacterStatus>().AddObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI(currentHp);
        levelText.text = level.ToString();
    }

    public void OnPlayerDamaged(float currentHealth)
    {
        currentHp = currentHealth;
        lerpTimer = 0;
    }

    public void OnPlayerExperienceGained(int experience)
    {
        expText.text = experience.ToString();
    }

    public void OnPlayerKilled()
    {
        Debug.Log("You are dead!");
    }

    public void OnPlayerScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }

    public void OnPlayerMaxHpChanged(int hp)
    {
        maxHp = hp;
        currentHp = hp;
    }

    public void OnPlayerLevelChanged(int lv)
    {
        level = lv;
    }

    public void UpdateHealthUI(float currentHP)
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = currentHP / maxHp;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(currentHP) + "/" + Mathf.Round(maxHp);
    }
}

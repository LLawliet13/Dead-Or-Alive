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
    public Image fronExpBar;
    public Image backExpBar;

    private int maxHp;
    private float currentHp;
    private int currentExp;
    private int maxExp;
    private int level;
    private float lerpTimerHp;
    private float lerpTimerExp;
    private float delayTimer;
    private float chipSpeed = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<CharacterStatus>().AddObserver(this);
        FindObjectOfType<SceneManager>().AddObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        UpdateExpUI();
        levelText.text = level.ToString();
    }

    public void OnPlayerMaxHpChanged(int hp)
    {
        maxHp = hp;
        currentHp = hp;
    }

    public void OnPlayerDamaged(float currentHealth)
    {
        currentHp = currentHealth;
        lerpTimerHp = 0;
    }

    public void OnPlayerExperienceGained(int experience)
    {
        currentExp = experience;
    }

    public void OnPlayerTotalExperienceChanged(int totalExp)
    {
        maxExp = totalExp;
    }

    public void OnPlayerKilled()
    {
        Debug.Log("You are dead!");
    }

    public void OnPlayerScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }
    

    public void OnPlayerLevelChanged(int lv)
    {
        level = lv;
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = currentHp / maxHp;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimerHp += Time.deltaTime;
            float percentComplete = lerpTimerHp / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimerHp += Time.deltaTime;
            float percentComplete = lerpTimerHp / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(currentHp) + "/" + Mathf.Round(maxHp);
    }

    public void UpdateExpUI()
    {
        float expFraction = currentExp / maxExp;
        float fillF = fronExpBar.fillAmount;
        if(fillF < expFraction)
        {
            delayTimer += Time.deltaTime;
            backExpBar.fillAmount = expFraction;
            if(delayTimer > 1)
            {
                lerpTimerExp += Time.deltaTime;
                float percentComplete = lerpTimerExp / 4;
                fronExpBar.fillAmount = Mathf.Lerp(fillF, backExpBar.fillAmount, percentComplete);
            }
        }
        expText.text = currentExp.ToString() + "/" + maxExp.ToString();
    }
}

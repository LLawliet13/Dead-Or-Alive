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
    public Image frontExpBar;
    public Image backExpBar;

    private int maxHp;
    private float currentHp;
    private int currentExp;
    private int maxExp;
    private int level;
    private int oldLevel;
    private float lerpTimerHp;
    private float lerpTimerExp;
    private float delayTimer;
    private float chipSpeed = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("UIHPEXP");
        FindObjectOfType<CharacterStatus>().AddObserver(this);
        FindObjectOfType<SceneManager>().AddObserver(this);
        oldLevel = level;
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
        scoreText.text = "Point: " + score;
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
        if(oldLevel < level)
        {
            frontExpBar.fillAmount = 0f;
            backExpBar.fillAmount = 0f;
            oldLevel++;
        }
        float expFraction = (float)currentExp / maxExp;
        float fillF = frontExpBar.fillAmount;
        Debug.Log(expFraction + "//////" + currentExp + "/////" + maxExp +"/////" + fillF);
        if(fillF < expFraction)
        {
            delayTimer += Time.deltaTime;
            backExpBar.fillAmount = expFraction;
            if(delayTimer > 1)
            {
                lerpTimerExp += Time.deltaTime;
                float percentComplete = lerpTimerExp / 4;
                frontExpBar.fillAmount = Mathf.Lerp(fillF, backExpBar.fillAmount, percentComplete);
            }
        }
        expText.text = currentExp.ToString() + "/" + maxExp.ToString();
    }
}

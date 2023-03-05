using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class WeightShot : MonoBehaviour, BaseSkill
{
    [SerializeField]
    GameObject WeightArrow;
    public float GetCD()
    {
        return 0;
    }

    public string GetName()
    {
        return "WeightShot";
    }

    public bool IsActive()
    {
        return true;
    }
    Boolean hold = false;
    GameObject HoldArrow;
    [SerializeField]
    [Header("Do lon cua mui ten ban ra,>0")]
    float ScaleOfArrow;
    [SerializeField]
    [Header("thoi gian du tinh charge xong,>0")]
    float timeCharge;
 
    public void RunSkill(GameObject character)
    {
        //viet len tren day vi trong ham nay co xet dieu kien cua hold,
        // ma if else co thay doi gia tri cua hold
        SupportUISkill(character);

        if (!hold)
        {
            hold = true;
            Vector3 diff = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").position);
            float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            HoldArrow = Instantiate(WeightArrow, character.transform.position, Quaternion.Euler(0, 0, curAngle));
            HoldArrow.transform.parent = character.transform.Find("WeaponParent").Find("Weapon");
            HoldArrow.GetComponent<WeightArrow>().SetScaleOfArrow(ScaleOfArrow);
            HoldArrow.GetComponent<WeightArrow>().SetChargeTime(timeCharge);
            HoldArrow.GetComponent<SpriteRenderer>().enabled = false;
            character.transform.Find("WeaponParent").Find("Weapon").GetComponent<Animator>().Play("WeightBow_Charge");
        }
        else
        {
            hold = false;
            HoldArrow.GetComponent<SpriteRenderer>().enabled = true;
            character.transform.Find("WeaponParent").Find("Weapon").GetComponent<Animator>().Play("Normal");
            HoldArrow.GetComponent<WeightArrow>().Fire(character);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        ChargeBar.SetActive(false);
    }
    float createArrowTime;
    // Update is called once per frame
    void Update()
    {
        ChargeProcess();
    }
    [SerializeField]
    private GameObject ChargeBar;
    [SerializeField]
    private TextMeshProUGUI ChargeText;
    [SerializeField]
    private RectTransform Bar;
    public void SupportUISkill(GameObject character)
    {
        if (!hold)
        {
            //hien thi tu luc
            ChargeBar.SetActive(true);
            Bar.localScale = new Vector3(0, Bar.localScale.y, Bar.localScale.z);
            createArrowTime = Time.time;

        }
        else
        {
            //Tat tu luc khi an skill lan 2
            ChargeBar.SetActive(false);
        }
        return;
    }
    void ChargeProcess()
    {
        if (ChargeBar.activeSelf == true)
        {
            float value = (float)(Time.time - createArrowTime) / timeCharge;
            if (value <= 1)
            {
                Bar.localScale = new Vector3(value, Bar.localScale.y, Bar.localScale.z);
                ChargeText.text = (int)(value * 100) + " %";
            }
            else
            {
                Bar.localScale = new Vector3(1, Bar.localScale.y, Bar.localScale.z);
                ChargeText.text = 100 + " %";
            }
        }
        
    }
    public int getButtonIndex()
    {
        return 2;
    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Bow/WeightShot";
    }

    public string description()
    {
        return "t?? l??c b??n ra 1 light wave, tu?y theo th??i gian tu? l??c, light wave se? to dâ?n va? tô?n ta?i lâu h?n";
    }
}

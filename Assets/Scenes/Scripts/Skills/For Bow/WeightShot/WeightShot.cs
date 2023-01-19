using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class WeightShot : MonoBehaviour,BaseSkill
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
    public void RunSkill(GameObject character)
    {
        if (!hold)
        {
            hold = true;
            HoldArrow =  Instantiate(WeightArrow, character.transform.position, Quaternion.identity);
            HoldArrow.transform.parent = character.transform.Find("Weapon");
        }
        else { 
            hold = false;
            HoldArrow.GetComponent<WeightArrow>().Fire(character);
        }
        SupportUISkill(character);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SupportUISkill(GameObject character)
    {
        if (!hold)
        {
           //hien thi tu luc
        }
        else
        {
            //hien thi thoi gian cd
        }
        return;
    }

    public int getButtonIndex()
    {
        return 2;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseSkill 
{
    public void RunSkill(GameObject character);
    public string GetName();
    public float GetCD();
    public bool IsActive();// co the trien khai hay khong

   
}

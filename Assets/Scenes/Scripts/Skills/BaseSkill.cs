using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseSkill 
{
    public void RunSkill(GameObject character);
    public string GetName();
    public float GetCD();
    public bool IsActive();// co the trien khai hay khong

    public void SupportUISkill(GameObject character);// neu skill can tuong tac voi 1 so UI dac biet gi do thi viet vao day

    public int getButtonIndex();
    public string getPathOfImage();
    public string description();
}

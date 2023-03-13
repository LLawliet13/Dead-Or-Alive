using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseSkill 
{
    /// <summary>
    /// ham goi de trien khai skill
    /// </summary>
    /// <param name="character"></param>
    public void RunSkill(GameObject character);
    /// <summary>
    /// ten ki nang
    /// </summary>
    /// <returns></returns>
    public string GetName();
    /// <summary>
    /// thoi gian hoi chieu
    /// </summary>
    /// <returns></returns>
    public float GetCD();
    /// <summary>
    /// kiem tra xem skill hien tai con an duoc khong, thuong duoc goi boi ui
    /// </summary>
    /// <returns></returns>
    public bool IsActive();// co the trien khai hay khong
    /// <summary>
    /// khi ki nang khoi chay, co ui gi di kem thi viet vao day, ham nay duoc goi trong runskill()
    /// </summary>
    /// <param name="character"></param>

    public void SupportUISkill(GameObject character);// neu skill can tuong tac voi 1 so UI dac biet gi do thi viet vao day
    /// <summary>
    /// vi tri dang ki nut ki nang
    /// </summary>
    /// <returns></returns>

    public int getButtonIndex();
    /// <summary>
    /// image hien thi len ui tuong trung cho skill
    /// </summary>
    /// <returns></returns>
    public string getPathOfImage();
    /// <summary>
    /// mo ta cach skill hoat dong
    /// </summary>
    /// <returns></returns>
    public string description();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleShot : MonoBehaviour, BaseSkill
{
    [SerializeField]
    GameObject MuiTen;

    public int getButtonIndex()
    {
        return 1;
    }

    public float GetCD()
    {
        return 3.5f;
    }

    public string GetName()
    {
        return "MultipleShot";
    }

    public bool IsActive()
    {

        return false;
    }
    [SerializeField]
    [Header("Phai la so chan,>0")]
    int baseArrowAmount = 20;
    [SerializeField]
    [Header("Goc co ban co the ban ra,<=360&&>0")]
    float totalAngle = 360;
    public void RunSkill(GameObject character)
    {
        Vector3 spawnPosition = character.transform.Find("WeaponParent").Find("Weapon").transform.position;
        Vector3 diff = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").transform.position);
        float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion baseAngle = Quaternion.Euler(0, 0, curAngle);
        float diffAngle = totalAngle / baseArrowAmount;
        for (int i = 0; i < baseArrowAmount/2; i++)
        {
            GameObject a = Instantiate(MuiTen, spawnPosition, Quaternion.identity);
            a.transform.rotation = Quaternion.Euler(0, 0, baseAngle.eulerAngles.z + (i + 1) * diffAngle);
            a.GetComponent<MuiTenScript>().atk = Mathf.RoundToInt(character.GetComponent<CharacterStatus>().Atk * 1.5f);
            GameObject b = Instantiate(MuiTen, spawnPosition, Quaternion.identity);
            b.transform.rotation = Quaternion.Euler(0, 0, baseAngle.eulerAngles.z - (i + 1) * diffAngle);
            b.GetComponent<MuiTenScript>().atk = Mathf.RoundToInt(character.GetComponent<CharacterStatus>().Atk * 1.5f);
        }
    }

    public void SupportUISkill(GameObject character)
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Bow/MultipleShot";
    }

    public string description()
    {
        return "Summon 25 arrows and aim at the targets closest to them.";
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        return 2;
    }

    public string GetName()
    {
        return "MultipleShot";
    }

    public bool IsActive()
    {

        if (true)//co the check 1 so dieu kien gi do o day nhu lv gioi han cac kieu, boss dac thu
        {
            return true;
        }
    }
    [SerializeField]
    [Header("Phai la so chan,>0")]
    int baseArrowAmount = 20;
    [SerializeField]
    [Header("Goc co ban co the ban ra,<=360&&>0")]
    float totalAngle = 360;
    public void RunSkill(GameObject character)
    {
        Vector3 diff = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("Square").Find("Weapon").transform.position);
        float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion baseAngle = Quaternion.Euler(0, 0, curAngle);
        float diffAngle = totalAngle / baseArrowAmount;
        for (int i = 0; i < baseArrowAmount/2; i++)
        {
            GameObject a = Instantiate(MuiTen, new Vector3(character.transform.position.x, character.transform.position.y , character.transform.position.z)
                , Quaternion.identity);
            a.transform.rotation = Quaternion.Euler(0, 0, baseAngle.eulerAngles.z + (i + 1) * diffAngle);
            //a.GetComponent<Rigidbody2D>().AddForce((a.transform.rotation * new Vector3(-10, 0, 0)).normalized*5 , ForceMode2D.Impulse);
            GameObject b = Instantiate(MuiTen, new Vector3(character.transform.position.x, character.transform.position.y , character.transform.position.z)
                , Quaternion.identity);
            b.transform.rotation = Quaternion.Euler(0, 0, baseAngle.eulerAngles.z - (i + 1) * diffAngle);
            //b.GetComponent<Rigidbody2D>().AddForce((b.transform.rotation * new Vector3(-10, 0, 0)).normalized * 5, ForceMode2D.Impulse);
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
        return "b??n ra 1 tru?m mu?i tên t?? truy ?uô?i, nh??m t??i mu?c tiêu gâ?n ca?c mu?i tên nhâ?t";
    }
}

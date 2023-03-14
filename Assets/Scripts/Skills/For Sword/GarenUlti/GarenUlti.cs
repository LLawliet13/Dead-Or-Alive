using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarenUlti : MonoBehaviour, BaseSkill
{
    public string description()
    {
        return "Trieu hoi 1 thanh kiem khong lo roi xuong gay sat thuong 1 vung chi dinh";
    }

    public int getButtonIndex()
    {

        return 2;

    }

    public float GetCD()
    {
        return 2;
    }

    public string GetName()
    {
        return "GarenUlti";
    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Sword/GarenUlti";
    }

    public bool IsActive()
    {
        return false;
    }
    public GameObject garenSword;
    public void RunSkill(GameObject character)
    {

        if (character != null)
        {

            GameObject weapon = character.transform.Find("WeaponParent").Find("Weapon").gameObject;
            Vector3 targetPosition = 2 * weapon.transform.position - character.transform.position + new Vector3(0, garenSword.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            GarenSword a = Instantiate(garenSword, new Vector3(targetPosition.x, targetPosition.y + 10, targetPosition.z), Quaternion.identity).GetComponent<GarenSword>();
            a.Trigger(targetPosition);
            a.atk = character.GetComponent<CharacterStatus>().Atk * 3;
        }
    }

    public void SupportUISkill(GameObject character)
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

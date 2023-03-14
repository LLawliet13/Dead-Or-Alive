using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarenUlti : MonoBehaviour, BaseSkill
{
    public string description()
    {
        return "triê?u hô?i 1 thanh kiê?m to tô? bô?, gia?ng xuô?ng ?â?t gây sa?t th??ng 1 vu?ng";
    }

    public int getButtonIndex()
    {

        return 2;

    }

    public float GetCD()
    {
        return 1;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {

            GameObject weapon = player.transform.Find("WeaponParent").Find("Weapon").gameObject;
            Vector3 targetPosition = 2 * weapon.transform.position - player.transform.position + new Vector3(0, garenSword.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            GameObject a = Instantiate(garenSword, new Vector3(targetPosition.x, targetPosition.y + 10, targetPosition.z), Quaternion.identity);

            a.GetComponent<GarenSword>().Trigger(targetPosition);
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

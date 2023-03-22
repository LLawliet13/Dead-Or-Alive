using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeShot : MonoBehaviour, BaseSkill
{
    [SerializeField]
    private GameObject blackholeBullet;
    public string description()
    {
        return "Shoot a bullet that can summon a black hole. The black hole will swept monsters(except boss), inflict damage, and disable their abilities(except boss)";
    }

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
        return "BlackholeShot";
    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Bow/AurelionSolE";
    }

    public bool IsActive()
    {
        return false;
    }
    public float atk;
    public void RunSkill(GameObject character)
    {
        Vector3 diff = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").position);
        float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        GameObject b = Instantiate(blackholeBullet, character.transform.position, Quaternion.Euler(0, 0, curAngle));
        b.GetComponent<BlackholeBullet>().SetAtk(character.GetComponent<CharacterStatus>().Atk);
        Vector3 moveVector = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").transform.position);
        b.GetComponent<BlackholeBullet>().setMoveVector(moveVector);
    }

    public void SupportUISkill(GameObject character)
    {
        throw new System.NotImplementedException();
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

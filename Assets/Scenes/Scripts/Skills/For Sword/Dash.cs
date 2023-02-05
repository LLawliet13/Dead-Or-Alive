using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Dash : MonoBehaviour, BaseSkill
{
    public string description()
    {
        return "Dam vao quai theo huong nhan vat";
    }

    public int getButtonIndex()
    {
        return 1;
    }

    public float GetCD()
    {
        return 5;
    }

    public string GetName()
    {
        return "Dash";
    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Sword/Dash";
    }

    public bool IsActive()
    {
        return true;
    }
    bool moveCharacter;
    GameObject character;
    Vector3 moveVector;
    [SerializeField]
    float DashSpeed;
    public void RunSkill(GameObject character)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Dashing(player);
        }

    }

    public void Dashing(GameObject character)
    {
        moveVector = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").transform.position);
        character.transform.position += moveVector * DashSpeed * Time.deltaTime;
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

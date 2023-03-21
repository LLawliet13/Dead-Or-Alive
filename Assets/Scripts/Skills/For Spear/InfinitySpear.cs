using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfinitySpear : MonoBehaviour, BaseSkill
{
    [SerializeField]
    private GameObject spear;
    [SerializeField]
    private float force = 20f;
    private Vector3 diff;
    GameObject character;
    float timeToStopFire;
    [Header("Thoi gian cast skill")]
    public float duration = 5;
    double time;
    [Header("Thoi gian delay de goi ham update")]
    public float timeDelay = 0.03f;
    public string description()
    {
        return "bbbbbbbbbbbbbb";
    }

    public int getButtonIndex()
    {
        return 1;
    }

    public float GetCD()
    {
        return 0;
    }

    public string GetName()
    {
        return "InfinitySpear";
    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Spear/InfinitySpear";
    }

    public bool IsActive()
    {
        return false;
    }

    public void RunSkill(GameObject character)
    {
        timeToStopFire = Time.time + duration;
        this.character = character;
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
        /*time = Time.time + 0.03;
        if (time >= timeToStopFire)
        {
            fire = false;
        }
        if (fire)
        {
            diff = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").position);
            float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            GameObject infSpear = Instantiate(spear, character.transform.position, Quaternion.Euler(0, 0, curAngle - 45));
            Rigidbody2D rb = infSpear.GetComponent<Rigidbody2D>();
            rb.AddForce(diff.normalized * force, ForceMode2D.Impulse);
            count++;
            Debug.Log(count);
        }*/
        time = time + 0.1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;
            if(Time.time < timeToStopFire)
            {
                diff = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("WeaponParent").Find("Weapon").position);
                float anglex = Random.Range(-0.5f, 0.5f);
                float angley = Random.Range(-0.5f, 0.5f);
                Vector3 direc = new Vector3(diff.x + anglex, diff.y + angley, diff.z);
                float curAngle = Mathf.Atan2(direc.y, direc.x) * Mathf.Rad2Deg;
                GameObject infSpear = Instantiate(spear, character.transform.position, Quaternion.Euler(0, 0, curAngle));
                infSpear.GetComponent<OutRange>().atk = Mathf.RoundToInt(character.GetComponent<CharacterStatus>().Atk * 1.5f);
                Rigidbody2D rb = infSpear.GetComponent<Rigidbody2D>();
                rb.AddForce(direc.normalized * force, ForceMode2D.Impulse);
                //Debug.Log("////////////////////////////////////////////////////////////////////////////////" + infSpear.GetComponent<MuiTenScript>().atk);
            }
        }
    }
}

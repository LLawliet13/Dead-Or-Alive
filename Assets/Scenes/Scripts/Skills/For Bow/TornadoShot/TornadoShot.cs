using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoShot : MonoBehaviour, BaseSkill
{
    public int getButtonIndex()
    {
        return 1;
    }

    public float GetCD()
    {
        return 3;
    }

    public string GetName()
    {
        return "TornadoShot";
    }

    public bool IsActive()
    {
        return true;
    }
    bool moveCharacter;
    GameObject character;
    public void RunSkill(GameObject character)
    {
        moveCharacter = true;
        this.character = character;
        currentWeaponVector = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("Weapon").transform.position);
    }
    Vector3 moveVector;
    [SerializeField]
    float moveSpeed;

    public void MoveCharacter(GameObject character)
    {
        moveVector = MovementSetting.CalculateMoveVector(character.transform.position, character.transform.Find("Weapon").transform.position);
        character.transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public void SupportUISkill(GameObject character)
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    float endMoveTime;
    [SerializeField]
    GameObject arrow;
    Vector3 currentWeaponVector;
    [SerializeField]
    float numberOfRotation;
    // Update is called once per frame
    float targetEdge = 0;
    float nextSpawnTime = 0;
    float duration = 5;
    void Update()
    {
        if (moveCharacter)
        {
            if (endMoveTime > Time.time)
            {
                targetEdge +=  numberOfRotation*360* Time.deltaTime/duration;
                MoveCharacter(character);
                if (Time.time > nextSpawnTime )
                {
                    GameObject arrowSpawn = Instantiate(arrow, character.transform.position, Quaternion.Euler(0,0, targetEdge));
                    arrowSpawn.GetComponent<TornadoArrow>().SetVector(CaculateVectorB(currentWeaponVector, targetEdge));
                    nextSpawnTime = Time.time + 2* Time.deltaTime;
                }
            }
            else
            {
                moveCharacter = false;
            }
        }
        else
        {
            //chua di chuyen thi se tinh thoi gian
            targetEdge = 0;
            endMoveTime = Time.time + duration;
            nextSpawnTime = 0;
        }
    }
    Vector3 CaculateVectorB(Vector3 a, float angle)
    {
        Quaternion q = Quaternion.Euler(0, 0, angle);


        return (q * a);
    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Bow/TornadoShot";
    }

    public string description()
    {
        return "ki? n?ng cho phe?p ng???i ch?i di chuyê?n nhanh h?n bi?nh th???ng va? liên tu?c b??n ra dagger theo vo?ng tro?ng";
    }
}

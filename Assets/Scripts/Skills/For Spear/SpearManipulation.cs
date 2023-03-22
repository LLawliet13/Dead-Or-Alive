using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpearManipulation : MonoBehaviour, BaseSkill
{
    [SerializeField]
    GameObject flySpear;
    [SerializeField]
    [Header("Thoi gian spawn")]
    float durationToSpawn = 0.5f;
    [Header("Thoi gian cast skill")]
    public float duration = 5;
    double timeToStopSpawn;
    Timer spawnTimer;
    [SerializeField]
    [Header("Spawn point")]
    GameObject spearHole;
    GameObject spawnPoint;
    GameObject character;
    public string description()
    {
        return "Summons spears that fly around the character";
    }

    public int getButtonIndex()
    {
        return 2;
    }

    public float GetCD()
    {
        return 15;
    }

    public string GetName()
    {
        return "SpearManipulation";
    }

    public string getPathOfImage()
    {
        return "Sprites/Skills/For Spear/SpearHole";
    }

    public bool IsActive()
    {
        return false;
    }

    public void RunSkill(GameObject character)
    {
        this.character = character;
        float xChar = character.transform.position.x;
        float yChar = character.transform.position.y + 1.2f;
        spawnPoint = Instantiate<GameObject>(spearHole, new Vector3(xChar, yChar), Quaternion.identity);
        spawnPoint.transform.parent = character.transform;

        spawnTimer = character.AddComponent<Timer>();
        spawnTimer.Duration = durationToSpawn;
        spawnTimer.Run();

        timeToStopSpawn = Time.time + duration;
        trigger = true;
    }

    public void SupportUISkill(GameObject character)
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    bool trigger = false;
    // Update is called once per frame
    void Update()
    {
        GameObject spear = null;
        if (trigger)
        {
            if (Time.time <= timeToStopSpawn)
            {
                float xSpawn = spawnPoint.transform.position.x;
                float ySpawn = spawnPoint.transform.position.y;
                if (spawnTimer.Finished)
                {
                    spear = Instantiate<GameObject>(flySpear, new Vector3(xSpawn, ySpawn), Quaternion.identity);
                    spear.GetComponent<SpearMovement>().atk = Mathf.RoundToInt(character.GetComponent<CharacterStatus>().Atk * 1.5f);
                    spear.GetComponent<SpearMovement>().TimeToGoBack = Time.time + duration;

                    spawnTimer.Run();
                }
            }
            else
            {
                GameObject[] spearList = GameObject.FindGameObjectsWithTag("FlySpear");
                if (spearList.Length == 0)
                {
                    Destroy(spawnPoint);
                    trigger = !trigger;
                }
            }
        }

    }
}

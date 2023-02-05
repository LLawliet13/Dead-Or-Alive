using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject flySpear;
    [SerializeField]
    [Header("Thoi gian spawn")]
    float durationToSpawn = 2;
    Timer spawnTimer;
    [SerializeField]
    GameObject spawnPoint;
    float x , y;

    List<GameObject> spears = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = durationToSpawn;
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        x = spawnPoint.transform.position.x;
        y = spawnPoint.transform.position.y;
        if (spawnTimer.Finished)
        {
            GameObject creep = Instantiate<GameObject>(flySpear, new Vector3(x, y), Quaternion.identity);
            spears.Add(creep);
            spawnTimer.Run();
        }
    }
}

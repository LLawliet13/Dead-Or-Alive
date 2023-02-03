using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRange : MonoBehaviour
{
    [SerializeField]
    private GameObject spear;
    [SerializeField]
    private float range;
    private GameObject player;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 spearPos = spear.transform.position;
        Vector2 playerPos = player.transform.position;
        distance = Vector2.Distance(spearPos, playerPos);
        if(distance >= range)
        {
            Destroy(spear);
        }
    }
}

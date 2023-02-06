using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    static List<float> m_Range = new List<float>()
    {
        10,20,30,40,50,60
    };
    public static float Range(int index)
    {
        return m_Range[index-1];
    }
}

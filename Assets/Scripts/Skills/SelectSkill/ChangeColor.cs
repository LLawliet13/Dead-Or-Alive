
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private bool click = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor1()
    {
        if (click == false)
        {
            transform.GetComponent<Image>().color = Color.red;
            click = true;
        }
        else
        {
            transform.GetComponent<Image>().color = Color.yellow;
            click = false;
        }
    }
}

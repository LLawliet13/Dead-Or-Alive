using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, BaseSkill
{
    public string description()
    {
        return "Dam vao quai theo huong nhan vat";
    }

    public int getButtonIndex()
    {
        return 4;
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
        throw new System.NotImplementedException();
    }

    public bool IsActive()
    {
        throw new System.NotImplementedException();
    }

    public void RunSkill(GameObject character)
    {
        throw new System.NotImplementedException();
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

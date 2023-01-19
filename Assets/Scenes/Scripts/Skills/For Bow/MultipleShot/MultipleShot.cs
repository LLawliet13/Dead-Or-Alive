using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleShot : MonoBehaviour,BaseSkill
{
    [SerializeField]
    GameObject MuiTen;

    public int getButtonIndex()
    {
        return 1;
    }

    public float GetCD()
    {
        return 2;
    }

    public string GetName()
    {
        return "MultipleShot";
    }

    public bool IsActive()
    {
       
        if (true)//co the check 1 so dieu kien gi do o day nhu lv gioi han cac kieu, boss dac thu
        {
            return true;
        }
    }

    public void RunSkill(GameObject character)
    {
        for(int i = 0; i < 10; i++)
        Instantiate(MuiTen, new Vector3(character.transform.position.x, character.transform.position.y-5+i, character.transform.position.z),Quaternion.identity);
        //neu nhan vat co animation thi lam nhu sau
        //dung bo comment dong nay vi t k co animation
        //Animator an = character.GetComponent<Animator>();
        //an.Play("name of animation");

    }

    public void SupportUISkill(GameObject character)
    {
        return;
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

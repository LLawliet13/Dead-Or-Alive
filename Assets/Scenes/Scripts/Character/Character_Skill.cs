using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Character_Skill : MonoBehaviour
{
    // Co the them 2 list skill rieng la skill chu dong va skill bi dong rieng
    // , tam thoi o day chi add skill chu dong
    LinkedList<UnityEvent<GameObject>> skills;
    KeyCode[] buttons = { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E };
    // Start is called before the first frame update
    void Awake()
    {
        //danh sach cach hoat dong cua cac skill
        skills = new LinkedList<UnityEvent<GameObject>>();
    }
    //ref:https://stackoverflow.com/questions/489317/how-to-pass-an-arbitrary-number-of-parameters-in-c-sharp
    public void AddSkillListener(params UnityAction<GameObject>[] action)
    {
        if (skills.Count > 5) throw new System.Exception("So luong skill duoc su dung vuot qua gioi han");
        UnityEvent<GameObject> unityEvent = new UnityEvent<GameObject>();
        foreach (UnityAction<GameObject> unityAction in action)
            unityEvent.AddListener(unityAction);
        skills.AddLast(unityEvent);

    }
    // Update is called once per frame
    void Update()
    {
        SkillController();
    }
    //co the sua doi trong tuong lai vi game tren dien thoai dieu khien bang cach an phim
    void SkillController()
    {


        for (int i = 0; i < skills.Count; i++)
        {
            //dua vao cac phim dc an ma kich hoat skill da duoc dang ki truoc do
            if (Input.GetKeyDown(buttons[i]))
            {
                //Debug.Log("clicked");
                skills.ElementAt(i).Invoke(gameObject);
            }
        }
    }
    // dung neu sau nay muon cho setting phim vao UI !OPTIONAL
    public void setButtons(KeyCode[] buttons)
    {
        this.buttons = buttons;
    }
    
}

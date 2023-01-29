using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    List<Button> buttons;
    void Start()
    {
        skills = new LinkedList<UnityEvent<GameObject>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Button1Onclick()
    {
        skills.ElementAt(0).Invoke(GameObject.FindGameObjectWithTag("Player"));
    }
    public void Button2Onclick()
    {
        skills.ElementAt(1).Invoke(GameObject.FindGameObjectWithTag("Player"));
    }
    public void Button3Onclick()
    {
        skills.ElementAt(2).Invoke(GameObject.FindGameObjectWithTag("Player"));
    }
    LinkedList<UnityEvent<GameObject>> skills;
    public void AddSkillListener(string imageSkill,params UnityAction<GameObject>[] action)
    {
        if (skills.Count > 5) throw new System.Exception("So luong skill duoc su dung vuot qua gioi han");
        UnityEvent<GameObject> unityEvent = new UnityEvent<GameObject>();
        foreach (UnityAction<GameObject> unityAction in action)
            unityEvent.AddListener(unityAction);
        skills.AddLast(unityEvent);
        //"Sprites/Skills/For Bow/NameOfImage"
        //ref: https://docs.unity3d.com/ScriptReference/Resources.Load.html
        buttons.ElementAt(skills.Count - 1).GetComponent<Image>().sprite = Resources.Load<Sprite>(imageSkill);
    }
}

using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseControlPlayer : BaseSkillBoss
{
    public override bool AbleToTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return false;
        character_Movement = player.GetComponent<Character_Movement>();
        endEffectTime = Time.time + 10;
        return true;
    }

    private float endEffectTime;
    Character_Movement character_Movement;

    public override float CD_Skill()
    {
        return 25;
    }


    public override int LVToUse()
    {
        return 10;
    }

    public override bool RangeSkill(Vector3 position)
    {
        return true;
    }



    public override void UpdateSkillBaseOnCharacterLv()
    {
    }

    protected override void SetAtkSkill()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        firstTimeUse = Time.time + 5;
        joystick = GameObject.Find("CanvasJoyStick");
        sr = transform.Find("Effect").GetComponent<SpriteRenderer>();
    }



    public override void UpdateState()
    {

        if (Time.time < endEffectTime)
        {
            joystick.transform.Find("imgJoystickBg").GetComponent<Image>().color = Color.red;
            joystick.transform.Find("imgJoystickBg").Find("imgJoystick").GetComponent<Image>().color = Color.red;
            sr.sprite = Resources.Load<Sprite>("Sprites/Boss/Skill/ReverseEffect");
            character_Movement.heSoInputX = -1;
            character_Movement.heSoInputY = -1;
        }
        else
        {
            joystick.transform.Find("imgJoystickBg").GetComponent<Image>().color = Color.white;
            joystick.transform.Find("imgJoystickBg").Find("imgJoystick").GetComponent<Image>().color = Color.white;

            sr.sprite = null;
            character_Movement.heSoInputX = 1;
            character_Movement.heSoInputY = 1;
            ExitState();

        }
    }
    GameObject joystick;
    SpriteRenderer sr;
    private void OnDisable()
    {

        try { 
        joystick.transform.Find("imgJoystickBg").GetComponent<Image>().color = Color.white;
        joystick.transform.Find("imgJoystickBg").Find("imgJoystick").GetComponent<Image>().color = Color.white;
        sr.sprite = null;
        character_Movement.heSoInputX = 1;
        character_Movement.heSoInputY = 1;
        }
        catch
        {
            Debug.Log("Game Stop");
        }

    }
}

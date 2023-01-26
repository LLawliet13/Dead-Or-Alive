using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public int speed;
    public int rotationSpeed;
    public GameObject _charController;
    private ManageJoystick _mngrJoystick;
    private float inputX, inputY;
    // Start is called before the first frame update
    void Start()
    {
        _mngrJoystick = GameObject.Find("imgJoystickBg").GetComponent<ManageJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = _mngrJoystick.inputHorizontal();
        inputY = _mngrJoystick.inputVertical();
        //char move
        if(inputX != 0 && inputY != 0)
        {
            Vector2 movementDirection = new Vector2(inputX, inputY);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            movementDirection.Normalize();
            transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
    }
}

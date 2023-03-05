using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public int speed;
    public int rotationSpeed;
    public GameObject weaponParent;
    public GameObject playerSprite;
    private ManageJoystick _mngrJoystick;
    private float inputX, inputY;
    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        _mngrJoystick = GameObject.Find("imgJoystickBg").GetComponent<ManageJoystick>();
        if(playerSprite == null || weaponParent == null)
        {
            playerSprite = GameObject.FindGameObjectWithTag("playerSprite");
            weaponParent = GameObject.FindGameObjectWithTag("weaponParent");
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputX = _mngrJoystick.inputHorizontal();
        inputY = _mngrJoystick.inputVertical();
        //char move
        if(inputX != 0 && inputY != 0)
        {
            if (inputX > 0 && !facingRight)
            {
                Flip();
            }
            else if (inputX < 0 && facingRight)
            {
                Flip();
            }
            Vector2 movementDirection = new Vector2(inputX, inputY);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            movementDirection.Normalize();
            transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            //----------
            Quaternion fixRotation = Quaternion.Euler(0, 0, toRotation.eulerAngles.z + 90);
            weaponParent.transform.rotation = Quaternion.RotateTowards(weaponParent.transform.rotation, fixRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = playerSprite.transform.localScale;
        theScale.x *= -1;
        playerSprite.transform.localScale = theScale;
    }
}

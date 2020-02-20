using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float Horizontal;
    public FloatingJoystick floatingJoystick;
    public bool readyToClear;
    private bool isJumpPressed;

    public bool GetIsJumpPressed() => isJumpPressed;

    public void SetIsJumpPressedTrue()
    {
        Debug.Log(isJumpPressed);
        isJumpPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (GameManager.IsGameOver())
        //return;
        //ProcessInputs();
        Horizontal = floatingJoystick.Horizontal;
        Horizontal = Mathf.Clamp(Horizontal, -1f, 1f);

    }

    public void ClearInput()
    {
        if (!readyToClear)
            return;
        //reset inputs
        Horizontal = 0f;
        isJumpPressed = false;
        readyToClear = false;

    }
    void ProcessInputs()
    {
       Horizontal = Input.GetAxis("Horizontal");
       isJumpPressed = GetIsJumpPressed() || Input.GetButtonDown("Jump");
    }
    void ProcessTouchInputs()
    {
    }
}

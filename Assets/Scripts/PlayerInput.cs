using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontal;
    public bool jumppressed;
    public bool jumpheld;
    bool readyToClear;

    // Update is called once per frame
    void Update()
    {
        ClearInput();
        //if (GameManager.IsGameOver())
            //return;
        ProcessInputs();
        ProcessTouchInputs();
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);


    }
    private void FixedUpdate()
    {
        //This ensure that all code gets to use the current inputs
        readyToClear = true;
    }
    void ClearInput()
    {
        if (!readyToClear)
            return;
        //reset inputs
        horizontal = 0f;

        jumpheld = false;
        jumppressed = false;
        readyToClear = false;

    }
    void ProcessInputs()
    {
        horizontal += Input.GetAxis("Horizontal");

        jumppressed = jumppressed || Input.GetButtonDown("Jump");
        jumpheld = jumpheld || Input.GetButtonDown("Jump");


    }
    void ProcessTouchInputs()
    {


    }
}

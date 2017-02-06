using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    private string verticalAxis = "Vertical";
    private string horizontalAxis = "Horizontal";
    private string jumpButton = "Jump";
    private PlayerController pc;
    private float vValue;
    private float hValue;
    private bool jump;

    // Use this for initialization
    void Start () {
        vValue = 0f;
        hValue = 0f;
        jump = false;
        pc = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        vValue = Input.GetAxis(verticalAxis);
        hValue = Input.GetAxis(horizontalAxis);
        jump = Input.GetButton(jumpButton);
	}

    void FixedUpdate() {
        Vector3 direction;
        direction = (Vector3.forward * vValue + Vector3.right * hValue)* Time.deltaTime;
        pc.Move(direction);
        if (jump)
            pc.Jump();
    }
}

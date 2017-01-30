using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    private string verticalAxis = "Vertical";
    private string horizontalAxis = "Horizontal";
    private PlayerController pc;
    private float vValue;
    private float hValue;

    public float speedMult;


    // Use this for initialization
    void Start () {
        vValue = 0f;
        hValue = 0f;
        pc = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        vValue = Input.GetAxis(verticalAxis);
        hValue = Input.GetAxis(horizontalAxis);
	}

    void FixedUpdate() {
        Vector3 direction;
        direction = (Vector3.forward * vValue + Vector3.right * hValue) * speedMult * Time.deltaTime;
        pc.Move(direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteraction : MonoBehaviour {

    public float rotateSpeedRatio;

    public float jumpPower;

    private static float fullRotate = 360f;

    private float rotation;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rotation = 0f;
	}

    void FixedUpdate()
    {
        float rotationOffset = fullRotate * rotateSpeedRatio * Time.deltaTime;
        rotation += rotationOffset;
        if (rotation > fullRotate)
        {
            Jump();
            rotation = rotation % fullRotate;
        }
        Quaternion rota = Quaternion.Euler(rb.rotation.eulerAngles + Vector3.up * rotationOffset);
        rb.MoveRotation(rota);
    }

    private void Jump()
    {
        rb.velocity.Set(rb.velocity.x, rb.velocity.y + jumpPower, rb.velocity.z);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpAngle;
    public float minimumJump;
    public float originalJumpRatio;

    private bool canJump = true;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(Vector3 direction)
    {
        direction.Set(direction.x, rb.velocity.y, direction.z);
        rb.velocity = direction;
        Vector3 rota = direction.z * Vector3.forward + direction.x * Vector3.right;
        if (rota != Vector3.zero) {
            Quaternion newRotation = Quaternion.LookRotation(rota);
            rb.MoveRotation(newRotation);
        }
    }

    public void Jump()
    {
        Debug.Log(canJump);
        if (canJump)
        {
            canJump = false;
            rb.velocity += Vector3.up * getYVelocity();
            //rb.velocity.Set(rb.velocity.x, 10f, rb.velocity.z);
        }
    }

    private float getYVelocity()
    {
        return Mathf.Max(Vector3.Magnitude(rb.velocity) * Mathf.Tan(Mathf.Deg2Rad * jumpAngle)*originalJumpRatio, minimumJump);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")){
            if(rb.velocity.y == 0)
            {
                canJump = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }
}

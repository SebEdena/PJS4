using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //public float jumpAngle;
    //public float minimumJump;
    //public float originalJumpRatio;
    public float jumpPower;
    public float speedMult;

    private bool canJump = true;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 direction)
    {
        direction *= speedMult;
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
		if (canJump) {
			rb.velocity += Vector3.up * getYVelocity ();
			if (!Mathf.Approximately (rb.velocity.y, 0f))
				canJump = false;
			//rb.velocity.Set(rb.velocity.x, 10f, rb.velocity.z);
		}
    }

    private float getYVelocity()
    {
        return jumpPower;
        //return Mathf.Max(Vector3.Magnitude(rb.velocity) * Mathf.Tan(Mathf.Deg2Rad * jumpAngle)*originalJumpRatio, minimumJump);
    }

	public void Die(){
		//gameObject.SetActive (false);
		Debug.Log ("Vous êtes morts");
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            //Debug.Log("You can jump bro");s
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
			if (Mathf.Approximately (rb.velocity.y, 0f)) {
				canJump = true;
			}
        }
    }
}

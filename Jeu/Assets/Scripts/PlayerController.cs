using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxRotation;
    public float speedReduction;

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
}

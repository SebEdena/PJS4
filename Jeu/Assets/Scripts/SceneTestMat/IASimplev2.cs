using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASimplev2 : MonoBehaviour {

	private Rigidbody rb;

	private Vector3 initial;

	public float pathLength;

	public float speed;

    public float jumpPower;

    private float rotation;

	private int direction;

	private bool canJump = false;

	void Awake(){}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		initial = transform.position;
		direction = 1;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.z < initial.z || transform.position.z > (initial.z + pathLength)) {
			rotation += 180f * Time.deltaTime;
			if (rotation > 180f) {
				float diff = 180f - rotation;

				Quaternion rota = Quaternion.Euler(rb.rotation.eulerAngles.x, 
					rb.rotation.eulerAngles.y + (180f * Time.deltaTime) - diff, 
					rb.rotation.eulerAngles.z);
				rb.MoveRotation (rota);
				rotation = 0f;
				direction *= (-1);
				transform.position += Vector3.forward * direction * speed * Time.deltaTime;
			} else
			{
				Quaternion rota = Quaternion.Euler(rb.rotation.eulerAngles.x,
					rb.rotation.eulerAngles.y + (180f * Time.deltaTime),
					rb.rotation.eulerAngles.z);
				rb.MoveRotation(rota);
			}
		} else {
			transform.position += Vector3.forward * direction * speed * Time.deltaTime;
			Jump ();
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Player")){
			col.gameObject.GetComponent<PlayerController>().Die();
		}
		if (col.gameObject.CompareTag("Floor"))
		{
			canJump = true;
		}
	}

	public void Jump()
	{
		if (canJump) {
			rb.velocity += Vector3.up * getYVelocity ();
			if (!Mathf.Approximately (rb.velocity.y, 0f))
				canJump = false;
		}
	}

	private float getYVelocity()
	{
		return jumpPower;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity;
		if (Input.GetAxisRaw("Horizontal") > 0f) {
			velocity = new Vector3(moveSpeed, rigidBody.velocity.y, 0f);
			rigidBody.velocity = velocity; 
		} else if (Input.GetAxisRaw("Horizontal") < 0f) {
			velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, 0f);
			rigidBody.velocity = velocity; 
		} else {
			velocity = new Vector3(0f, rigidBody.velocity.y, 0f);
			rigidBody.velocity = velocity;
		}
	}
}

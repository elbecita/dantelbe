using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;

	// Variables to check that the user is on the ground
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool isGrounded;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		// OverlapCircle checks if a collider falls within a circular area (in this case groundCheck
		// is at the feet of the Player)
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		Vector3 newVelocity;
		if (Input.GetAxisRaw("Horizontal") > 0f) {
			newVelocity = new Vector3(moveSpeed, rigidBody.velocity.y, 0f);
			rigidBody.velocity = newVelocity; 
		} else if (Input.GetAxisRaw("Horizontal") < 0f) {
			newVelocity = new Vector3(-moveSpeed, rigidBody.velocity.y, 0f);
			rigidBody.velocity = newVelocity; 
		} else {
			newVelocity = new Vector3(0f, rigidBody.velocity.y, 0f);
			rigidBody.velocity = newVelocity;
		}

		// Only jump if the Player is on the ground
		if (Input.GetButtonDown("Jump") && isGrounded) {
			newVelocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0f);
			rigidBody.velocity = newVelocity;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public float doubleJumpSpeed;

	// Variables to check that the user is on the ground
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool isGrounded;

	public bool isInPet;
	private bool canDoubleJump;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		canDoubleJump = isInPet;
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
			// Set localScale so the player faces the direction she's moving
			transform.localScale = new Vector3(1f, 1f, 1f);
		} else if (Input.GetAxisRaw("Horizontal") < 0f) {
			newVelocity = new Vector3(-moveSpeed, rigidBody.velocity.y, 0f);
			rigidBody.velocity = newVelocity; 
			transform.localScale = new Vector3(-1f, 1f, 1f);
		} else {
			newVelocity = new Vector3(0f, rigidBody.velocity.y, 0f);
			rigidBody.velocity = newVelocity;
		}

		// Only jump if the Player is on the ground or on doubleJump
		if (Input.GetButtonDown("Jump")) {
			if (isGrounded) {
				canDoubleJump = isInPet;
				newVelocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0f);
				rigidBody.velocity = newVelocity;
			} else if (canDoubleJump) {
				canDoubleJump = false;
				newVelocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0f);
				rigidBody.velocity = newVelocity;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float changeSpeedFactor;
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

	private float currentMoveSpeed;
	private bool isStopping;
	private float facingDirectionWhenStopping;
	private bool isSpeeding;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		canDoubleJump = isInPet;
		isStopping = false;
		isSpeeding = false;
		currentMoveSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		// OverlapCircle checks if a collider falls within a circular area (in this case groundCheck
		// is at the feet of the Player)
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (isStopping) {
			currentMoveSpeed *= changeSpeedFactor;
			print(">>> currentMoveSpeed: " + currentMoveSpeed);
		} else if (isSpeeding) {
			currentMoveSpeed /= changeSpeedFactor;
			if(currentMoveSpeed > moveSpeed) {
				isSpeeding = false;
				currentMoveSpeed = moveSpeed;
			}
		}

		Vector3 newVelocity;
		if (Input.GetAxisRaw("Horizontal") > 0f) {
			// If player was stopping but changes X-direction, start speeding
			if (isStopping && transform.localScale.x > facingDirectionWhenStopping) {
				isStopping = false;
				isSpeeding = true;
				facingDirectionWhenStopping = 0;
				if (currentMoveSpeed <= 0.0000001f) {
					currentMoveSpeed = changeSpeedFactor;
				} else {
					currentMoveSpeed /= changeSpeedFactor;
				}
			}
			newVelocity = new Vector3(currentMoveSpeed, rigidBody.velocity.y, 0f);
			rigidBody.velocity = newVelocity;
			// Set localScale so the player faces the direction she's moving
			transform.localScale = new Vector3(1f, 1f, 1f);
		} else if (Input.GetAxisRaw("Horizontal") < 0f) {
			// If player was stopping but changes X-direction, start speeding
			if (isStopping && transform.localScale.x < facingDirectionWhenStopping) {
				print("change of direction detected: " + currentMoveSpeed);
				isStopping = false;
				isSpeeding = true;
				facingDirectionWhenStopping = 0;
				if (currentMoveSpeed <= 0.0000001f) {
					currentMoveSpeed = changeSpeedFactor;
				} else {
					currentMoveSpeed /= changeSpeedFactor;
				}
			}
			newVelocity = new Vector3(-currentMoveSpeed, rigidBody.velocity.y, 0f);
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

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "EnergySource") {
			currentMoveSpeed = moveSpeed;
			isStopping = false;
			isSpeeding = false;
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider) {
		if (otherCollider.tag == "EnergySource") {
			facingDirectionWhenStopping = transform.localScale.x;
			isStopping = true;
			isSpeeding = false;
		}
	}
}

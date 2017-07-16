using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    const int PlayerLayerId = 9;
    const int SemiSolidLayerId = 10;

    //basic move
	public float moveSpeed;

    //jump
    public bool isInPet;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    //ground check attributes
    public Transform groundCheck;
    public float groundCheckRadius;
    public List<LayerMask> whatIsGrounds;

    //private move
    private float currentMoveSpeed;
    private bool isAirbornDirectionChanged;

    //private jump
    private float airXDirection;
    private bool canDoubleJump;
    private bool isGrounded;


    private Rigidbody2D rigidBody;

    //energy source attribute
    public float changeSpeedFactor;
    private float facingDirectionWhenStopping;
    private bool isSpeeding;
    private bool isStopping;


	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		canDoubleJump = isInPet;
        currentMoveSpeed = moveSpeed;
	}
	
	void Update () {
        // OverlapCircle checks if a collider falls within a circular area (in this case groundCheck
        // is at the feet of the Player)
        isGrounded = checkGrounded();

        SemiSolidHandler();

        BasicMovementHandler();
        JumpHandler();
	}
        
    void BasicMovementHandler() {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var velocityFactor = horizontalInput;

        if (!isGrounded) {
            if (isAirbornDirectionChanged) {
                velocityFactor *= 0.8f;
            } else {
                if (Mathf.Sign(airXDirection) != Mathf.Sign(velocityFactor)) {
                    isAirbornDirectionChanged = true;
                    velocityFactor *= 0.8f;
                }
            }
        }

        rigidBody.velocity = new Vector3(velocityFactor * currentMoveSpeed, rigidBody.velocity.y, 0f);

        if (horizontalInput != 0f) transform.localScale = new Vector3(horizontalInput, 1f, 1f);
    }

    void JumpHandler() {
        Vector3 newVelocity;

        if (!Input.GetButtonDown("Jump")) return;

        if (isGrounded) {
            isAirbornDirectionChanged = false;
            airXDirection = rigidBody.velocity.x;
            canDoubleJump = isInPet;
            newVelocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0f);
            rigidBody.velocity = newVelocity;
        } else if (canDoubleJump) {
            canDoubleJump = false;
            newVelocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0f);
            rigidBody.velocity = newVelocity;
        }
    }

    bool checkGrounded() {
        foreach (var ground in whatIsGrounds) {
            if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground)) return true;
        }

        return false;
    }

    void SemiSolidHandler() {
        if ((rigidBody.velocity.y > 0) || (!isGrounded)) {
            //IgnoreSemiSolidOnJumpingUpwards
            Physics2D.IgnoreLayerCollision(PlayerLayerId, SemiSolidLayerId);
        } else {
            //EnableSemiSolidCollision
            Physics2D.IgnoreLayerCollision(PlayerLayerId, SemiSolidLayerId, false);
        }
    }

    #region [ Energy Source ]
    void InitEnergySourceBehavior() {
        isStopping = false;
        isSpeeding = false;
        currentMoveSpeed = moveSpeed;
    }

    void UpdateEnergySourceBehavior() {
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

        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            //TODO: fix directions here based on get axis horizontal state
            if (isStopping && transform.localScale.x < facingDirectionWhenStopping)
            {
                print("change of direction detected: " + currentMoveSpeed);
                isStopping = false;
                isSpeeding = true;
                facingDirectionWhenStopping = 0;
                if (currentMoveSpeed <= 0.0000001f)
                {
                    currentMoveSpeed = changeSpeedFactor;
                }
                else
                {
                    currentMoveSpeed /= changeSpeedFactor;
                }
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
    #endregion
}

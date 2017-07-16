using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	
	public KeyCode InteractionKey = KeyCode.E;

	private bool canInteract;
	private bool hasInteracted;

	void Start() {
		canInteract = false;
		hasInteracted = false;
	}

	void Update() {
		if (Input.GetKeyDown(InteractionKey)) {
			GetInteraction();
		}
	}

	public void GetInteraction() {
		if (canInteract && !hasInteracted) {
			Interact();
			// TODO: maybe this hasInteracted need to be moved to derived classes
			// if there are interactables like dialogs that can be triggered more than once
			hasInteracted = true;
			print(">>> The interactable has interacted already, not possible again.");
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "Player") {
			print(">>> Interactable can interact, press interaction key.");
			canInteract = true;
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider) {
		if (otherCollider.tag == "Player") {
			print(">>> Interactable can NOT interact anymore.");
			canInteract = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			print(">>> Interactable can interact, press interaction key.");
			canInteract = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			print(">>> Interactable can NOT interact anymore.");
			canInteract = false;
		}
	}

	public virtual void Interact() {
		print(">>> Interact called in the base class.");
	}
}

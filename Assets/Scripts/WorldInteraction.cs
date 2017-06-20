using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteraction : MonoBehaviour {

	// TODO: every interactable will have its own trigger key and the player
	// will need to enter the collider, so right now WorldInteraction is not used
	/*void Update () {
		// IsPointerOverGameObject used to identify if we are clicking in the UI
		if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
			GetInteraction();
		}
	}

	void GetInteraction() {
		// Detect if an object has been hit
		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
		if (hit.collider != null) {
			GameObject interactedObject = hit.collider.gameObject;
			if (interactedObject.tag == "Interactable") {
				print("Interactable object clicked! Name: " + interactedObject.name);
			}
		}
	}*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float followAhead;
	public float smoothing;

	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Only move camera in the x axis
		targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

		// followAhead holds how much ahead of the target the camera will be positioned (in platform games 
		// the user is not in the center of the screen, normally you want to see ahead of the platform in the screen)
		if (target.transform.localScale.x > 0f) {
			targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
		} else {
			targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
		}

		// Multiplying by deltaTime (time in sec that took to complete last frame) we make the camera
		// movement smoothing independent of the fps the pc is running the game at
		transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
	}
}

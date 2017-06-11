using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public bool parallax, scrolling;
	public float backgroundSize;
	public float viewZone = 10f;

	private Transform cameraTransform;
	private Transform[] backgrounds;
	private int leftIndex;
	private int rightIndex;

	public float parallaxSpeed;
	public float smoothing;
	private float prevCameraX;

	void Start() {
		cameraTransform = Camera.main.transform;
		prevCameraX = cameraTransform.position.x;
		
		// Get every one of the repetaed layers that the background has
		backgrounds = new Transform[transform.childCount];
		for (int i = 0; i< transform.childCount; i++) {
			backgrounds[i] = transform.GetChild(i);
		}
		leftIndex = 0;
		rightIndex = backgrounds.Length - 1;
	}

	void LateUpdate() {
		if (parallax) {
			// Get diff between where the camera was and where is it now
			// and move the background by that much diff in the other direction
			float parallax = (cameraTransform.position.x - prevCameraX) * parallaxSpeed;
			Vector3 newPosition = new Vector3(transform.position.x - parallax, transform.position.y, transform.position.z);
			//transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);
			transform.position = newPosition;
		}

		prevCameraX = cameraTransform.position.x;

		if (scrolling) {
			// If camera falls too close to the left/right edge, scrollLeft/scrollRight
			if (cameraTransform.position.x < (backgrounds[leftIndex].transform.position.x + viewZone)) {
				ScrollLeft();
			}
			if (cameraTransform.position.x > (backgrounds[rightIndex].transform.position.x - viewZone)) {
				ScrollRight();
			}
		}
	}

	private void ScrollLeft() {
		// We move the most right layer to be the 1st in the left
		//int lastRight = rightIndex;
		print(">>> blabla scrollleft");
		backgrounds[rightIndex].position = new Vector3((backgrounds[leftIndex].position.x - backgroundSize), 0f, backgrounds[rightIndex].position.z);
		leftIndex = rightIndex;
		rightIndex--;
		if (rightIndex < 0) {
			rightIndex = backgrounds.Length - 1;
		}
	}

	private void ScrollRight() {
		// We move the most left layer to be the last in the right
		//int lastLeft = leftIndex;
		print(">>> blabla scrollright");
		backgrounds[leftIndex].position = new Vector3((backgrounds[rightIndex].position.x + backgroundSize), 0f, backgrounds[leftIndex].position.z) ;
		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == backgrounds.Length) {
			leftIndex = 0;
		}
	}
}

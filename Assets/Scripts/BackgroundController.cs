using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public float backgroundSize;
	public float viewZone = 10f;

	private Transform cameraTransform;
	private Transform[] layers;
	private int leftIndex;
	private int rightIndex;

	void Start() {
		cameraTransform = Camera.main.transform;
		
		// Get every one of the repetaed layers that the background has
		layers = new Transform[transform.childCount];
		for (int i = 0; i< transform.childCount; i++) {
			layers[i] = transform.GetChild(i);
		}
		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}

	void Update() {
		// If camera falls too close to the left/right edge, scrollLeft/scrollRight
		if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone)) {
			ScrollLeft();
		}
		if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone)) {
			ScrollRight();
		}
	}

	private void ScrollLeft() {
		// We move the most right layer to be the 1st in the left
		//int lastRight = rightIndex;
		layers[rightIndex].position = new Vector3((layers[leftIndex].position.x - backgroundSize), 0f, 1f);
		leftIndex = rightIndex;
		rightIndex--;
		if (rightIndex < 0) {
			rightIndex = layers.Length - 1;
		}
	}

	private void ScrollRight() {
		// We move the most left layer to be the last in the right
		//int lastLeft = leftIndex;
		layers[leftIndex].position = new Vector3((layers[rightIndex].position.x + backgroundSize), 0f, 1f) ;
		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == layers.Length) {
			leftIndex = 0;
		}
	}
}

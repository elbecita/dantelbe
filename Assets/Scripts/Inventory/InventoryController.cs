using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

	UsableController usableController;

	// For testing purposes
	private Item batteryLog;

	void Start() {
		usableController = GetComponent<UsableController>();
		batteryLog = new Item("battery_log", "Battery Log", "Use this to recharge the log! (:");
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.U)) {
			usableController.UseItem(batteryLog);
		}
	}
}

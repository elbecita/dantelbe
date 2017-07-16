using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

	public static InventoryController Instance { get; set; }
	UsableController usableController;

	public List<Item> PlayerItems = new List<Item>();


	// For testing purposes
	private Item batteryLog;

	void Start() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}

		usableController = GetComponent<UsableController>();
	}

	public void AddItem(string itemId) {
		var item = ItemDatabase.Instance.GetItem(itemId);
		PlayerItems.Add(item);
		print(PlayerItems.Count + " on inventory. Just added: " + item.Name);
	}

	public void UseItem(Item itemToUse) {
		usableController.UseItem(itemToUse);
	}
}

using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour {

	public static ItemDatabase Instance { get; set; }
	private List<Item> Items { get; set; }

	// Use this for initialization
	void Start () {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}
		BuildDatabase();
	}
	
	private void BuildDatabase() {
		Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
	}

	public Item GetItem(string itemId) {
		foreach (Item item in Items) {
			if (item.Id == itemId) {
				return item;
			}
		}
		print("Couldn't find item: " + itemId);
		return null;
	}
}

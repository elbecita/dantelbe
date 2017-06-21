using UnityEngine;

public class Item {
	public string Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string ActionName { get; set; } // TODO needed?

	public Item(string _id, string _name, string _description) {
		this.Id = _id;
		this.Name = _name;
		this.Description = _description;
	}
}

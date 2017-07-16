using UnityEngine;
using Newtonsoft.Json;

public class Item {
	public enum ItemType { Usable, SpeechTrigger }
	public string Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string ActionName { get; set; } // TODO needed?

	[JsonConverterAttribute(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
	public ItemType Type { get; set; }

	[Newtonsoft.Json.JsonConstructorAttribute]
	public Item(string _id, string _name, string _description, ItemType _type) {
		this.Id = _id;
		this.Name = _name;
		this.Description = _description;
		this.Type = _type;
	}
}

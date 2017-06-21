using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableController : MonoBehaviour {

	public void UseItem(Item item) {
		// In the Usables folder we will only have usables
		GameObject itemToUse = Instantiate(Resources.Load<GameObject>("Usables/" + item.Id));
		itemToUse.GetComponent<IUsable>().Use();
	}
}

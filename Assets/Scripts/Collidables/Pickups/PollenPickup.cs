using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenPickup : Pickup {
	public enum PollenType {
		Red,
		Purple,
		Blue,
        Yellow
	}

	public PollenType type;

	protected override void onPickedUp(GameObject picker){
		picker.GetComponent<Inventory>().AddPollen(this.type);
		Destroy(this.gameObject);
	}
}

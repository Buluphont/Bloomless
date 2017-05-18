using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	private int redPollen = 0;
	private int greenPollen = 0;
	private int bluePollen = 0;

	public void AddPollen(PollenPickup.PollenType type){
		AddPollen(1, type);
	}

	public void AddPollen(int amount, PollenPickup.PollenType type){
		switch(type) {
			case PollenPickup.PollenType.Red:
				redPollen += amount;
				break;
			case PollenPickup.PollenType.Green:
				greenPollen += amount;
				break;
			case PollenPickup.PollenType.Blue:
				bluePollen += amount;
				break;
			default:
				Debug.LogWarning("Inventory.AddPollen :: Unrecognized PollenType");
				break;
		}
	}

	public void RemovePollen(int amount, PollenPickup.PollenType type){
		AddPollen(-amount, type);
	}

	public int GetPollen(PollenPickup.PollenType type){
		switch(type) {
			case PollenPickup.PollenType.Red:
				return this.redPollen;
			case PollenPickup.PollenType.Green:
				return this.greenPollen;
			case PollenPickup.PollenType.Blue:
				return this.bluePollen;
			default:
				throw new System.Exception("Inventory.GetPollen :: Unrecognized PollenType");
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    // Counts
	private int redPollen = 0;
	private int purplePollen = 0;
	private int bluePollen = 0;
    private int yellowPollen = 0;

    // Texts to update
    private Text redText;
    private Text blueText;
    private Text purpleText;
    private Text yellowText;

    void Start()
    {
        GameObject inventoryTracker = GameObject.FindGameObjectWithTag("InventoryTracker");
        this.redText = inventoryTracker.transform.Find("RedPollenCount").GetComponent<Text>();
        this.blueText = inventoryTracker.transform.Find("BluePollenCount").GetComponent<Text>();
        this.purpleText = inventoryTracker.transform.Find("PurplePollenCount").GetComponent<Text>();
        this.yellowText = inventoryTracker.transform.Find("YellowPollenCount").GetComponent<Text>();
    }

	public void AddPollen(PollenPickup.PollenType type){
		AddPollen(1, type);
	}

	public void AddPollen(int amount, PollenPickup.PollenType type){
		switch(type) {
			case PollenPickup.PollenType.Red:
				redPollen += amount;
				break;
			case PollenPickup.PollenType.Purple:
				purplePollen += amount;
				break;
			case PollenPickup.PollenType.Blue:
				bluePollen += amount;
				break;
            case PollenPickup.PollenType.Yellow:
                yellowPollen += amount;
                break;
			default:
				Debug.LogWarning("Inventory.AddPollen :: Unrecognized PollenType");
				break;
		}
        updateUI();
	}

	public void RemovePollen(int amount, PollenPickup.PollenType type){
		AddPollen(-amount, type);
	}

	public int GetPollen(PollenPickup.PollenType type){
		switch(type) {
			case PollenPickup.PollenType.Red:
				return this.redPollen;
			case PollenPickup.PollenType.Purple:
				return this.purplePollen;
			case PollenPickup.PollenType.Blue:
				return this.bluePollen;
            case PollenPickup.PollenType.Yellow:
                return this.yellowPollen;
			default:
				throw new System.Exception("Inventory.GetPollen :: Unrecognized PollenType");
		}
	}

    private void updateUI()
    {
        redText.text = "x " + redPollen.ToString();
        blueText.text = "x " + bluePollen.ToString();
        purpleText.text = "x " + purplePollen.ToString();
        yellowText.text = "x " + yellowPollen.ToString();
    }
}
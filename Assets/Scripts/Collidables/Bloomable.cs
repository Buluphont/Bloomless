using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloomable : MonoBehaviour {
	public PollenPickup.PollenType type;
	public int requiredPollen;
	public GameObject bloomedFlower;

	private bool bloomed;

	void Start () {
		this.bloomed = false;
	}

	// TODO: Maybe we want to replace this collision trigger with something like a button press within proximity.
	void OnTriggerEnter(Collider other){
		if(!bloomed && other.gameObject.CompareTag("Player")){
			Inventory inventory = other.gameObject.GetComponent<Inventory>();

			if(requiredPollen <= inventory.GetPollen(type)) {
				inventory.RemovePollen(requiredPollen, type);
				onPollinate();
			}
		}
	}

	public bool HasBloomed(){
		return bloomed;
	}

	private void onPollinate(){
		bloomed = true;
		// TODO: Replace this simple rotation with model change or animation or whatever
		// and also accompanying changes in game state
		this.gameObject.SetActive(false);
		bloomedFlower.SetActive (true);
		GameObject.FindWithTag("Game State Manager").GetComponent<CheckpointController>().SetCheckpoint(bloomedFlower);
	}
}

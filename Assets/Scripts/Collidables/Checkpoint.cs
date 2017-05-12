using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	private CheckpointController checkpointController;

	void Start () {
		this.checkpointController = GameObject.FindWithTag("Game State Manager").GetComponent<CheckpointController>();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")) {
			Debug.Log("New checkpoint set");
			checkpointController.SetCheckpoint(this.gameObject);
		}
	}
}

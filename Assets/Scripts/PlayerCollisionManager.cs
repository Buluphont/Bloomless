using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour {
	private CheckpointController checkpointController;

	public void Start(){
		this.checkpointController = GameObject.FindWithTag("Game State Manager").GetComponent<CheckpointController>();
	}

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
			checkpointController.setCheckpoint(other.gameObject);
        }
    }
}

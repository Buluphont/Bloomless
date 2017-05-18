using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
	public GameObject playerPrefab;
	private GameObject lastCheckpoint;

	public void SetCheckpoint(GameObject checkpoint){
		this.lastCheckpoint = checkpoint;
	}

	public void RespawnFromLastCheckpoint(){
		var oldPos = lastCheckpoint.transform.position;
		var position = new Vector3(oldPos.x, oldPos.y + 3.0f, oldPos.z);
		GameObject clone = Instantiate(playerPrefab, position, Quaternion.identity);
		clone.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 10.0f, 0.0f));
		Camera.main.GetComponent<OrbitingCamera>().SetFocus(clone);
	}
}

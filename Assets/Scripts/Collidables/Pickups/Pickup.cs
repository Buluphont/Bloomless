using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")) {
			onPickedUp(other.gameObject);
		}
	}
	protected abstract void onPickedUp(GameObject picker);
}

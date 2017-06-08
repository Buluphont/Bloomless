using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

	public float fieldOfViewAngle = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;
	public GameObject raycast;

	private UnityEngine.AI.NavMeshAgent nav;
	private SphereCollider col;
	private GameObject player;
	private Vector3 previousLastSighting;

	void Awake(){
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
		player = GameObject.FindGameObjectWithTag ("Player");

	}

	void OnTriggerStay(Collider other){
		if (other.gameObject == player) {
			playerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			if (angle < fieldOfViewAngle * 0.5f) {
				RaycastHit hit;
				if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)){
					raycast = hit.collider.gameObject;
					if(hit.collider.gameObject == player){
						playerInSight = true;
						previousLastSighting = personalLastSighting;
						personalLastSighting = player.transform.position;
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == player)
			playerInSight = false;
	}
}

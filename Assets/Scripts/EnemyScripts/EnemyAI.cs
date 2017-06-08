using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public float patrolspeed = 2f;
	public float chaseSpeed = 5f;
	public float chaseWaitTime = 5f;
	public float patrolWaitTime = 1f;
	public Transform[] patrolWayPoints;

	private EnemySight enemysight;
	private UnityEngine.AI.NavMeshAgent nav;
	private Transform player;
	private float chasetimer;
	private float patroltimer;
	private int waypointIndex;

	void Awake(){
		enemysight = GetComponent<EnemySight> ();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update(){
		if (enemysight.playerInSight)
			Chasing ();
		else
			Patrolling ();
	}

	void Chasing(){
		Vector3 sightingDeltaPos = enemysight.personalLastSighting - transform.position;
		if (sightingDeltaPos.sqrMagnitude > 4f)
			nav.destination = enemysight.personalLastSighting;

		nav.speed = chaseSpeed;
		if (nav.remainingDistance < nav.stoppingDistance) {
			chasetimer += Time.deltaTime;
			if (chasetimer > chaseWaitTime) {
				chasetimer = 0f;
			}
		} else
			chasetimer = 0f;
	}

	void Patrolling(){
		nav.speed = patrolspeed;

		if (nav.remainingDistance < nav.stoppingDistance) {
			patroltimer += Time.deltaTime;

			if (patroltimer >= patrolWaitTime) {
				if (waypointIndex == patrolWayPoints.Length - 1)
					waypointIndex = 0;
				else
					waypointIndex++;

				patroltimer = 0f;
			}
		} 
		else
			patroltimer = 0f;

		nav.destination = patrolWayPoints[waypointIndex].position;
	}
}
	
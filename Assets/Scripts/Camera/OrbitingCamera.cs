using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingCamera : MonoBehaviour {
	#region public properties
	// Multipliers to control camera orbiting speed
	public float yawSpeed;
	public float pitchSpeed;
	// Minimum/maximum angle to clamp pitch to 
	public float pitchAngleMin;
	public float pitchAngleMax;
	// Maximum distance between camera and focus
	public float maxCameraDistance;
	#endregion

	#region private state
	// The object to follow and orbit
	private GameObject focus;

	private Vector3 offset;
	private float yaw;
	private float pitch;
	#endregion

	#region MonoBehaviour callbacks
	void Start() {
		this.SetFocus(GameObject.FindWithTag("Player"));
	}
	
	void LateUpdate() {
		UpdatePositionRelativeToFocus();
		UpdateRotationAndOrbitPosition();
	}
	#endregion

	#region public methods
	public void SetFocus(GameObject newFocus){
		this.focus = newFocus;
		var focusPosition = focus.transform.position;
		this.transform.position = focus.transform.position;
		this.transform.Translate((focus.transform.forward * -1) * (maxCameraDistance / 2));
		this.transform.Translate((focus.transform.up) * (maxCameraDistance / 2));
		this.offset = this.transform.position - focus.transform.position;
		this.yaw = this.transform.eulerAngles.x;
		this.pitch = this.transform.eulerAngles.y;
	}

	#endregion

	#region private helper methods
	private void UpdatePositionRelativeToFocus(){
		this.transform.position = focus.transform.position + offset;
	}

	private void UpdateRotationAndOrbitPosition(){
		// Calculate clamped deltas
		float yawDelta = Input.GetAxis("Mouse X") * yawSpeed;
		float pitchDelta = Input.GetAxis("Mouse Y") * pitchSpeed;
		yaw += yawDelta;
		pitch -= pitchDelta;
		pitch = Clamp(pitch, pitchAngleMin, pitchAngleMax);

		// Update camera's orbiting position relative to focus
		this.transform.position = Quaternion.Euler(pitch, yaw, 0) * (transform.position - focus.transform.position) + focus.transform.position;

		// Look at focus
		this.transform.LookAt(focus.transform);

		// Check for obstructions and reposition as necessary
		Vector3 rayDirection = this.transform.forward * -1;
		RaycastHit hit;
		if(Physics.Raycast(focus.transform.position, rayDirection, out hit, maxCameraDistance)) {
			this.transform.position = hit.point;
		}
	}

	private static float Clamp(float angle, float min, float max){
		angle = (angle % 360);
		return Mathf.Clamp(angle, min, max);
	}
	#endregion
}
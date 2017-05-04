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
		this.focus = GameObject.FindWithTag("Player");
		this.offset = this.transform.position - focus.transform.position;
		this.yaw = this.transform.eulerAngles.x;
		this.pitch = this.transform.eulerAngles.y;
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
		this.transform.position = new Vector3(focusPosition.x, focusPosition.y + 1.5f, focusPosition.z - 4f);
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

		// Update camera's orbiting position relative to focus, then look at focus
		// is there an easier way to do this? lol
		this.transform.position = Quaternion.Euler(pitch, yaw, 0) * (transform.position - focus.transform.position) + focus.transform.position;
		this.transform.LookAt(focus.transform);
	}

	private static float Clamp(float angle, float min, float max){
		angle = Mathf.Abs(angle % 360);
		return Mathf.Clamp(angle, min, max);
	}
	#endregion
}
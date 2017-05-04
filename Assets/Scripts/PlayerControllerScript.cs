using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
	public Camera trackingCamera;

	private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

	void Start(){
		controller = GetComponent<CharacterController>();
	}

    void Update()
	{
        float yVel = moveDirection.y;

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	    moveDirection = alignVectorTo(moveDirection, trackingCamera.transform);
        moveDirection *= speed;

        moveDirection += (Vector3.up * yVel);

        if (controller.isGrounded)
        {
            
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

	private Vector3 alignVectorTo(Vector3 vector, Transform target){
		// Rotate along camera axes
		vector = target.TransformDirection(vector);

		// Negate y component, restore magnitude lost from negating y component
		float originalMagnitude = vector.magnitude;
		vector.y = 0;
		vector = vector.normalized * originalMagnitude;
		return vector;
	}
}

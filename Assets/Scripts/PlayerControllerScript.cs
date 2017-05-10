using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    
	private Camera trackingCamera;

	private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private bool isglide = false;

	void Start(){
		controller = GetComponent<CharacterController>();
		this.trackingCamera = Camera.main;
		trackingCamera.GetComponent<OrbitingCamera>().SetFocus(this.gameObject);
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
            if (isglide)
                gravity = gravity * 4;
            isglide = false;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        

        }
        else
        {
            if (Input.GetButtonDown("Glide"))
            {
                if (moveDirection.y < 0 && !isglide)
                {
                    gravity = gravity / 4;
                    isglide = true;
                }
            }
            if (Input.GetButtonUp("Glide") && isglide)
            {
                gravity = gravity * 4;
                isglide = false;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
		if(Input.GetKeyDown(KeyCode.R)) {
			GameObject.FindWithTag("Game State Manager").GetComponent<CheckpointController>().respawnFromLastCheckpoint();
			Destroy(this.gameObject);
		}
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

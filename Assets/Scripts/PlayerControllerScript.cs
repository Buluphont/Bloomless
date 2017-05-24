using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public bool alive;
    public bool isDashing = false;
    private Camera trackingCamera;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private bool isglide = false;
    private float dashTimer = 0.0F;
    void Start()
    {
        alive = true;
        controller = GetComponent<CharacterController>();
        this.trackingCamera = Camera.main;
        trackingCamera.GetComponent<OrbitingCamera>().SetFocus(this.gameObject);
    }

    void Update()
    {
        float yVel = moveDirection.y;

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = alignVectorTo(moveDirection, trackingCamera.transform);
        if (moveDirection.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), 0.5f);
        }
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
            if (Input.GetButton("Glide"))
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

        if (dashTimer > 0.0F)
        {
            dashTimer -= 1.0F;
            if (dashTimer == 0.0F)
            {
                isDashing = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.B) && dashTimer == 0.0F)
        {
            isDashing = true;
            dashTimer = 10.0F;
        }

        if (isDashing)
        {
            controller.SimpleMove(transform.forward * 20);
        }
        else
        {
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    private Vector3 alignVectorTo(Vector3 vector, Transform target)
    {
        // Rotate along camera axes
        vector = target.TransformDirection(vector);

        // Negate y component, restore magnitude lost from negating y component
        float originalMagnitude = vector.magnitude;
        vector.y = 0;
        vector = vector.normalized * originalMagnitude;
        return vector;
    }
}

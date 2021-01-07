using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public static Movement MovementSystem;

    public float speed = 10f;
    public float gravity = -9.81f;

    public float jumpHeight = 3f;
    private Footsteps player_Footsteps;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isMoving;

    public Vector3 velocity;
    public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            //velocity.y = 0f;
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (move.x > 0 || move.z > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        controller.Move(move * speed * Time.deltaTime);
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    private Vector3 moveDirection;
    private float acceleration = 8f;
    private CharacterController cr;

    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;
    private Vector3 velocity;
    private bool isGrounded;

    private CapsuleCollider col;
    float crouchTime = 0f;
    private int crouchDuration = 3;
    private Vector3 originalSize;
    // Start is called before the first frame update
    void Start()
    {
        
        cr = GetComponent<CharacterController>();
        col = cr.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
        //Jump();
    }
    void InputSystem()
    {
        moveDirection = Vector3.zero;

        isGrounded = cr.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f; // Small value to keep the character grounded

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine(Crouch());
        }
        if (Input.GetKey(KeyCode.D))
            moveDirection += transform.right * acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= transform.right * acceleration * Time.deltaTime;

        velocity.y += gravity * Time.deltaTime;
        cr.Move(moveDirection + (velocity * Time.deltaTime));
    }
    IEnumerator Crouch()
    {
        // Shrink collider
        cr.height *= 0.5f; // Halve the height
        cr.center = new Vector3(originalSize.x, originalSize.y * 0.5f, originalSize.z);

        yield return new WaitForSeconds(crouchDuration); // Wait for duration

        // Reset collider to original size
        cr.height *= 2f;
        cr.center = originalSize;
    }
    
}
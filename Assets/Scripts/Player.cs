using System.Collections;
using UnityEngine;

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
    Quaternion defaultPos;
    public LayerMask obstacleLayer;

    public PlayerAnimation playerAnimation;
    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        cr = GetComponent<CharacterController>();
        col = cr.GetComponent<CapsuleCollider>();
        defaultPos = transform.rotation;
    }

    void Update()
    {
        
        playerAnimation.SetRunning(true);
        InputSystem();
    }

    void InputSystem()
    {
        moveDirection = Vector3.zero;
        isGrounded = cr.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            Jump();
            playerAnimation.Jumping(true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine(Crouch());
            playerAnimation.Sliding(true);
        }

        if (Input.GetKey(KeyCode.D))
            MoveRight();

        if (Input.GetKey(KeyCode.A))
            MoveLeft();

        ApplyGravity();
        cr.Move(moveDirection + (velocity * Time.deltaTime));
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    IEnumerator Crouch()
    {
        if (!isGrounded) yield break;
        cr.gameObject.transform.rotation = Quaternion.Euler(-90, defaultPos.y, defaultPos.z);
        yield return new WaitForSeconds(crouchDuration);
        if (!isGrounded) yield break;
        cr.gameObject.transform.rotation = Quaternion.Euler(0, defaultPos.y, defaultPos.z);
    }

    void MoveRight()
    {
        moveDirection += transform.right * acceleration * Time.deltaTime;
    }

    void MoveLeft()
    {
        moveDirection -= transform.right * acceleration * Time.deltaTime;
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("dd");
        if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("GameOver");
    }
}
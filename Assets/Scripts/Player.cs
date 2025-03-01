using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private Vector3 moveDirection;
    private float acceleration = 8f;
    private CharacterController cr;

    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;
    private Vector3 velocity;
    public bool isGrounded;

    private CapsuleCollider col;
    float crouchTime = 0f;
    private int crouchDuration = 3;
    private Vector3 originalSize;
    Quaternion defaultPos;
    private bool isSliding = false;
    public PlayerAnimation playerAnimation;
    public Enemy enemy;

    public Image h1;
    public Image h2;
    public Image h3;
    public int health = 3;
    void Start()
    {
        cr = GetComponent<CharacterController>();
        col = cr.GetComponent<CapsuleCollider>();

        playerAnimation = GetComponent<PlayerAnimation>();
        defaultPos = transform.rotation;
        enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<Enemy>();
        
    }

    void Update()
    {
        if (gameObject.transform.position.y < -1)
        {
            GameManager.instance.GameOver();
            return;
        }
        InputSystem();
        Jump();
    }

    void InputSystem()
    {
        moveDirection = Vector3.zero;
        

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetKey(KeyCode.S))
           StartCoroutine(Crouch());

        if (Input.GetKey(KeyCode.D))
            MoveRight();

        if (Input.GetKey(KeyCode.A))
            MoveLeft();

        
        cr.Move(moveDirection + (velocity * Time.deltaTime));
    }

    void Jump()
    {
        isGrounded = cr.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep the character grounded
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        cr.Move(velocity * Time.deltaTime);
    }

    IEnumerator Crouch()
    {
        playerAnimation.Sliding(true);
        
        cr.gameObject.transform.rotation = Quaternion.Euler(-90, defaultPos.y, defaultPos.z);
        yield return new WaitForSeconds(crouchDuration);
        
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacles>()) 
        {

           
            TakeDamage();
        }
       

    }
    public void TakeDamage()
    {
        if (health <= 0)
        {
            GameManager.instance.GameOver();
            return; // Exit to prevent further changes
        }
        

        if (health == 3)
            h3.color = Color.black;
        else if (health == 2)
            h2.color = Color.black;
        else if (health == 1)
            h1.color = Color.black;

        health--; 
        MoveEnemyCloser();
        if (health < 0)
            GameManager.instance.GameOver();
    }
    void MoveEnemyCloser()
    {
        float moveCloserAmount = 2f; // Adjust this value to control movement step
        Vector3 newPosition = enemy.transform.position + new Vector3(0, 0, moveCloserAmount);
        enemy.transform.position = newPosition;

        Debug.Log("Enemy moved closer to player: " + enemy.transform.position);
    }

}
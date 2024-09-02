using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardVelocity = 3.0f;
    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    public float laneDistance = 4; // Distance between two lanes
    public float jumpForce;
    public float gravity = -20;
    private Animator anim;
    public float laneSwitchSpeed = 10.0f;
  
    
    public float startDelay = 2.0f; // to play animation
    public float lookingDelay = 2.0f;
    private bool canMove = false;
    public ItemManager itemManager;
    public GameManager GameManager;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        StartCoroutine(StartLookingAfterDelay(lookingDelay));
        StartCoroutine(StartMovementAfterDelay(startDelay));
    }

    private IEnumerator StartLookingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("Start", true);
    }

    private IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        direction.z = forwardVelocity;
        anim.SetBool("IsRunning", true);

        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
           
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return; // Don't update if the player is not allowed to move yet
        }
        anim.SetBool("IsRunning", true);

        // Calculate the target position based on the desired lane
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        // move the player towards the target lane position
        Vector3 moveVector = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.fixedDeltaTime) - transform.position;

        // Combine the forward direction and the move vector
        moveVector.z = forwardVelocity * Time.fixedDeltaTime;

        // Apply gravity
        moveVector.y = direction.y * Time.fixedDeltaTime;

        // Move the player
        controller.Move(moveVector);

        // Update direction.y after the move to apply gravity correctly
        if (!controller.isGrounded)
        {
            direction.y += gravity * Time.fixedDeltaTime;
        }
    }

    private void Jump()
    {
        direction.y = jumpForce;
        anim.SetTrigger("Jump");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.IncreaseCollectedCoins(1);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Fruit"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null)
            {
              
                if (itemManager != null)
                {
                    itemManager.ActivatePowerUp(fruit.powerUpType, fruit.duration);
                }
                else
                {
                    Debug.LogError("ItemManager not found in the scene.");
                }
                Destroy(other.gameObject);
            }
            else
            {
                Debug.LogError("Fruit component not found on the collected object.");
            }
        }
        if (other.CompareTag("Enemy"))
        {
            canMove = false;
            anim.SetBool("GameOver", true);
        
        }
    }
}

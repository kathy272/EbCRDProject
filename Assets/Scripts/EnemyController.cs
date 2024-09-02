using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float speedOffset = 1.0f; // Speed at which the bear follows the player
    public float laneDistance = 4.0f; // Distance between lanes
    public GameManager gameManager; // Reference to your game manager for handling game-over

    private CharacterController controller; // To move the bear and ignore obstacles
    private Vector3 targetPosition; // Target position to follow the player
    public PlayerController playerController;
    public Animator bearAnimator;
    private bool canMove = false; // Flag to control when the bear starts moving
    public float startDelay = 2.0f; // Delay before the bear starts moving
    public float gameOverDelay = 2.0f; // Delay before game over after hitting the player
    public Animator playerAnimator;




    void Start()
    {
        bearAnimator = GetComponent<Animator>();
        playerAnimator = playerTransform.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        if (!playerTransform)
        {
            Debug.LogWarning("Player Transform is not assigned in EnemyBear script.");
        }

        if (!gameManager)
        {
            Debug.LogWarning("GameManager is not assigned in EnemyBear script.");
        }

        playerController = playerTransform.GetComponent<PlayerController>();
        if (!playerController)
        {
            Debug.LogWarning("PlayerController script is not found on the player object.");
        }

        StartCoroutine(StartMovementAfterDelay(startDelay));
    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
    void Update()
    {
        if (canMove && playerTransform && playerController)
        {
            // Calculate the target position to follow the player while maintaining the fixed Y position
            targetPosition = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);

            // Calculate the bear's speed based on the player's velocity
            float bearSpeed = playerController.forwardVelocity - speedOffset;

            // Move the bear towards the target position
            Vector3 direction = (targetPosition - transform.position).normalized;
            controller.Move(direction * bearSpeed * Time.deltaTime);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Ignore obstacles by not stopping the bear on collision
        if (hit.collider.CompareTag("Obstacle"))
        {
      
            Physics.IgnoreCollision(hit.collider, controller);
        }
        if (hit.collider.CompareTag("Player"))
        {
            // Set trigger to play the animation
            bearAnimator.SetBool("GameOver", true);
            playerAnimator.SetBool("GameOver", true);

            //stop enemy movement
            canMove = false;
            StartCoroutine(GameOverAfterDelay(gameOverDelay));

        }
    }


    private IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.GameOver();

    }
}

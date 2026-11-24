using UnityEngine;

using UnityEngine.UI;

using System.Collections;



public class PlayerMovement : MonoBehaviour

{

    public float moveSpeed = 5f; // Normal move speed

    public float jumpForce = 7f;

    public Transform groundCheck;

    public string groundTag = "Ground";

    public float turnSpeed = 360f;



    public float speedBoostMultiplier = 2f; // Multiplier for speed boost

    public float speedBoostDuration = 5f; // How long the speed boost lasts



    public float climbSpeed = 3f; // Speed at which the player climbs the ladder



    private float normalMoveSpeed; // Store normal speed

    private Rigidbody rb;

    private bool isGrounded;

    private float targetAngle;

    private bool isTurning = false;

    private bool isClimbing = false; // Track if the player is climbing



    public Animator animator;



    // UI Buttons for movement

    public Button moveLeftButton;

    public Button moveRightButton;

    public Button moveForwardButton;

    public Button moveBackwardButton;

    public Button jumpButton;



    private bool isNearLadder = false; // Check if player is near a ladder



    private void Start()

    {

        rb = GetComponent<Rigidbody>();

        if (rb == null)

        {

            Debug.LogError("Rigidbody component is missing from this GameObject.");

        }



        animator = GetComponent<Animator>();



        normalMoveSpeed = moveSpeed; // Initialize the normal move speed



        // Button listeners

        if (moveLeftButton != null)

            moveLeftButton.onClick.AddListener(() => Move(Vector3.left));



        if (moveRightButton != null)

            moveRightButton.onClick.AddListener(() => Move(Vector3.right));



        if (moveForwardButton != null)

            moveForwardButton.onClick.AddListener(() => Move(Vector3.forward));



        if (moveBackwardButton != null)

            moveBackwardButton.onClick.AddListener(() => Move(Vector3.back));



        if (jumpButton != null)

            jumpButton.onClick.AddListener(Jump);

    }



    private void Update()

    {

        if (rb == null) return; // Stop regular movement if no Rigidbody



        if (isClimbing)

        {

            // Continue climbing while the U key is held down

            if (Input.GetKey(KeyCode.U))

            {

                ClimbLadder();

            }

            else

            {

                StopClimbing();

            }

            return;

        }



        float horizontal = Input.GetAxis("Horizontal");

        float vertical = Input.GetAxis("Vertical");



        if (horizontal != 0 && !isTurning)

        {

            targetAngle = horizontal > 0 ? 90f : -90f;

            StartCoroutine(TurnPlayer(targetAngle));

        }



        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);



        HandleAnimations();



        if (isGrounded && Input.GetButtonDown("Jump"))

        {

            Jump();

        }



        // Ladder climbing logic

        if (isNearLadder && Input.GetKeyDown(KeyCode.U) && !isClimbing)

        {

            Debug.Log("U key pressed. Starting to climb...");

            StartClimbing();

        }



        // Trigger the attack animation when the player presses F

        if (Input.GetKeyDown(KeyCode.F))

        {

            Attack();

        }

    }



    private void HandleAnimations()

    {

        float horizontal = Input.GetAxis("Horizontal");

        float vertical = Input.GetAxis("Vertical");



        bool isMoving = vertical != 0 || horizontal != 0;

        animator.SetBool("isWalking", isMoving);



        if (Input.GetButtonDown("Jump") && isGrounded)

        {

            animator.SetTrigger("Jump");

        }

    }



    private void StartClimbing()

    {

        isClimbing = true; // Set climbing to true to stop regular movement

        rb.isKinematic = true; // Disable physics while climbing



        animator.SetTrigger("Climb"); // Play climb animation

    }



    private void ClimbLadder()

    {

        transform.Translate(Vector3.up * climbSpeed * Time.deltaTime); // Move the player up

    }



    private void StopClimbing()

    {

        isClimbing = false; // Stop climbing

        rb.isKinematic = false; // Re-enable physics after climbing



        // Ensure player falls down to ground

        if (!isGrounded)

        {

            rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);

        }



        // Reset climbing animation trigger

        animator.ResetTrigger("Climb");



        Debug.Log("Finished climbing.");

    }



    private void Jump()

    {

        if (isGrounded)

        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            animator.SetTrigger("Jump");

        }

    }



    private void Move(Vector3 direction)

    {

        Vector3 movement = direction * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);

    }



    private void OnCollisionStay(Collision collision)

    {

        if (collision.gameObject.CompareTag(groundTag))

        {

            isGrounded = true;

            animator.ResetTrigger("Jump");

        }

    }



    private void OnCollisionExit(Collision collision)

    {

        if (collision.gameObject.CompareTag(groundTag))

        {

            isGrounded = false;

        }

    }



    private void OnTriggerEnter(Collider other)

    {

        if (other.CompareTag("Ladder")) // Detect if player is near the ladder

        {

            isNearLadder = true;

            Debug.Log("Player is near the ladder.");

        }



        if (other.CompareTag("SpeedUpCoin")) // Check if the collided object is a "SpeedUpCoin"

        {

            StartCoroutine(SpeedBoost());

            Destroy(other.gameObject); // Destroy the coin after collecting it

            Debug.Log("Speed boost activated!");

        }

    }



    private void OnTriggerExit(Collider other)

    {

        if (other.CompareTag("Ladder")) // Player is no longer near the ladder

        {

            isNearLadder = false;

            Debug.Log("Player left the ladder area.");

        }

    }



    private IEnumerator SpeedBoost()

    {

        moveSpeed *= speedBoostMultiplier; // Increase the move speed

        yield return new WaitForSeconds(speedBoostDuration); // Wait for the duration of the speed boost

        moveSpeed = normalMoveSpeed; // Reset the move speed to normal

        Debug.Log("Speed boost ended.");

    }



    private void OnDrawGizmos()

    {

        Gizmos.color = Color.red;

        Gizmos.DrawLine(groundCheck.position, groundCheck.position - Vector3.up * 0.2f);

    }



    private IEnumerator TurnPlayer(float targetAngle)

    {

        isTurning = true;



        Quaternion startRotation = transform.rotation;

        // Further reduce the target angle range for smaller, more controlled rotations

        Quaternion endRotation = Quaternion.Euler(0, transform.eulerAngles.y + Mathf.Clamp(targetAngle, -10f, 10f), 0);



        float elapsedTime = 0f;

        // Increase turn duration to make the rotation slower and smoother

        float turnDuration = Mathf.Abs(targetAngle) / (turnSpeed * 2f); // Adjusting this multiplier slows down the turn



        while (elapsedTime < turnDuration)

        {

            // Slerp the rotation more gradually over time

            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / turnDuration);

            elapsedTime += Time.deltaTime;

            yield return null;

        }



        transform.rotation = endRotation; // Ensure final rotation is exact

        isTurning = false;

    }







    // Method to trigger attack animation

    private void Attack()

    {

        animator.SetTrigger("Attack"); // Trigger the attack animation

        Debug.Log("Player attacked.");

    }

}
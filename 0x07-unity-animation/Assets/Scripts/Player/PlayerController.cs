using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


///<summary>Handles player movement</summary>
public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;
    Camera cam;

    int isWalkingHash;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 appliedMovement;
    Vector3 correctedMovement;
    bool isMovementPressed;

    public float movementSpeed = 1f;

    private float turnSpeed = 0.1f;
    private float turnVelocity;

    float gravity = -9.8f;
    float groundedGravity = -.5f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 2.5f;
    float maxJumpTime = .8f;
    bool isJumping = false;
    int isJumpingHash;
    bool isJumpAnimating = false;
    int jumpCount = 0;
    Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    Coroutine currentJumpResetRoutine = null;

    int isFallingHardHash;
    bool isFallingHard = false;
    float getUpTime = 5f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
        cam = Camera.main;

        isWalkingHash = Animator.StringToHash("isWalking");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHardHash = Animator.StringToHash("isFallingHard");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;

        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        setupJumpVariables();
    }

    void setupJumpVariables ()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight / Mathf.Pow(timeToApex, 2));
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;

        float secondJumpGravity = (-2 * (maxJumpHeight + 2) / Mathf.Pow((timeToApex * 1.25f), 2));
        float secondJumpInitialVelocity = (2 * (maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (maxJumpHeight + 4) / Mathf.Pow((timeToApex * 1.5f), 2));
        float thirdJumpInitialVelocity = (2 * (maxJumpHeight + 4)) / (timeToApex * 1.5f);

        initialJumpVelocities.Add(1, initialJumpVelocity);
        initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        jumpGravities.Add(0, gravity);
        jumpGravities.Add(1, gravity);
        jumpGravities.Add(2, secondJumpGravity);
        jumpGravities.Add(3, thirdJumpGravity);
    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            if (jumpCount < 3 && currentJumpResetRoutine != null)
                StopCoroutine(currentJumpResetRoutine);
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            jumpCount += 1;
            currentMovement.y = initialJumpVelocities[jumpCount];
            appliedMovement.y = initialJumpVelocities[jumpCount];
        } 
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    IEnumerator jumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        jumpCount = 0;
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();

        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void handleFallingHard()
    {
        if (isFallingHard)
        {
            animator.SetBool(isFallingHardHash, true);
            currentMovement.x = 0;
            currentMovement.z = 0;

            if (characterController.isGrounded)
            {
                animator.SetBool(isFallingHardHash, false);
                StartCoroutine(getUpRoutine());
            }
        }
    }

    IEnumerator getUpRoutine()
    {
        yield return new WaitForSeconds(getUpTime);
        isFallingHard = false;
    }

    private void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);

        if (isMovementPressed && !isWalking)
            animator.SetBool(isWalkingHash, true);
        else if (!isMovementPressed && isWalking)
            animator.SetBool(isWalkingHash, false);
    }

    private void handleRotation()
    {
        if (isMovementPressed)
        {
            float targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            correctedMovement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            correctedMovement.y = appliedMovement.y;
        }
        else
        {
            correctedMovement = currentMovement;
            correctedMovement.y = appliedMovement.y;
        }
    }

    private void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 1.5f;

        if (characterController.isGrounded)
        {
            if (isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
                currentJumpResetRoutine = StartCoroutine(jumpResetRoutine());
                if (jumpCount == 3)
                {
                    jumpCount = 0;
                }
            }
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y += (jumpGravities[jumpCount] * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * .5f, -20f);

            if (transform.position.y < -75)
            {
                transform.position = new Vector3(0, 75, 0);
                isFallingHard = true;
            }
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y += (jumpGravities[jumpCount] * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * .5f;
        }
    }

    private void handleMovement()
    {
        if (isMovementPressed)
        {
            correctedMovement.x *= movementSpeed;
            correctedMovement.z *= movementSpeed;
        }
        if (isFallingHard)
        {
            correctedMovement.x = 0;
            correctedMovement.z = 0;
        }
        characterController.Move(correctedMovement * Time.deltaTime);
    }

    private void Update()
    {
        handleFallingHard();
        handleRotation();
        handleAnimation();
        handleMovement();
        handleGravity();
        handleJump();
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public event Action InteractEvent;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float rotationDamping = 10f;
    [SerializeField] private float jumpButtonGracePeriod;
    [SerializeField] private float animationDampTime = 0.05f;
    [SerializeField] private AudioSource interactSoundEffect;

    private Vector2 movementInput;
    private Vector3 movement;
    private float verticalVelocity;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool areControlsDisabled = false;

    private CharacterController characterController;
    private Animator animator;
    private Transform mainCameraTransform;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        movement = CalculatePlayerMovementFromInput();

        ApplyGravity();

        ApplyRotation();

        ApplyMovement();

        ApplyMovementAnimation();
    }

    private void ApplyMovement()
    {
        characterController.Move(movement * speed * Time.deltaTime);
    }

    private void ApplyRotation()
    {
        if (movementInput == Vector2.zero) return;

        Vector3 horizontalMovement = new Vector3(movement.x, 0, movement.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(horizontalMovement), Time.deltaTime * rotationDamping);
    }

    private void ApplyGravity()
    {
        if (verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        movement.y = verticalVelocity;
    }

    private void ApplyMovementAnimation()
    {
        float inputMagnitude = 0;

        if (movementInput == Vector2.zero)
        {
            inputMagnitude = 0;
        }
        else
        {
            inputMagnitude = Mathf.Clamp01(movement.magnitude);
        }

        animator.SetFloat("Input Magnitude", inputMagnitude, animationDampTime, Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        if (areControlsDisabled)
        {
            movementInput = Vector2.zero;
            return;
        }

        movementInput = value.Get<Vector2>();
    }

    private void OnJump()
    {
        if (areControlsDisabled)
        {
            verticalVelocity = 0;
            return;
        }

        jumpButtonPressedTime = Time.time;

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            verticalVelocity = 0;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                verticalVelocity += jumpPower;
            }
        }
    }

    private Vector3 CalculatePlayerMovementFromInput()
    {
        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return (forward * movementInput.y) + (right * movementInput.x);
    }

    private void OnInteract()
    {
        InteractEvent.Invoke();
    }

    public void PlayInteractSound()
    {
        interactSoundEffect.Play();
    }

    public void DisableControls()
    {
        areControlsDisabled = true;
    }
}

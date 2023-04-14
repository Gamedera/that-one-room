using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationDamping = 10f;

    private Vector2 movementInput;
    private Vector3 movement;
    private float verticalVelocity;
    private Transform mainCameraTransform;

    private CharacterController characterController;

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        movement = CalculatePlayerMovementFromInput();
        
        ApplyGravity();

        ApplyRotation();

        ApplyMovement();
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

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        // movement = new Vector3(movementInput.x, 0, movementInput.y);
        // movement = movement.normalized;
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
}
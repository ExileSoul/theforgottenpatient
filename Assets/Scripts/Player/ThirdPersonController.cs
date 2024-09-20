using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerControls))]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Physics Settings")]
    [Tooltip("The gravity applied to the character.")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Movement Settings")]
    [Tooltip("Speed at which the character rotates towards the target direction.")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f;

    private PlayerControls playerControls;
    private CharacterController characterController;

    private Vector2 inputVector = Vector2.zero;
    private Vector3 velocity;
    private bool isSprinting = false;
    private bool isSprintPressed = false;

    private void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        playerControls.actions.gameplay.sprint.performed += OnSprintPerformed;
        playerControls.actions.gameplay.sprint.canceled += OnSprintCanceled;
    }

    private void Update()
    {
        inputVector = playerControls.actions.gameplay.move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementDirection = Vector3.zero;

        transform.Rotate(0, inputVector.x * rotationSpeed * Time.fixedDeltaTime, 0);
        movementDirection = transform.forward * inputVector.y * movementSpeed;

        characterController.Move(movementDirection * Time.fixedDeltaTime - Vector3.up * 0.1f);

        velocity.y += gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Debug.DrawRay(transform.position, transform.forward * 5f, Color.green);
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        isSprintPressed = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isSprintPressed = false;
    }
}

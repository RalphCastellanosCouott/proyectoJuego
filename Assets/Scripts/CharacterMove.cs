using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMove : MonoBehaviour
{

    [Header("Movement Speed")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public float rotationSpeed = 10f;
    public float mouseSensitivity = 1f;

    [Header("References")]
    public Transform cameraTransform;
    private CharacterController controller;

    private Vector3 velocity;
    private float currentSpeed;
    private bool isGrounded;
    private float yaw;
    public bool isMoving { get; private set; }
    public float CurrentYaw => yaw;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        isMoving = inputDirection.magnitude > 0.1f;

        if (isMoving)
        {
            Vector3 moveDirection = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f) * inputDirection;
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yaw += mouseX;

        if (isMoving)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yaw, 0f), rotationSpeed * Time.deltaTime);
        }
    }
}
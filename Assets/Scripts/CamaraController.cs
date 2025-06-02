using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [Header("Camera Setup")]
    public Transform player;
    public Transform cameraTarget;
    public Vector3 shoulderOffice = new Vector3(0.3f, 1.7f, -2f);
    public float followSpeed = 10f;
    public float rotationSpeed = 5f;
    public float mouseSensitivity = 0.5f;
    [Header("Orbital")]
    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch;
    private CharacterMove playerController;
    private Transform mainCameraTransform;
    void Start()
    {
        playerController = player.GetComponent<CharacterMove>();
        mainCameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        HandleInput();
        UpdateCameraPosition();
    }


    void HandleInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (playerController.IsMoving)
        {
            yaw = playerController.CurrentYaw;
        }
        else
        {
            yaw += mouseX * rotationSpeed;
        }

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = cameraTarget.position + rotation * shoulderOffice;
        mainCameraTransform.position = Vector3.Lerp(mainCameraTransform.position, targetPosition, followSpeed * Time.deltaTime);
        mainCameraTransform.LookAt(cameraTarget);
    }
}
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
    //private PlayerController playerController;
    private Transform mainCameraTransform;
    void Start()
    {
        if (player != null)
        {
            //playerController = player.GetComponent<>();
            //if (playerController == null)
            {
                Debug.Log("PlayerController no encontrado", this);
            }
        }
        else
        {
            Debug.Log("El campo 'player' en CameraController", this);
        }

        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
            if (mainCameraTransform != this.transform)
            {
                Debug.LogWarning("");
            }
        }
        else
        {
            Debug.LogError("");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        //if (playerController != null && playerController.IsMoving)
        {
            //yaw = playerController.CurrentYaw;
        }
        //else {
        //yaw *= mouseX * rotationSpeed;
        //}

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    void UpdateCameraPosition()
    {
        if (cameraTarget == null)
        {
            Debug.LogError("", this);
            return;
        }
        if (mainCameraTransform == null)
        {
            Debug.LogError("");
            return;
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = cameraTarget.position + rotation * shoulderOffice;
        mainCameraTransform.position = Vector3.Lerp(mainCameraTransform.position, targetPosition, followSpeed * Time.deltaTime);
        mainCameraTransform.LookAt(cameraTarget.position);
    }
}

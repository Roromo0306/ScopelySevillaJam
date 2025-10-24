using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonRigidbodyController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float smoothMoveTime = 0.1f;

    [Header("Cámara")]
    public CinemachineVirtualCamera vCam;
    public float mouseSensitivity = 150f;
    public float verticalLimit = 70f;

    [Header("Head Bob")]
    public float bobSpeed = 6f;
    public float bobAmount = 0.05f;

    private Rigidbody rb;
    private Transform camTransform;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float xRotation = 0f;
    private float defaultCamY;
    private float bobTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 

        if (vCam == null)
        {
            Debug.LogError("Asigna la CinemachineVirtualCamera en el inspector.");
            return;
        }

        camTransform = vCam.VirtualCameraGameObject.transform;
        defaultCamY = camTransform.localPosition.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(vCam == null)
        {
            return;
        }
      

        LeerInput();
        RotacionCamara();
        HeadBob();
    }

    void FixedUpdate()
    {
        MovimientoFisico();
    }

    void LeerInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveInput = (transform.right * x + transform.forward * z).normalized;
    }

    void MovimientoFisico()
    {
        Vector3 targetVelocity = moveInput * moveSpeed;
        moveVelocity = Vector3.Lerp(moveVelocity, targetVelocity, Time.fixedDeltaTime / smoothMoveTime);
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void RotacionCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLimit, verticalLimit);

        camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HeadBob()
    {
        bool isMoving = moveInput.magnitude > 0.1f;

        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float newY = defaultCamY + Mathf.Sin(bobTimer) * bobAmount;
            camTransform.localPosition = new Vector3(camTransform.localPosition.x, newY, camTransform.localPosition.z);
        }
        else
        {
            bobTimer = 0f;
            camTransform.localPosition = new Vector3(
                camTransform.localPosition.x,
                Mathf.Lerp(camTransform.localPosition.y, defaultCamY, Time.deltaTime * bobSpeed),
                camTransform.localPosition.z
            );
        }
    }
}

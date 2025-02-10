using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("=============\nPlayer Controller\n=============")]
    
    [Header("Camera settings")]
    public GameObject playerCam;
    public float sens;
    public float cameraSmooth;

    [Header("Zoom settings")]
    public KeyCode zoomKey;
    public float defaultFov;
    public float zoomFov;
    public float zoomSpeed;

    private float xRotation;
    private float yRotation;
    private Transform playerCamTransform;
    private float xRotationVelocity;
    private float yRotationVelocity;
    private Camera camParams;

    [Header("Movement settings")]
    public float moveSpeed;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camParams = playerCam.GetComponent<Camera>();

        playerCamTransform = playerCam.transform;
        camParams.fieldOfView = defaultFov;
        
        CursorLock();
    }

    private void Update()
    {
        PlayerMovement();
        CameraBehaviour();
    }

    private void PlayerMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        Vector3 currVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
       
        if(moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(currVel, Vector3.zero, Time.deltaTime * 10f);
        }

        
        if(currVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = currVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void CameraBehaviour()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        float smoothXRotation = Mathf.SmoothDampAngle(playerCamTransform.localEulerAngles.x, xRotation, ref xRotationVelocity, cameraSmooth);
        float smoothYRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRotation, ref yRotationVelocity, cameraSmooth);

        transform.rotation = Quaternion.Euler(0, smoothYRotation, 0);
        playerCamTransform.localRotation = Quaternion.Euler(smoothXRotation, 0, 0);

        if(Input.GetKey(zoomKey))
        {
            camParams.fieldOfView = Mathf.Lerp(camParams.fieldOfView, zoomFov, zoomSpeed * Time.deltaTime);
        }
        else
        {
            camParams.fieldOfView = Mathf.Lerp(camParams.fieldOfView, defaultFov, zoomSpeed * Time.deltaTime);
        }
    }
    
    public static void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

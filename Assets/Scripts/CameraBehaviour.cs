using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform PLAYER;

    private float xRotation = 0f;

    public Texture2D cursorTexture;
    public Vector2 cursorSize = new Vector2(32, 32);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        HandleCursorUnlock();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        PLAYER.Rotate(Vector3.up * mouseX);

    }

    void HandleCursorUnlock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnGUI()
    {
        if (cursorTexture != null)
        {
            Vector2 position = new Vector2(Screen.width / 2 - cursorSize.x / 2, Screen.height / 2 - cursorSize.y / 2);
            GUI.DrawTexture(new Rect(position, cursorSize), cursorTexture);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform PLAYER;

    private float xRotation = 0f;

    public Texture2D cursorTexture;
    public Vector2 cursorSize = new Vector2(50, 50);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

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
        if (PauseMenuScript.isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!PauseMenuScript.isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnGUI()
    {
        if (!PauseMenuScript.isPaused && cursorTexture != null)
        {
            Vector2 position = new Vector2(Screen.width / 2 - cursorSize.x / 2, Screen.height / 2 - cursorSize.y / 2);
            GUI.DrawTexture(new Rect(position, cursorSize), cursorTexture);
        }
    }

}

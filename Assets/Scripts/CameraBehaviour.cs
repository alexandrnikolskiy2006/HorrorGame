using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform PLAYER;
    private float xRotation = 0f;

    public Texture2D cursorTexture;
    public Vector2 cursorSize = new Vector2(50, 50);

    public Camera cam;  // Ссылка на камеру
    public float zoomFOV = 30f; // Поле зрения при зуме
    public float normalFOV = 60f; // Обычное поле зрения
    public float zoomSpeed = 10f; // Скорость зума

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cam == null)
            cam = GetComponent<Camera>(); // Автозахват камеры
    }

    void Update()
    {
        LookAround();
        HandleCursorUnlock();
        HandleZoom();
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
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void HandleZoom()
    {
        // Если ПКМ удерживается, приближаем камеру, иначе возвращаем в нормальное состояние
        float targetFOV = Input.GetMouseButton(1) ? zoomFOV : normalFOV;

        // Плавное изменение FOV (зум)
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
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

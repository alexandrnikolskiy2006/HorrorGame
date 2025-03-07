using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Vector3 velocity;

    public float gravity = -9.81f; // Гравитация
    public float interactionRange = 3f; 
    public Camera playerCamera;

    private bool isGrounded;

    void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded; // Проверяем, стоит ли игрок на земле

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Чтобы стабилизировать игрока на земле
        }

        Move();
        ApplyGravity();
        HandleInteraction();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
            if (interactableObject != null && Input.GetKeyDown(KeyCode.E))
            {
                interactableObject.Interact();
            }
        }
    }
}

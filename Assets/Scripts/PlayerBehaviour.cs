using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;
    private Vector3 velocity;

    public float interactionRange = 3f; 
    public Camera playerCamera;

    void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        HandleInteraction();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // controller.Move(velocity * Time.deltaTime);
    }

    void HandleInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
            {
                if (interactableObject != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactableObject.Interact();
                    }
                }

            }
        }
    }
}

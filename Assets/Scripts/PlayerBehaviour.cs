using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float verticalInput;
    public float horizontalInput;
    public Vector3 moveDirection;


    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        this.transform.position += (moveDirection * moveSpeed * Time.deltaTime);


    }
}

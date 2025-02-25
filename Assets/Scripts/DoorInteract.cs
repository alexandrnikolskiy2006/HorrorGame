using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float smooth = 2f;
    
    private bool isOpen = false;
    
    public Transform hingePoint;

    // private float currentAngle = 0f;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = hingePoint.rotation;
        openRotation = Quaternion.Euler(hingePoint.eulerAngles + new Vector3(0f, openAngle, 0f));
        // currentAngle = transform.rotation.eulerAngles.y;
    }

    public void Interact()
    {
        isOpen = !isOpen;
    }

    void Update()
    {
        // if (isOpen && currentAngle < openAngle)
        // {
        //     // transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * smooth);
        //     currentAngle += smooth * Time.deltaTime;
        //     if (currentAngle > openAngle) currentAngle = openAngle;
        // }
        // else if (!isOpen && currentAngle > 0)
        // {
        //     currentAngle -= smooth * Time.deltaTime;
        //     if (currentAngle < 0) currentAngle = 0;
        // }

        if (isOpen)
        {
            hingePoint.rotation = Quaternion.Slerp(hingePoint.rotation, openRotation, Time.deltaTime * smooth);
        }
        else
        {
            hingePoint.rotation = Quaternion.Slerp(hingePoint.rotation, closedRotation, Time.deltaTime * smooth);
        }
        // transform.rotation = Quaternion.Euler(0, currentAngle, 0);
    }
}

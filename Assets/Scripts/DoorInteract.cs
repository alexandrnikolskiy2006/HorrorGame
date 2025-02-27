using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float smooth = 2f;
    
    private bool isOpen = false;
    public Transform hingePoint;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = hingePoint.rotation;
        openRotation = Quaternion.Euler(hingePoint.eulerAngles + new Vector3(0f, openAngle, 0f));
    }

    public void Interact()
    {
        isOpen = !isOpen;
    }

    void Update()
    {
        if (isOpen)
        {
            hingePoint.rotation = Quaternion.Slerp(hingePoint.rotation, openRotation, Time.deltaTime * smooth);
        }
        else
        {
            hingePoint.rotation = Quaternion.Slerp(hingePoint.rotation, closedRotation, Time.deltaTime * smooth);
        }
    }
}

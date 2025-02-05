using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    [Header("Drag Settings")]
    public float smoothSpeed = 10f; 
    public Transform targetTransform;

    [Header("Movement Check")]
    public bool isMoving; 
    public float minVelocityThreshold = 0.1f;

    [Header("Rotation Settings")]
    public float rotationSmoothSpeed = 5f; 
    public float maxRotationSpeed = 10f; 

    private Vector3 targetPosition; 
    private Vector3 previousTargetPosition; 
    private Vector3 targetVelocity; 
    private Quaternion targetRotation; 
    private Quaternion originalRotation; 


    private void Start()
    {
        mainCamera = Camera.main;

        if (targetTransform == null)
        {
            targetTransform = transform;
        }

        previousTargetPosition = targetTransform.position;

        originalRotation = targetTransform.rotation;
        targetRotation = originalRotation;
        

    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        offset = transform.position - mainCamera.ScreenToWorldPoint(mousePos);
        isDragging = true;


        targetPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
            targetPosition = mainCamera.ScreenToWorldPoint(mousePos) + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        OnDropAction();

    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = targetPosition;
        }


        if (targetTransform != null)
        {
            targetTransform.position = Vector3.Lerp(targetTransform.position, transform.position, smoothSpeed * Time.deltaTime);
            targetVelocity = (targetTransform.position - previousTargetPosition) / Time.deltaTime;
            previousTargetPosition = targetTransform.position;

            isMoving = targetVelocity.magnitude > minVelocityThreshold;

            ApplyRotationBasedOnVelocity();
        }
    }

    private void ApplyRotationBasedOnVelocity()
    {

        if (isMoving)
        {

            Vector3 moveDirection = targetVelocity.normalized;
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f; // -90° para que apunte hacia adelante
            Quaternion desiredRotation = Quaternion.Euler(0, 0, targetAngle);

            float velocityFactor = Mathf.Clamp01(targetVelocity.magnitude / maxRotationSpeed);
            targetRotation = Quaternion.Slerp(originalRotation, desiredRotation, velocityFactor);
        }
        else
        {
            targetRotation = originalRotation;
        }
        targetTransform.rotation = Quaternion.Slerp(targetTransform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
    }


    public void OnDropAction()
    {
        // Disparar el evento
        InteractionSystem.GatillarOnDrop();

    }
    


}

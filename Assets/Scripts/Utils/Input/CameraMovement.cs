using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    private Vector3 origin;
    private Vector3 difference;
    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    private bool isDragging;
    void Awake()
    {
        cam = Camera.main;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) origin = GetMousePosition;
        isDragging = ctx.started || ctx.performed;
    }

    public void OnScroll(InputAction.CallbackContext ctx)
    {
        float newSize = cam.orthographicSize - Mouse.current.scroll.ReadValue().normalized.y * zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

    }

    private void LateUpdate()
    {
        if (!isDragging) return;
        difference = GetMousePosition - transform.position;
        transform.position = origin - difference;
    }

    private Vector3 GetMousePosition => cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

}

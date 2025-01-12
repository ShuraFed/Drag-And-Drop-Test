using UnityEngine;
using UnityEngine.EventSystems;

public class SpritePan : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float edgeMargin = 0.1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] LayerMask draggableLayer;

    private Vector3 dragOrigin;
    private bool isDragging = false;
    private float moveSpeedWithItemCoeff = 3;
    private float minX;
    private float maxX;

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            moveSpeed /= 20;
            moveSpeedWithItemCoeff *= 20;
        }

        CalculateClampedX();
    }

    private void CalculateClampedX()
    {
        float spriteWidth = spriteRenderer.bounds.size.x;
        Camera mainCamera = Camera.main;
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;

        minX = spriteRenderer.transform.position.x - spriteWidth / 2 + cameraWidth;
        maxX = spriteRenderer.transform.position.x + spriteWidth / 2 - cameraWidth;
    }

    private void Update()
    {
        HandleMouseDrag();
    }

    private void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUI() && !IsPointerOverDraggable())
            {
                isDragging = true;
                dragOrigin = Input.mousePosition;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 direction = mousePos- dragOrigin;

            spriteRenderer.transform.position += new Vector3(direction.x * moveSpeed * Time.deltaTime, 0, 0);
            dragOrigin = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (Input.GetMouseButton(0)&&!isDragging && (Input.mousePosition.x <= edgeMargin * Screen.width || Input.mousePosition.x >= (1 - edgeMargin) * Screen.width))
        {
            float moveDirection = Input.mousePosition.x <= edgeMargin * Screen.width ? 1 : -1;
            spriteRenderer.transform.position += new Vector3(moveDirection * moveSpeed * moveSpeedWithItemCoeff * Time.deltaTime, 0, 0);
        }

        ClampSpritePosition();
    }

    private void ClampSpritePosition()
    {
        var clampedX = Mathf.Clamp(spriteRenderer.transform.position.x, minX, maxX);
        spriteRenderer.transform.position = new Vector3(clampedX, spriteRenderer.transform.position.y, spriteRenderer.transform.position.z);
    }

    private bool IsPointerOverUI()
    {
        foreach (var touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true;
            }
        }
        return EventSystem.current.IsPointerOverGameObject(0);
    }

    private bool IsPointerOverDraggable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableLayer);
        return hit.collider != null;
    }
}
using UnityEngine;
using System.Collections.Generic;

public class DragAndDropSystem : MonoBehaviour
{
    private Dictionary<int, IDraggable> draggingObjects = new Dictionary<int, IDraggable>();

    private void Update()
    {
#if UNITY_EDITOR
        HandleMouseInput();
#else
            HandleInput();
#endif
    }
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                var go = hit.collider.gameObject;
                IDraggable draggable = go.GetComponent<IDraggable>();
                if (draggable != null)
                {
                    Vector3 offset = go.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    draggable.OnDragStart(offset);
                    draggingObjects[0] = draggable; 
                }
            }
        }
        else if (Input.GetMouseButton(0) && draggingObjects.ContainsKey(0))
        {
            draggingObjects[0].OnDrag(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0) && draggingObjects.ContainsKey(0))
        {
            draggingObjects[0].OnDrop();
            draggingObjects.Remove(0);
        }
    }
    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (hit.collider != null)
                        {
                            var go = hit.collider.gameObject;
                            IDraggable draggable = go.GetComponent<IDraggable>();
                            if (draggable != null)
                            {
                                Vector3 offset = go.transform.position - Camera.main.ScreenToWorldPoint(touch.position);
                                draggable.OnDragStart(offset);
                                draggingObjects[touch.fingerId] = draggable; 
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        if (draggingObjects.ContainsKey(touch.fingerId))
                        {
                            draggingObjects[touch.fingerId].OnDrag(Camera.main.ScreenToWorldPoint(touch.position));
                        }
                        break;
                    case TouchPhase.Ended:
                        if (draggingObjects.ContainsKey(touch.fingerId))
                        {
                            draggingObjects[touch.fingerId].OnDrop();
                            draggingObjects.Remove(touch.fingerId); 
                        }
                        break;
                }
            }
        }
    }
}
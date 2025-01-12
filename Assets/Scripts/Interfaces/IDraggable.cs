using UnityEngine;

public interface IDraggable
{ 
    void OnDragStart(Vector3 offset);
    void OnDrag(Vector3 position);
    void OnDrop();
}

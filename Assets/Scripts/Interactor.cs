using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, interactableLayer);
            if (hit)
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                interactable?.Interact();
            }
        }
    }
}

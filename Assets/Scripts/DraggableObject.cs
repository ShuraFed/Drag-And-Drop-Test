using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DraggableObject : MonoBehaviour, IDraggable
{
    [SerializeField] private LayerMask dropLayers;
    private Vector3 offset;
    private float dropTime = 0.4f;
    private float scaleTime = 0.3f;
    private float scaleFactor = 1.1f;

    public virtual void OnDragStart(Vector3 offset)
    {
        transform.DOKill();

        ICanReturn ICanReturn = transform.parent.GetComponent<ICanReturn>();
        if (ICanReturn != null) ICanReturn.Return(transform);

        this.offset = offset;
        transform.DOScale(Vector3.one * scaleFactor, scaleTime);
    }

    public virtual void OnDrag(Vector3 position)
    {
        transform.position = position + offset;
    }

    public virtual void OnDrop()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, dropLayers);

        if (hit.collider != null)
        {
            Vector3 newPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            Tween moveTween = transform.DOMove(newPosition, dropTime);

            if (hit.distance > 0.5f)
            {
                moveTween.OnComplete(() =>
                {
                    transform.DOJump(newPosition, 0.1f, 1, 0.1f);
                });
            }
            transform.DOScale(Vector3.one, scaleTime);
            IReciever receiver = hit.collider.GetComponent<IReciever>();
            if (receiver != null)
            {
                moveTween.OnComplete(() => receiver.Recieve(transform));
            }
        }
    }
}

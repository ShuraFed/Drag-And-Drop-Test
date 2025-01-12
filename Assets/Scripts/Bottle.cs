using DG.Tweening;
using UnityEngine;

public class Bottle : DraggableObject
{
    [SerializeField] ParticleSystem ps;
    private Vector3 rotation = new Vector3(0, 0, 30);
    private float rotationTime = 0.2f;
    private Tween rotationTween;
    public override void OnDragStart(Vector3 offset)
    {
        base.OnDragStart(offset);
        rotationTween?.Kill(true);
        rotationTween = transform.DORotate(rotation, rotationTime);
    }
    public override void OnDrag(Vector3 position)
    {
        base.OnDrag(position);
        if(ps.isStopped) ps.Play();
    }

    public override void OnDrop()
    {
        base.OnDrop();
        rotationTween?.Kill(true);
        rotationTween =transform.DORotate(Vector3.zero, rotationTime);
        ps.Stop();
        ps.Clear();
    }
}

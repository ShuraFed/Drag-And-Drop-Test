using DG.Tweening;
using UnityEngine;

public class Container : MonoBehaviour, IReciever,ICanReturn
{
    [SerializeField] Transform objectsPosition;
    public void Recieve(Transform transform)
    {
        transform.SetParent(this.transform);
        transform.DOKill();
        transform.DOMove(objectsPosition.position, 0.2f);
    }

    public void Return(Transform transform)
    {
        transform.SetParent(this.transform.parent);
    }
}

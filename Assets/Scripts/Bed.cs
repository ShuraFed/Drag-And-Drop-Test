using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour,IReciever,ICanReturn
{
    public void Recieve(Transform transform)
    {
        transform.SetParent(this.transform);
        transform.DORotate(new Vector3(0, 0, 90), 0.3f);
        var person = transform.GetComponent<Character>();
        if (person != null) person.LayDown();
    }

    public void Return(Transform transform)
    {
        transform.DORotate(new Vector3(0, 0, 0), 0.3f);
        transform.SetParent(this.transform.parent);
        var person = transform.GetComponent<Character>();
        if (person != null) person.Stand();
    }
}

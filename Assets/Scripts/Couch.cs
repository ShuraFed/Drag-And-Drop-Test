using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couch : MonoBehaviour,IReciever,ICanReturn
{
    public void Recieve(Transform transform)
    {
        transform.SetParent(this.transform);

        var person =transform.GetComponent<Character>();
        if (person != null) person.Sit();
    }

    public void Return(Transform transform)
    {
        transform.SetParent(this.transform.parent);

        var person = transform.GetComponent<Character>();
        if (person != null) person.Stand();
    }
}

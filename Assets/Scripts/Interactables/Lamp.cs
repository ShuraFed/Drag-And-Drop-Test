using UnityEngine;
using UnityEngine.Rendering.Universal;
[RequireComponent(typeof(Light2D))]
[RequireComponent (typeof(BoxCollider2D))]
public class Lamp :MonoBehaviour, IInteractable
{
    Light2D lamp;
    private void Awake()
    {
        lamp= GetComponent<Light2D>();
    }
    public void Interact()
    {
        if(lamp.enabled) lamp.enabled = false;
        else lamp.enabled = true;
    }
}

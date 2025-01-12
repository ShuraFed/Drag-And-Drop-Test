using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Shower : MonoBehaviour,IInteractable
{
    [SerializeField] ParticleSystem ps;
    private ParticleSystem.EmissionModule emission;
    private void Awake()
    {
        emission = ps.emission;
    }
    public void Interact()
    {
        emission.enabled =!emission.enabled;
    }
}

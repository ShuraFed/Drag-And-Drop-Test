using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class DayNight : MonoBehaviour, IInteractable
{
    private Light2D globalLight;
    private float dayIntensity=1f;
    private float nightIntensity=0.5f;
    private void Awake()
    {
        globalLight= GetComponent<Light2D>();
    }
    private void Swith(float i)
    {
        float intensity = globalLight.intensity;
        DOTween.To(() => intensity, x => intensity = x, i, 1f)
            .OnUpdate(() => {
                globalLight.intensity = intensity;
            });
        transform.DORotate(new Vector3(0, 0, -180), 1f, RotateMode.LocalAxisAdd);
    }

    public void Interact()
    {
        if (globalLight.intensity == dayIntensity)
        {
            Swith(nightIntensity);
        }
        else
        {
            Swith(dayIntensity);
        }
    }

}

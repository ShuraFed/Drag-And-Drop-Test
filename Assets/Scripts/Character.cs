using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite StandSprite;
    [SerializeField] Sprite SitSprite;
    [SerializeField] Sprite LayDownSprite;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void Sit()
    {
        ChangeSprite(SitSprite);
    }
    public void Stand()
    {
        ChangeSprite(StandSprite);
    }
    public void LayDown()
    {
        ChangeSprite(LayDownSprite);
    }
}

using UnityEngine;

public class DoorKey : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject doorObject;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (spriteRenderer)
        {
            var c = spriteRenderer.color;
            c = Color.green;
            spriteRenderer.color = c;
        }
        doorObject.SetActive(false);
        enabled = false;
    }
}

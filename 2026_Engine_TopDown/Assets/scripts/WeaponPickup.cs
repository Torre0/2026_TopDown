using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponData weaponData;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RefreshSprite()
    {
        if (weaponData == null)
            return;

        spriteRenderer.sprite = weaponData.icon;
    }
}
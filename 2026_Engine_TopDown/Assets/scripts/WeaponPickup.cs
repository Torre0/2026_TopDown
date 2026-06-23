using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponData weaponData;

    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polyCollider;

    private void Awake()
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();

        polyCollider =
            GetComponent<PolygonCollider2D>();
    }

    public void RefreshSprite()
    {
        if (weaponData == null)
            return;

        spriteRenderer.sprite =
            weaponData.icon;

        if (polyCollider != null)
        {
            Destroy(polyCollider);

            polyCollider =
                gameObject.AddComponent
                <PolygonCollider2D>();

            polyCollider.isTrigger = true;
        }
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public int attackDamage;

    public float attackSpeed;

    public float attackRange;

    public float effectScale;

    public Sprite icon;
}
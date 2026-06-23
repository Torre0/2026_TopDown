using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Game Setting Data")]
public class GameSettingData : ScriptableObject
{
    public int startHp = 10;
    public int startAttack = 5;
    public float playerMoveSpeed = 1.1f;

    public int hpBonusPerDeath = 5;
    public int atkBonusPerDeath = 1;
}

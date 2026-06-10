using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameSettingData gameSattingData;
    public SaveData saveData;
    public int isTurorialFinished;

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            saveData = Application.persistentDataPath + "/saveData.json";

            LoadJsonData();
            LoadPlayerPrefs();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int GetPlayerHp()
    {
        int baseHp = gameSattingData.startHp;
        int bornusHp = gameSattingData.hpBonusPerDeath;

        return baseHp + bornusHp * saveData.deathCount;
    }

    public int GetPlayerAttack()
    {
        int baseAttack = gameSattingData.startAttack;
        int bornusAttack = gameSattingData.attackBonusPerDeath;
    }
}

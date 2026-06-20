using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameSettingData gameSattingData;
    public SaveData saveData;
    public int isTutorialFinished;

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/saveData.json";

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

        int deathBonus =
            gameSattingData.hpBonusPerDeath *
            saveData.deathCount;

        int upgradeBonus =
            saveData.hpLevel * 20;

        return baseHp +
            deathBonus +
            upgradeBonus;
    }

    public int GetPlayerAttack()
    {
        int baseAttack =
        gameSattingData.startAttack;

        int deathBonus =
            gameSattingData.atkBonusPerDeath *
            saveData.deathCount;

        int upgradeBonus =
            saveData.attackLevel * 2;

        return baseAttack +
            deathBonus +
            upgradeBonus;
    }

    public float GetPlayerMoveSpeed()
    {
        return gameSattingData.playerMoveSpeed +
           saveData.speedLevel * 0.2f;
    }

    public void SaveGameResult()
    {
        saveData.deathCount++;

        SaveJsonData();
    }

    public void SaveJsonData()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);

        Debug.Log("JSON 저장 완료: " + savePath);
    }

    public void LoadJsonData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
            SaveJsonData();
        }
    }

    public void DeleteJsonData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        saveData = new SaveData();
        SaveJsonData();

        Debug.Log("JSON 저장 데이터 삭제");
    }

    public void LoadPlayerPrefs()
    {
        isTutorialFinished = PlayerPrefs.GetInt("TUTORIAL", 0);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("TUTORIAL", isTutorialFinished);
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs 저장 완료");
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("TUTORIAL");
        LoadPlayerPrefs();

        Debug.Log("PlayerPrefs 삭제 완료");
    }

    public void AddGold(int amount)
    {
        saveData.totalGold += amount;

        SaveJsonData();

        Debug.Log("현재 골드 : " + saveData.totalGold);
    }

    public bool UseGold(int amount)
    {
        if (saveData.totalGold < amount)
        {
            return false;
        }

        saveData.totalGold -= amount;

        SaveJsonData();

        return true;
    }
    
    public void UpgradeAttack()
    {
        int cost = 50;

        if (UseGold(cost))
        {
            saveData.attackLevel++;

            SaveJsonData();

            Debug.Log("공격력 강화!");
        }
    }

    public void UpgradeSpeed()
    {
        int cost = 50;

        if (UseGold(cost))
        {
            saveData.speedLevel++;

            SaveJsonData();

            Debug.Log("이동속도 강화!");
        }
    }
}

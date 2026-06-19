using UnityEngine;

public class BattleRoomController : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] monsterPrefabs;

    public int minMonsterCount = 1;
    public int maxMonsterCount = 3;

    private void Start()
    {
        SpawnMonsters();
    }

    private void SpawnMonsters()
    {
        int monsterCount =
            Random.Range(
                minMonsterCount,
                maxMonsterCount + 1);

        for (int i = 0; i < monsterCount; i++)
        {
            Transform spawnPoint =
                spawnPoints[
                    Random.Range(
                        0,
                        spawnPoints.Length)];

            GameObject monster =
                monsterPrefabs[
                    Random.Range(
                        0,
                        monsterPrefabs.Length)];

            Instantiate(
                monster,
                spawnPoint.position,
                Quaternion.identity,
                transform);
        }
    }
}
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public GameObject portalPrefab;

    private bool portalCreated;
    private bool canCheckPortal;

    private Vector3 lastEnemyPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(EnablePortalCheck());
    }

    private IEnumerator EnablePortalCheck()
    {
        yield return new WaitForSeconds(3f);

        canCheckPortal = true;
    }

    public void SetLastEnemyPosition(Vector3 position)
    {
        lastEnemyPosition = position;
    }

    private void Update()
    {
        if (!canCheckPortal)
            return;

        if (portalCreated)
            return;

        EnemyHealth[] enemies =
            FindObjectsByType<EnemyHealth>(
                FindObjectsSortMode.None);

        BossHealth[] bosses =
            FindObjectsByType<BossHealth>(
                FindObjectsSortMode.None);

        if (enemies.Length == 0 &&
            bosses.Length == 0)
        {
            portalCreated = true;

            CreatePortal();
        }
    }

    private void CreatePortal()
    {
        Debug.Log("포탈 생성");

        Instantiate(
            portalPrefab,
            lastEnemyPosition,
            Quaternion.identity);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapGenerator : MonoBehaviour
{
    [Header("UI")]
    public RectTransform minimapParent;
    public GameObject roomIconPrefab;

    [Header("설정")]
    public float roomSpacing = 20f;

    public void GenerateMap(
        List<Vector2Int> roomPositions)
    {
        // 기존 아이콘 삭제
        foreach (Transform child in minimapParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Vector2Int pos in roomPositions)
        {
            GameObject icon =
                Instantiate(
                    roomIconPrefab,
                    minimapParent);

            RectTransform rect =
                icon.GetComponent<RectTransform>();

            rect.anchoredPosition =
                new Vector2(
                    pos.x * roomSpacing,
                    pos.y * roomSpacing);
        }
    }
}
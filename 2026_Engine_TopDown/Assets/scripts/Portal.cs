using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(ActivatePortal());
    }

    private IEnumerator ActivatePortal()
    {
        if (col != null)
            col.enabled = false;

        Debug.Log("포탈 생성됨 (3초 대기)");

        yield return new WaitForSeconds(3f);

        if (col != null)
            col.enabled = true;

        Debug.Log("포탈 활성화");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        string currentScene =
            SceneManager.GetActiveScene().name;

        if (currentScene == "Level_5")
        {
            SceneManager.LoadScene("Title");
            return;
        }

        int nextScene =
            SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextScene);
    }
}
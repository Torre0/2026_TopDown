using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject settingPanel;

    // 게임 시작
    public void GameStart()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // 설정 열기
    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    // 설정 닫기
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

    // 게임 종료
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
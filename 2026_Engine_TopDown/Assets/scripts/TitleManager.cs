using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [Header("설정창")]
    public GameObject settingPanel;

    [Header("UI")]
    public Slider titleSlider;
    public Slider gameSlider;

    public Toggle titleToggle;
    public Toggle gameToggle;

    private void Start()
    {
        LoadAudioSettingUI();
    }

    // 게임 시작
    public void GameStart()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // 설정창 열기
    public void OpenSetting()
    {
        settingPanel.SetActive(true);

        LoadAudioSettingUI();
    }

    // 설정창 닫기
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

    private void LoadAudioSettingUI()
    {
        if (AudioManager.Instance == null)
        return;

        titleSlider.SetValueWithoutNotify(
            AudioManager.Instance.GetTitleVolume());

        gameSlider.SetValueWithoutNotify(
            AudioManager.Instance.GetGameVolume());

        titleToggle.SetIsOnWithoutNotify(
            AudioManager.Instance.GetTitleToggleState());

        gameToggle.SetIsOnWithoutNotify(
            AudioManager.Instance.GetGameToggleState());
    }
}
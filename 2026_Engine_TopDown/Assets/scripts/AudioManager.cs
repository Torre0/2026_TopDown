using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource bgmSource;

    [Header("BGM")]
    public AudioClip titleBGM;
    public AudioClip gameBGM;

    [Header("Volume")]
    [Range(0f, 1f)]
    public float titleVolume = 1f;

    [Range(0f, 1f)]
    public float gameVolume = 1f;

    private bool titleMute = false;
    private bool gameMute = false;

    private bool isInitialized = false;

    private void Start()
{
    isInitialized = true;
}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 저장값 불러오기
            titleVolume = PlayerPrefs.GetFloat("TitleVolume", 1f);
            gameVolume = PlayerPrefs.GetFloat("GameVolume", 1f);

            titleMute = PlayerPrefs.GetInt("TitleMute", 0) == 1;
            gameMute = PlayerPrefs.GetInt("GameMute", 0) == 1;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title")
        {
            PlayBGM(titleBGM);
        }
        else if (scene.name == "Tutorial" ||
                 scene.name == "Level_1")
        {
            PlayBGM(gameBGM);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
            return;

        bgmSource.clip = clip;
        bgmSource.loop = true;

        if (clip == titleBGM)
        {
            bgmSource.volume =
                titleMute ? 0f : titleVolume;
        }
        else
        {
            bgmSource.volume =
                gameMute ? 0f : gameVolume;
        }

        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    // =========================
    // 타이틀 음악
    // =========================

    public void SetTitleVolume(float volume)
    {
        titleVolume = volume;

        PlayerPrefs.SetFloat("TitleVolume", volume);
        PlayerPrefs.Save();

        if (bgmSource.clip == titleBGM)
        {
            bgmSource.volume =
                titleMute ? 0f : titleVolume;
        }
    }

    public void ToggleTitleMute(bool isOn)
    {
        if (!isInitialized)
        return;

        // 체크 = 소리 켜짐
        titleMute = !isOn;

        PlayerPrefs.SetInt(
            "TitleMute",
            titleMute ? 1 : 0);

        PlayerPrefs.Save();

        if (bgmSource.clip == titleBGM)
        {
            bgmSource.volume =
                titleMute ? 0f : titleVolume;
        }
    }

    // =========================
    // 게임 음악
    // =========================

    public void SetGameVolume(float volume)
    {
        gameVolume = volume;

        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();

        if (bgmSource.clip == gameBGM)
        {
            bgmSource.volume =
                gameMute ? 0f : gameVolume;
        }
    }

    public void ToggleGameMute(bool isOn)
    {
        if (!isInitialized)
        return;

        // 체크 = 소리 켜짐
        gameMute = !isOn;

        PlayerPrefs.SetInt(
            "GameMute",
            gameMute ? 1 : 0);

        PlayerPrefs.Save();

        if (bgmSource.clip == gameBGM)
        {
            bgmSource.volume =
                gameMute ? 0f : gameVolume;
        }
    }

    // =========================
    // UI 불러오기용
    // =========================

    public float GetTitleVolume()
    {
        return titleVolume;
    }

    public float GetGameVolume()
    {
        return gameVolume;
    }

    public bool GetTitleToggleState()
    {
        return !titleMute;
    }

    public bool GetGameToggleState()
    {
        return !gameMute;
    }
}
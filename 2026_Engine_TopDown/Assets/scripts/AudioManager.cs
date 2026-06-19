using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;

    public AudioClip titleBGM;
    public AudioClip gameBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title")
        {
            PlayBGM(titleBGM);
        }
        else if (scene.name == "Level_1" || scene.name == "Tutorial")
        {
            PlayBGM(gameBGM);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("대기 애니메이션")]
    public Sprite[] idle;

    [Header("애니메이션 설정")]
    public float frameTime = 0.15f;

    private SpriteRenderer sr;
    private int frameIndex;
    private float timer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (idle.Length > 0)
        {
            sr.sprite = idle[0];
        }
    }

    private void Update()
    {
        if (idle == null || idle.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;

            frameIndex++;

            if (frameIndex >= idle.Length)
            {
                frameIndex = 0;
            }

            sr.sprite = idle[frameIndex];
        }
    }
}
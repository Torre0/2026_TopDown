using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public Sprite[] effectSprites;
    public float frameTime = 0.05f;

    private SpriteRenderer sr;

    private int currentFrame;
    private float timer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if (effectSprites.Length > 0)
        {
            sr.sprite = effectSprites[0];
        }
    }

    private void Update()
    {
        if (effectSprites.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;

            currentFrame++;

            if (currentFrame >= effectSprites.Length)
            {
                Destroy(gameObject);
                return;
            }

            sr.sprite = effectSprites[currentFrame];
        }
    }
}
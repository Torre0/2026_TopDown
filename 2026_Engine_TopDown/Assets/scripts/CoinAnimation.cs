using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public Sprite[] coinSprites;

    public float frameRate = 0.1f;

    private SpriteRenderer sr;

    private int currentFrame;
    private float timer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (coinSprites.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;

            currentFrame++;

            if (currentFrame >= coinSprites.Length)
            {
                currentFrame = 0;
            }

            sr.sprite = coinSprites[currentFrame];
        }
    }
}
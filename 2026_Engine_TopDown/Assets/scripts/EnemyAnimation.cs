using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [Header("이동 스프라이트")]
    public Sprite[] moveDown;
    public Sprite[] moveUp;
    public Sprite[] moveLeft;
    public Sprite[] moveRight;

    [Header("애니메이션")]
    public float frameTime = 0.15f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Sprite[] currentSprites;
    private Sprite[] lastDirectionSprites;

    private float timer;
    private int frameIndex;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentSprites = moveDown;
        lastDirectionSprites = moveDown;

        if (moveDown.Length > 0)
        {
            sr.sprite = moveDown[0];
        }
    }

    private void Update()
    {
        UpdateDirection();
        UpdateAnimation();
    }

    private void UpdateDirection()
    {
        Vector2 move = rb.linearVelocity;

        if (move.sqrMagnitude < 0.01f)
            return;

        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            if (move.x > 0)
            {
                if (currentSprites != moveRight)
                {
                    currentSprites = moveRight;
                    lastDirectionSprites = moveRight;
                    frameIndex = 0;
                }
            }
            else
            {
                if (currentSprites != moveLeft)
                {
                    currentSprites = moveLeft;
                    lastDirectionSprites = moveLeft;
                    frameIndex = 0;
                }
            }
        }
        else
        {
            if (move.y > 0)
            {
                if (currentSprites != moveUp)
                {
                    currentSprites = moveUp;
                    lastDirectionSprites = moveUp;
                    frameIndex = 0;
                }
            }
            else
            {
                if (currentSprites != moveDown)
                {
                    currentSprites = moveDown;
                    lastDirectionSprites = moveDown;
                    frameIndex = 0;
                }
            }
        }
    }

    private void UpdateAnimation()
    {
        // 정지 상태
        if (rb.linearVelocity.sqrMagnitude < 0.01f)
        {
            if (lastDirectionSprites != null &&
                lastDirectionSprites.Length > 0)
            {
                sr.sprite = lastDirectionSprites[0];
            }

            return;
        }

        if (currentSprites == null ||
            currentSprites.Length == 0)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;

            frameIndex++;

            if (frameIndex >= currentSprites.Length)
            {
                frameIndex = 0;
            }

            sr.sprite = currentSprites[frameIndex];
        }
    }
}
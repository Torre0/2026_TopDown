using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Sprite[] idleUp;
    public Sprite[] idleDown;
    public Sprite[] idleLeft;
    public Sprite[] idleRight;
    public Sprite[] spriteUp;
    public Sprite[] spriteDown;
    public Sprite[] spriteLeft;
    public Sprite[] spriteRight;
    public float frameTime = 0.15f;
    public float animationSpeed = 2f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Sprite[] idleSprite;
    private Vector2 input;
    private Vector2 velocity;
    private Sprite[] currentSprites;
    private int frameIndex = 0;
    private float timer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentSprites = spriteDown;
        idleSprite = idleDown;

        sr.sprite = idleSprite[0];
    }

    void Update()
    {
        if (sr == null) return;

        timer += Time.deltaTime * animationSpeed;

        if (timer >= frameTime)
        {
            timer = 0f;
            frameIndex++;

            if (input.sqrMagnitude <= 0.01f)
            {
                if (frameIndex >= idleSprite.Length)
                    frameIndex = 0;

                sr.sprite = idleSprite[frameIndex];
            }
            else
            {
                if (frameIndex >= currentSprites.Length)
                    frameIndex = 0;

                sr.sprite = currentSprites[frameIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void ChangeSprites(Sprite[] newSprite)
    {
        if (currentSprites == newSprite)
            return;

        currentSprites = newSprite;
        frameIndex = 0;
        timer = 0f;
        sr.sprite = currentSprites[frameIndex];
    }

    private void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude <= 0.01f)
            return;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            if (input.x > 0)
            {
                ChangeSprites(spriteRight);
                idleSprite = idleRight;
            }
            else
            {
                ChangeSprites(spriteLeft);
                idleSprite = idleLeft;
            }
        }
        else
        {
            if (input.y > 0)
            {
                ChangeSprites(spriteUp);
                idleSprite = idleUp;
            }
            else
            {
                ChangeSprites(spriteDown);
                idleSprite = idleDown;
            }
        }
    }
}

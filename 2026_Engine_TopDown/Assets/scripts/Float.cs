using UnityEngine;

public class Float : MonoBehaviour
{
    public Transform coinSprite;

    public float floatSpeed = 3f;
    public float floatAmount = 0.15f;

    private Vector3 startLocalPos;

    private void Start()
    {
        startLocalPos = coinSprite.localPosition;
    }

    private void Update()
    {
        coinSprite.localPosition =
            startLocalPos +
            Vector3.up *
            Mathf.Sin(Time.time * floatSpeed)
            * floatAmount;
    }
}
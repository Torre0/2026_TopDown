using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject wallUp;
    public GameObject wallDown;
    public GameObject wallLeft;
    public GameObject wallRight;

    public void SetupWalls(
        bool hasUp,
        bool hasDown,
        bool hasLeft,
        bool hasRight)
    {
        wallUp.SetActive(!hasUp);
        wallDown.SetActive(!hasDown);
        wallLeft.SetActive(!hasLeft);
        wallRight.SetActive(!hasRight);
    }
}
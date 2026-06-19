using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("방 프리팹")]
    public GameObject startRoomPrefab;
    public GameObject battleRoomPrefab;

    [Header("통로 프리팹")]
    public GameObject horizontalCorridorPrefab;
    public GameObject verticalCorridorPrefab;

    [Header("플레이어")]
    public Transform player;

    [Header("생성 설정")]
    public int battleRoomCount = 6;
    public float roomDistance = 12f;

    public MinimapGenerator minimapGenerator;

    private List<Vector2Int> roomPositions =
        new List<Vector2Int>();

    private List<Connection> connections =
        new List<Connection>();

    private class Connection
    {
        public Vector2Int from;
        public Vector2Int to;

        public Connection(
            Vector2Int from,
            Vector2Int to)
        {
            this.from = from;
            this.to = to;
        }
    }

    private void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        roomPositions.Clear();
        connections.Clear();

        Vector2Int startPos =
            Vector2Int.zero;

        roomPositions.Add(startPos);

        GameObject startRoom =
            Instantiate(
                startRoomPrefab,
                GridToWorld(startPos),
                Quaternion.identity);

        if (player != null)
        {
            player.position =
                GridToWorld(startPos);
        }

        // 첫 전투방 생성
        Vector2Int firstDir =
            GetRandomDirection();

        Vector2Int firstBattlePos =
            startPos + firstDir;

        roomPositions.Add(firstBattlePos);

        connections.Add(
            new Connection(
                startPos,
                firstBattlePos));

        // 나머지 전투방 생성
        for (int i = 1;
             i < battleRoomCount;
             i++)
        {
            CreateBattleRoom();
        }

        // 전투방 생성
        for (int i = 1;
             i < roomPositions.Count;
             i++)
        {
            Vector2Int roomPos =
                roomPositions[i];

            GameObject room =
                Instantiate(
                    battleRoomPrefab,
                    GridToWorld(roomPos),
                    Quaternion.identity);

            RoomController roomController =
                room.GetComponent<RoomController>();

            if (roomController != null)
            {
                bool up;
                bool down;
                bool left;
                bool right;

                GetConnections(
                    roomPos,
                    out up,
                    out down,
                    out left,
                    out right);

                roomController.SetupWalls(
                    up,
                    down,
                    left,
                    right);
            }
        }

        // 시작방 벽 설정
        RoomController startController =
            startRoom.GetComponent<RoomController>();

        if (startController != null)
        {
            bool up;
            bool down;
            bool left;
            bool right;

            GetConnections(
                startPos,
                out up,
                out down,
                out left,
                out right);

            startController.SetupWalls(
                up,
                down,
                left,
                right);
        }

        CreateCorridors();

        if (minimapGenerator != null)
        {
            minimapGenerator.GenerateMap(
                roomPositions);
        }
    }

    private void CreateBattleRoom()
    {
        bool created = false;

        while (!created)
        {
            Vector2Int parentRoom =
                roomPositions[
                    Random.Range(
                        1,
                        roomPositions.Count)];

            Vector2Int direction =
                GetRandomDirection();

            Vector2Int newRoomPos =
                parentRoom + direction;

            if (!roomPositions.Contains(
                newRoomPos))
            {
                roomPositions.Add(
                    newRoomPos);

                connections.Add(
                    new Connection(
                        parentRoom,
                        newRoomPos));

                created = true;
            }
        }
    }

    private void GetConnections(
        Vector2Int roomPos,
        out bool up,
        out bool down,
        out bool left,
        out bool right)
    {
        up = false;
        down = false;
        left = false;
        right = false;

        foreach (Connection connection
                 in connections)
        {
            if (connection.from ==
                roomPos)
            {
                Vector2Int dir =
                    connection.to -
                    connection.from;

                if (dir ==
                    Vector2Int.up)
                    up = true;

                if (dir ==
                    Vector2Int.down)
                    down = true;

                if (dir ==
                    Vector2Int.left)
                    left = true;

                if (dir ==
                    Vector2Int.right)
                    right = true;
            }

            if (connection.to ==
                roomPos)
            {
                Vector2Int dir =
                    connection.from -
                    connection.to;

                if (dir ==
                    Vector2Int.up)
                    up = true;

                if (dir ==
                    Vector2Int.down)
                    down = true;

                if (dir ==
                    Vector2Int.left)
                    left = true;

                if (dir ==
                    Vector2Int.right)
                    right = true;
            }
        }
    }

    private void CreateCorridors()
    {
        foreach (Connection connection
                 in connections)
        {
            Vector3 fromPos =
                GridToWorld(
                    connection.from);

            Vector3 toPos =
                GridToWorld(
                    connection.to);

            Vector3 corridorPos =
                (fromPos + toPos)
                / 2f;

            Vector2Int direction =
                connection.to -
                connection.from;

            if (direction ==
                    Vector2Int.left ||
                direction ==
                    Vector2Int.right)
            {
                Instantiate(
                    horizontalCorridorPrefab,
                    corridorPos,
                    Quaternion.identity);
            }
            else
            {
                Instantiate(
                    verticalCorridorPrefab,
                    corridorPos,
                    Quaternion.identity);
            }
        }
    }

    private Vector2Int
        GetRandomDirection()
    {
        int random =
            Random.Range(0, 4);

        switch (random)
        {
            case 0:
                return Vector2Int.up;

            case 1:
                return Vector2Int.down;

            case 2:
                return Vector2Int.left;

            default:
                return Vector2Int.right;
        }
    }

    private Vector3 GridToWorld(
        Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x *
            roomDistance,
            gridPos.y *
            roomDistance,
            0f);
    }
}
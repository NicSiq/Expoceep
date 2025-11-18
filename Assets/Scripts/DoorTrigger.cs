using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public enum DoorDirection { North, South, East, West }
    public DoorDirection direction;

    [Header("Prefabs (Arraste aqui)")]
    public GameObject corridorPrefab;
    public GameObject roomPrefab;

    [Header("Medidas (Preenchidas no Inspector)")]
    public float roomSize = 10f;
    public float corridorSize = 5f;

    private RoomController roomController;
    private bool hasSpawned = false;

    // ... (Start e OnTriggerEnter2D inalterados) ...

    void Start()
    {
        roomController = GetComponentInParent<RoomController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            if (roomController != null)
            {
                roomController.OnDoorEntered(this);
            }

            SpawnNextSection();

            hasSpawned = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }


    void SpawnNextSection()
    {
        Vector3 doorPos = transform.position;
        // Posição central da sala atual (Sala 1)
        Vector3 currentRoomCenter = roomController.transform.position;


        Vector3 corridorPos = Vector3.zero;
        Vector3 nextRoomPos = Vector3.zero;
        Quaternion corridorRot = Quaternion.identity;

        float halfCorridor = corridorSize / 2f;

        // --- SUAS DISTÂNCIAS FIXAS ---
        const float DIST_Y_NORTH = 30f;
        const float DIST_Y_SOUTH = 37f; // Usaremos 37f como o valor absoluto
        const float DIST_X_EAST = 34f;
        const float DIST_X_WEST = 34f;  // Usaremos 34f como o valor absoluto
        // -----------------------------

        switch (direction)
        {
            case DoorDirection.North:
                corridorPos = new Vector3(doorPos.x, doorPos.y + halfCorridor, 0);
                // A posição da próxima sala é o centro da sala atual + a distância fixa
                nextRoomPos = currentRoomCenter + new Vector3(0, DIST_Y_NORTH, 0);
                corridorRot = Quaternion.identity;
                break;

            case DoorDirection.South:
                corridorPos = new Vector3(doorPos.x, doorPos.y - halfCorridor, 0);
                // A posição da próxima sala é o centro da sala atual - a distância fixa
                nextRoomPos = currentRoomCenter + new Vector3(0, -DIST_Y_SOUTH, 0);
                corridorRot = Quaternion.identity;
                break;

            case DoorDirection.East:
                corridorPos = new Vector3(doorPos.x + halfCorridor, doorPos.y, 0);
                // A posição da próxima sala é o centro da sala atual + a distância fixa
                nextRoomPos = currentRoomCenter + new Vector3(DIST_X_EAST, 0, 0);
                corridorRot = Quaternion.Euler(0, 0, -90);
                break;

            case DoorDirection.West:
                corridorPos = new Vector3(doorPos.x - halfCorridor, doorPos.y, 0);
                // A posição da próxima sala é o centro da sala atual - a distância fixa
                nextRoomPos = currentRoomCenter + new Vector3(-DIST_X_WEST, 0, 0);
                corridorRot = Quaternion.Euler(0, 0, 90);
                break;
        }

        // 1. Instancia
        GameObject newCorridor = Instantiate(corridorPrefab, corridorPos, corridorRot);
        GameObject newRoom = Instantiate(roomPrefab, nextRoomPos, Quaternion.identity);

        // ... (Configuração do CorridorTrigger) ...
        Vector3 nextRoomCenter = newRoom.transform.position;

        if (GameManager.instance != null)
        {
            CorridorTrigger transitionTrigger = newCorridor.GetComponentInChildren<CorridorTrigger>();
            if (transitionTrigger != null)
            {
                transitionTrigger.nextRoomPosition = nextRoomCenter;
            }
        }
    }
}
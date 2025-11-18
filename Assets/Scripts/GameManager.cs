using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Assets")]
    public GameObject initialRoomPrefab;

    [Header("Referências")]
    // Referência da câmera mantida, mas não usada para movimento neste script.
    public Transform mainCamera;
    // cameraMoveSpeed REMOVIDA

    // Variáveis de movimento de câmera (targetCameraPosition e isMovingCamera) REMOVIDAS

    void Awake()
    {
        // Implementação do Singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitGame();
    }

    // O Update que continha a lógica de MoveTowards da câmera foi REMOVIDO
    void Update()
    {

    }

    void InitGame()
    {
        GameObject currentRoom = Instantiate(initialRoomPrefab, Vector3.zero, Quaternion.identity);

        Transform spawnPoint = currentRoom.transform.Find("PlayerSpawnPoint");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;

            // Chamada MoveCameraTo REMOVIDA, pois a câmera segue o Player automaticamente.
            /*
            if (mainCamera != null)
            {
                MoveCameraTo(currentRoom.transform.position); 
            }
            */
        }
        else
        {
            Debug.LogError("ERRO: Falha ao spawnar. Verifique Tag 'Player' ou SpawnPoint.");
        }
    }

    // A função MoveCameraTo foi REMOVIDA
}
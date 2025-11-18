using UnityEngine;

// O NOME DESTA CLASSE DEVE SER EXATAMENTE CameraFollow
public class CameraFollow : MonoBehaviour
{
    [Header("Configuração")]
    public Transform target;

    private float zOffset;

    void Start()
    {
        zOffset = transform.position.z;

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("CameraFollow: Jogador (Tag 'Player') não encontrado.");
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(
                target.position.x,
                target.position.y,
                zOffset
            );
        }
    }
}
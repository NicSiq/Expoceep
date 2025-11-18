using UnityEngine;

public class RoomController : MonoBehaviour
{
    // Arraste todas as 4 Portas_X aqui no Inspector
    public DoorTrigger[] doors;
    public GameObject wallBlockPrefab; // Opcional: Prefab de Parede para bloquear a porta

    // Recebe o trigger da porta que o jogador escolheu
    public void OnDoorEntered(DoorTrigger enteredDoor)
    {
        foreach (DoorTrigger door in doors)
        {
            if (door != enteredDoor)
            {
                // Bloqueia as outras 3 portas

                // 1. Desativa o objeto de gatilho
                door.gameObject.SetActive(false);

                // 2. Opcional: Coloca uma parede visual/física no lugar
                // if (wallBlockPrefab != null)
                // {
                //    Instantiate(wallBlockPrefab, door.transform.position, Quaternion.identity, transform);
                // }
            }
        }
    }
}
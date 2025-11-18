using UnityEngine;

public class CorridorTrigger : MonoBehaviour
{
    // Variável necessária para o DoorTrigger configurar a posição da Sala 2
    public Vector3 nextRoomPosition;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            // O Trigger é disparado.
            triggered = true;

            // O objetivo principal agora é DESATIVAR este Collider 
            // para que o jogador não o acione novamente e cause problemas.
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
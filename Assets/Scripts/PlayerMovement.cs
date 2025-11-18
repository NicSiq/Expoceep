using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // A velocidade de movimento, ajustável no Inspector
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Corrigido para a versão que você enviou, removendo o ajuste de posição para Vector3.zero
        // Você deve garantir que o spawn do player seja gerenciado pelo GameManager.InitGame().
    }

    void Update()
    {
        // Captura a entrada do usuário (W/A/S/D)
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        // Normaliza o vetor para que o movimento diagonal não seja mais rápido
        movementInput.Normalize();
    }

    void FixedUpdate()
    {
        // Calcula a nova posição baseada na entrada, velocidade e tempo físico
        Vector2 newPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;

        // Move o Rigidbody para a nova posição
        rb.MovePosition(newPosition);
    }
}
using UnityEngine;

public class IAController : MonoBehaviour
{
    public Transform puck;
    public float speed = 5f;
    public float activationDistance = 2f; // Distance at which AI moves toward puck
    public float stopDistance = 0.5f;
    private Rigidbody2D rb;
    private Vector2 startPosition;

    // Defina os limites da área
    public float leftLimit = -10f;
    public float rightLimit = 10f;
    public float topLimit = 3.1f;
    public float bottomLimit = 0.25f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Record initial position
    }

    void FixedUpdate()
    {
        if (puck == null) return;

        float puckDistance = Vector2.Distance(transform.position, puck.position);

        Vector2 targetPosition = puckDistance < activationDistance ? (Vector2)puck.position : startPosition;

        // Restrinja o movimento para os limites da área
        targetPosition = ClampPositionToBounds(targetPosition);

        MoveTowards(targetPosition);
    }

    void MoveTowards(Vector2 target)
    {
        float distance = Vector2.Distance(transform.position, target);

        if (distance > stopDistance)
        {
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop when near target
        }
    }

    // Método para limitar a posição da IA dentro dos limites
    Vector2 ClampPositionToBounds(Vector2 targetPosition)
    {
        float clampedX = Mathf.Clamp(targetPosition.x, leftLimit, rightLimit);
        float clampedY = Mathf.Clamp(targetPosition.y, bottomLimit, topLimit);

        return new Vector2(clampedX, clampedY);
    }
}

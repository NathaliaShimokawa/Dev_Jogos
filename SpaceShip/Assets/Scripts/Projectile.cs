using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public Vector3 direction = Vector3.right;  // Direção para a direita por padrão
    public float speed = 20f;
    private Rigidbody2D rb;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Configura a velocidade do projétil com base na direção
        rb.linearVelocity = direction.normalized * speed;  // Movimento horizontal
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Removido: Não verifica mais colisão com o Bunker
        Destroy(gameObject); // Destrói o projétil ao colidir com qualquer objeto
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Removido: Não verifica mais colisão com o Bunker
        Destroy(gameObject); // Destrói o projétil enquanto estiver em contato com qualquer objeto
    }
}

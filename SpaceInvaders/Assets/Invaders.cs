using UnityEngine;

public class Invaders : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 1.0f;
    private float speed = 2.0f;

    void ChangeState()
    {
        var vel = rb2d.linearVelocity;  // Corrigido para `velocity` em vez de `linearVelocity`
        vel.x *= -1;
        rb2d.linearVelocity = vel;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        var vel = rb2d.linearVelocity;
        vel.x = speed;
        rb2d.linearVelocity = vel;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            ChangeState();
            timer = 0.0f;
        }
    }

    // Método para detectar colisões com o míssel
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto que colidiu com o invader é um míssel
        if (collision.gameObject.CompareTag("Tiro"))  // Certifique-se de que o míssel tem a tag "Missile"
        {
            Destroy(collision.gameObject);  // Destrói o míssel
            Destroy(gameObject);  // Destrói o invader
        }
    }
}

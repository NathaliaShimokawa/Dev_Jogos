using UnityEngine;

public class Nave : MonoBehaviour
{
    public GameObject bullet;  // Prefab do míssel
    public float missileSpeed = 10f;  // A velocidade do míssel
    public float moveSpeed = 5f;  // A velocidade de movimentação da nave
    public GameObject spawnerBulletPos; // Posição onde o tiro será instanciado

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a nave colidiu com um objeto de camada 8
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);  // Destrói a nave
        }
    }

    void Update()
    {
        // Movimentação da nave com as teclas de seta
        float moveInput = Input.GetAxis("Horizontal");  // Captura a entrada do eixo horizontal (setas ou A/D)
        transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);

        // Verifica se o jogador pressionou a seta para baixo
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShootMissile();
        }
    }

    // Método para disparar o míssel
    void ShootMissile()
    {
        // Instancia o míssel na posição de onde o tiro deve sair
        GameObject missile = Instantiate(bullet, spawnerBulletPos.transform.position, Quaternion.identity);

        // Acesse o Rigidbody2D do míssel e defina a velocidade
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            // Desabilita a gravidade para o míssel não cair
            rb.gravityScale = 0;
            
            // Aplica a velocidade para cima
            rb.linearVelocity = Vector2.up * missileSpeed;
        }
    }
}

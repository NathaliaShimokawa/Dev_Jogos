using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField]
    private float speed = 0.8f;
    private float xMax = 8f;
    private float yMax = 6f;
    private float xRandom;

    private GameManager gameManager;  // Referência ao GameManager

    void Start() {
        // Inicializa o inimigo no canto direito da tela, com posição aleatória no eixo Y
        xRandom = Random.Range(-xMax, xMax);
        transform.position = new Vector3(xMax, xRandom, 0); // Posição no canto direito (lado X = xMax)

        // Obtém a referência ao GameManager
        gameManager = GameManager.Instance;  // Usando a instância singleton do GameManager
    }

    void Update() {
        // Movimenta o inimigo para a esquerda
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Se o inimigo sair da tela pelo lado esquerdo, ele reaparece no lado direito
        if (transform.position.x <= -xMax) {
            xRandom = Random.Range(-yMax, yMax);
            transform.position = new Vector3(xMax, xRandom, 0); // Volta para o canto direito com nova posição Y
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica se houve colisão com o Player
        if (collision.CompareTag("Player")) {
            // Destrói o Player quando ocorre a colisão
            Destroy(collision.gameObject);  // Destrói o objeto Player
            Destroy(this.gameObject);  // Destrói o inimigo

            // Chama a função OnEnemyCollidedWithPlayer do GameManager para terminar o jogo
            if (gameManager != null) {
                gameManager.OnEnemyCollidedWithPlayer();  // Termina o jogo
            }
        }

        // Verifica se houve colisão com o laser
        if (collision.CompareTag("Laser")) {
            Destroy(this.gameObject);  // Destrói o inimigo
            Destroy(collision.gameObject);  // Destrói o laser

            // Incrementa a pontuação
            if (gameManager != null) {
                gameManager.OnEnemyKilled(this);  // Aumenta o score
            }
        }
    }
    public void ResetEnemy()
{
    xRandom = Random.Range(-yMax, yMax);
    transform.position = new Vector3(xMax, xRandom, 0); // Reposiciona o inimigo no lado direito
    gameObject.SetActive(true); // Garante que o inimigo esteja ativo
}

}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private Player player;
    private Enemy[] enemies;

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = Object.FindFirstObjectByType<Player>();  // Encontra o player
        enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);  // Encontra todos os inimigos

        NewGame();
    }

    private void Update()
    {
        // Verifica se o número de vidas é menor ou igual a 0 e o jogador pressionou Enter para reiniciar o jogo
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

        // Verifica se a pontuação atingiu 20, e então chama o método para vencer o jogo
        if (score >= 20)
        {
            OnGameWon();
        }
    }

    private void NewGame()
    {
        gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.ResetEnemy();  // Reseta os inimigos (ajuste necessário para a função de reset no Enemy)
            enemy.gameObject.SetActive(true);
        }

        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false);  // Desativa os inimigos quando o jogo termina
        }
        SceneManager.LoadScene("Lose_Game");  // Carrega a cena de Game Over
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
        livesText.text = this.lives.ToString();
    }

    public void OnPlayerKilled(Player player)
    {
        SetLives(lives - 1);

        player.gameObject.SetActive(false);

        if (lives > 0)
        {
            Invoke(nameof(NewRound), 1f);  // Inicia uma nova rodada se ainda tiver vidas
        }
        else
        {
            GameOver();  // Se o jogador não tiver mais vidas, vai para o Game Over
        }
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);  // Desativa o inimigo

        SetScore(score + 1);  // Aumenta a pontuação por matar um inimigo

        // Verifica se todos os inimigos foram destruídos
        bool allEnemiesKilled = true;
        foreach (Enemy e in enemies)
        {
            if (e.gameObject.activeSelf)
            {
                allEnemiesKilled = false;
                break;
            }
        }

        if (allEnemiesKilled)
        {
            NewRound();  // Começa uma nova rodada se todos os inimigos forem destruídos
        }
    }

    // Modificado para verificar a colisão do inimigo com o jogador
    public void OnEnemyCollidedWithPlayer()
    {
        GameOver();  // Chama o Game Over quando um inimigo colide com o jogador
    }

    public void OnGameWon()
    {
        SceneManager.LoadScene("Win_Game");  // Carrega a cena de Vitória
    }
}

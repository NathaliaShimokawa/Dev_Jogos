using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text scoreText;

    private PlayerBehaviour player;
    private EnemyBehaviour[] enemies;

    public int score { get; private set; } = 0;

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
        player = Object.FindFirstObjectByType<PlayerBehaviour>();
        enemies = Object.FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None);

        if (player == null)
        {
            Debug.LogError("PlayerBehaviour não encontrado na cena!");
        }

        NewGame();
    }

    private void Update()
    {
        // Se o score atingir 20, chama a função de vitória
        if (score >= 20)
        {
            OnGameWon();
        }
    }

    private void NewGame()
    {
        gameOverUI.SetActive(false);
        SetScore(0);
        NewRound();
    }

    private void NewRound()
    {
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.ResetEnemy();
            enemy.gameObject.SetActive(true);
        }

        Respawn();
    }

    private void Respawn()
    {
        if (player != null)
        {
            Vector3 position = player.transform.position;
            position.x = 0f;
            player.transform.position = position;
            player.gameObject.SetActive(true);
        }
    }

    private void GameOver()
    {
        // Desativa todos os inimigos quando o jogo termina
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }

        // Carrega a cena de "Lose_Game"
        SceneManager.LoadScene("Lose_Game");
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    public void OnPlayerKilled(PlayerBehaviour player)
    {
        player.gameObject.SetActive(false);
        GameOver();  // Se o jogador for destruído, o jogo termina
    }

    public void OnEnemyKilled(EnemyBehaviour enemy)
    {
        enemy.gameObject.SetActive(false);
        SetScore(score + 1);  // Aumenta o score

        // Verifica se todos os inimigos foram mortos
        bool allEnemiesKilled = true;
        foreach (EnemyBehaviour e in enemies)
        {
            if (e.gameObject.activeSelf)
            {
                allEnemiesKilled = false;
                break;
            }
        }

        // Inicia uma nova rodada se todos os inimigos foram mortos
        if (allEnemiesKilled)
        {
            NewRound();
        }
    }

    public void OnEnemyCollidedWithPlayer()
    {
        // Chama a função de Game Over se um inimigo colidir com o jogador
        GameOver();
    }

    public void OnGameWon()
    {
        // Carrega a cena de "Win_Game" quando o jogador atingir o score 20
        SceneManager.LoadScene("Win_Game");
    }
}

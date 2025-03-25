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

    private PlayerBehaviour player;
    private EnemyBehaviour[] enemies;

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
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

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
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        SceneManager.LoadScene("Lose_Game");
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

    public void OnPlayerKilled(PlayerBehaviour player)
    {
        SetLives(lives - 1);
        player.gameObject.SetActive(false);

        if (lives > 0)
        {
            Invoke(nameof(NewRound), 1f);
        }
        else
        {
            GameOver();
        }
    }

    public void OnEnemyKilled(EnemyBehaviour enemy)
    {
        enemy.gameObject.SetActive(false);
        SetScore(score + 1);

        bool allEnemiesKilled = true;
        foreach (EnemyBehaviour e in enemies)
        {
            if (e.gameObject.activeSelf)
            {
                allEnemiesKilled = false;
                break;
            }
        }

        if (allEnemiesKilled)
        {
            NewRound();
        }
    }

    public void OnEnemyCollidedWithPlayer()
    {
        GameOver();
    }

    public void OnGameWon()
    {
        SceneManager.LoadScene("Win_Game");
    }
}
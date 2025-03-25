using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {

    [SerializeField]
    private float speed = 5;
    private float verticalInput;

    [SerializeField]
    private float yMax = 4.1f;

    [SerializeField]
    private GameObject laserPrefab = null;

    [SerializeField]
    private float fireRate = 0.25f;
    private float nextFire = 0.0f;
    
    private float powerupTime = 5.0f;
    private bool canSpeed = false;

    private int lifes = 3;
    public int score = 0;
    public Text scoreText;

    void Start() {
        transform.position = new Vector3(-7, 0, 0); // Posiciona o jogador no centro
    }

    void Update() {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }

        // Se o número de vidas for menor que 1, vai para o game over
        if (lifes < 1) {
            StartCoroutine(GoToGameOverScene());
        }

        scoreText.text = score.ToString();
    }

    IEnumerator GoToGameOverScene() {
        yield return new WaitForSeconds(2f);  // Espera 2 segundos antes de ir para a tela de game over
        SceneManager.LoadScene("GameOver");
    }

    private void Shoot() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            // Dispara o laser a partir da posição do jogador
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f), Quaternion.identity);
        }
    }

    private void Movement() {
        verticalInput = Input.GetAxis("Vertical");

        // Movimento do jogador com velocidade ajustada
        if (canSpeed) {
            transform.Translate(Vector3.up * (speed + 7f) * verticalInput * Time.deltaTime);
        } else {
            transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        }

        // Limitar a posição vertical dentro do intervalo
        if (transform.position.y >= yMax) {
            transform.position = new Vector3(transform.position.x, -yMax);
        } else if (transform.position.y <= -yMax) {
            transform.position = new Vector3(transform.position.x, yMax);
        }
    }

    public void Hit() {
        lifes--;
        Debug.Log("Lifes: " + lifes);
    }

    public void SpeedPowerOn() {
        canSpeed = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine() {
        yield return new WaitForSeconds(powerupTime);
        canSpeed = false;
    }
}

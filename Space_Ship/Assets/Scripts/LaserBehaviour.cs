using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    [SerializeField]
    private float speed = 10f;  // Velocidade do laser
    public GameObject player;  // Referência ao jogador (Player)

    void Start() {
        // Caso o 'player' não tenha sido atribuído no Inspector, tentamos localizar o objeto com a tag "Player"
        if (player == null) {
            player = GameObject.FindWithTag("Player");
        }
    }

    void Update() {
        // O laser agora se move para a direita (pode alterar para outro movimento se necessário)
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Limita a destruição do laser quando ele sai da tela (lado direito)
        if (transform.position.x >= 6f) {
            Destroy(gameObject);  // Destrói o laser após atingir a borda da tela
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            // Aumenta a pontuação do jogador ao atingir um inimigo
            if (player != null) {
                player.GetComponent<PlayerBehaviour>().score++;  // Incrementa o score do jogador
            }
            
            Destroy(collision.gameObject);  // Destrói o inimigo
            Destroy(gameObject);  // Destrói o laser
        }
    }
}

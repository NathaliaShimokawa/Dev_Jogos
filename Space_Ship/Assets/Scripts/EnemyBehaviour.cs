﻿using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.8f;
    private float originalSpeed;
    private float xMax = 8f;
    private float yMax = 6f;
    private float xRandom;

    private GameManager gameManager;  // Referência ao GameManager

    void Start()
    {
        // Armazena a velocidade original
        originalSpeed = speed;

        // Inicializa o inimigo no canto direito da tela, com posição aleatória no eixo Y
        xRandom = Random.Range(-xMax, xMax);
        transform.position = new Vector3(xMax, xRandom, 0); // Posição no canto direito (lado X = xMax)

        // Obtém a referência ao GameManager
        gameManager = GameManager.Instance;  // Usando a instância singleton do GameManager
    }

    void Update()
    {
        // Movimenta o inimigo para a esquerda
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Se o inimigo sair da tela pelo lado esquerdo, ele reaparece no lado direito
        if (transform.position.x <= -xMax)
        {
            xRandom = Random.Range(-yMax, yMax);
            transform.position = new Vector3(xMax, xRandom, 0); // Volta para o canto direito com nova posição Y
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se houve colisão com o Player
        if (collision.CompareTag("Player"))
        {
            // Destrói o Player quando ocorre a colisão
            Destroy(collision.gameObject);  // Destrói o objeto Player
            Destroy(this.gameObject);  // Destrói o inimigo

            // Chama a função OnEnemyCollidedWithPlayer do GameManager para terminar o jogo
            if (gameManager != null)
            {
                gameManager.OnEnemyCollidedWithPlayer();  // Termina o jogo
                StartCoroutine(WaitAndInitMenu()); // Espera 2 segundos antes de chamar o menu
            }
        }

        // Verifica se houve colisão com o laser
        if (collision.CompareTag("Laser"))
        {
            Destroy(this.gameObject);  // Destrói o inimigo
            Destroy(collision.gameObject);  // Destrói o laser

            // Incrementa a pontuação
            if (gameManager != null)
            {
                gameManager.OnEnemyKilled(this);  // Aumenta o score
            }
        }
    }

    // Método para desacelerar o inimigo
    public void SlowDownEnemy()
    {
        speed *= 0.5f;  // Reduz a velocidade do inimigo (50% da velocidade original)
    }

    // Método para restaurar a velocidade original do inimigo
    public void RestoreSpeed()
    {
        speed = originalSpeed;  // Restaura a velocidade original
    }

    public void ResetEnemy()
    {
        xRandom = Random.Range(-yMax, yMax);
        transform.position = new Vector3(xMax, xRandom, 0); // Reposiciona o inimigo no lado direito
        gameObject.SetActive(true); // Garante que o inimigo esteja ativo
    }

    private IEnumerator WaitAndInitMenu()
    {
        yield return new WaitForSeconds(2f); // Aguarda 2 segundos
        gameManager.InitMenu(); // Inicia o menu após a pausa
    }
}

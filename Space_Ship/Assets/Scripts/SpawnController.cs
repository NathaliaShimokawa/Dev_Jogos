using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [SerializeField]
    private GameObject enemyPrefab;   // Prefab do inimigo
    [SerializeField]
    private GameObject powerupPrefab; // Prefab do power-up (agora só existe um)

    private readonly float xMax = 8f; // Limite máximo no eixo X
    private readonly float yPos = 6.25f; // Posição Y onde os inimigos aparecem inicialmente
    private readonly float yMax = 4f; // Limite máximo no eixo Y para o posicionamento de powerups

    void Start() {
        // Inicia o spawn de inimigos e power-ups com um intervalo
        InvokeRepeating("InstantiateEnemy", 1f, 2f);
        InvokeRepeating("InstantiatePowerup", 5f, 5f);
    }

    void Update() {
        // Não há necessidade de atualizações contínuas no momento, a lógica está nas funções de spawn
    }

    // Função para instanciar inimigos
    private void InstantiateEnemy() {
        // Gera uma posição aleatória no eixo X (dentro da tela)
        Vector3 randomPosition = new Vector3(Random.Range(-xMax, xMax), yPos, 0);
        // Instancia o inimigo na posição gerada
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    // Função para instanciar power-ups
    private void InstantiatePowerup() {
        // Gera uma posição aleatória no eixo X (dentro da tela) e uma posição Y dentro da área visível
        Vector3 randomPosition = new Vector3(Random.Range(-xMax, xMax), Random.Range(-yMax, yMax), 0);
        // Instancia o único power-up na posição gerada
        Instantiate(powerupPrefab, randomPosition, Quaternion.identity);
    }
}

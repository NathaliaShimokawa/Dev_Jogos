using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour : MonoBehaviour
{
    private float speed = 1f;

    [SerializeField]
    private bool isSpeedPowerup = true; // Define que o power-up será de Speed

    void Start()
    {
        // Nenhum código necessário no Start, já que tudo ocorre no Update ou no OnTriggerEnter2D
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quando o power-up colide com o jogador
        if (collision.CompareTag("Player"))
        {
            // Verifica se o power-up é do tipo Speed
            if (isSpeedPowerup)
            {
                collision.GetComponent<PlayerBehaviour>().SpeedPowerOn();

                // Desacelera todos os inimigos na cena
                EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
                foreach (var enemy in enemies)
                {
                    enemy.SlowDownEnemy();
                }
            }

            // Após ativar o power-up, destrua o objeto
            Destroy(gameObject);
        }
    }
}

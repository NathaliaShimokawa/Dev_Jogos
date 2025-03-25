using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Projectile laserPrefab;
    private Projectile laser;

    private void Update()
    {
        Vector3 position = transform.position;

        // Atualiza a posição do jogador com base na entrada
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            position.y += speed * Time.deltaTime;  // Movimento para cima
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            position.y -= speed * Time.deltaTime;  // Movimento para baixo
        }

        // Limita a posição do personagem para que ele não saia da tela
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        position.y = Mathf.Clamp(position.y, bottomEdge.y, topEdge.y);

        // Define a nova posição
        transform.position = position;

        // Verifica se o jogador pressionou a tecla espaço para disparar o laser
        if (laser == null && Input.GetKeyDown(KeyCode.Space)) {
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }
}

using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 10.0f;             // Define a velocidade da raquete
    public float boundY = 2.85f;            // Define os limites em Y
    private Rigidbody2D rb2d;               // Define o corpo rígido 2D que representa a raquete

    public float upperLimit = 0.25f;           
    public float lowerLimit = -2.6f;         
    public float leftLimit = -10f;           
    public float rightLimit = 10f;           

    // Start é chamado uma vez antes da primeira execução do Update após o MonoBehaviour ser criado
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa a raquete
    }

    // Update é chamado uma vez por quadro
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = transform.position;

        // Restringe a posição do jogador nos eixos X e Y
        pos.x = Mathf.Clamp(mousePos.x, leftLimit, rightLimit); 
        pos.y = Mathf.Clamp(mousePos.y, lowerLimit, upperLimit);

        transform.position = pos; // Atualiza a posição da raquete
    }
}

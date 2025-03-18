using UnityEngine;
using UnityEngine.UI; 


public class GameController : MonoBehaviour
{

    public static int score = 0; // Pontuação do jogo
    public Text scoreText;       // Referência ao Text da UI para mostrar o score

    void Update()
    {
        // Atualiza a UI a cada frame
        scoreText.text = "Score: " + score.ToString();
    }

    // Método para aumentar o score
    public static void AddScore(int value)
    {
        score += value; // Adiciona pontos
    }


}

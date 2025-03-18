using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0; // Pontuação do player 1
    public static int PlayerScore2 = 0; // Pontuação do player 2

    public GUISkin layout;              // Fonte do placar
    GameObject theBall;                 // Referência ao objeto bola

    // Incrementa a pontuação
    public static void Score (string wallID) {
        if (wallID == "TopGoal")
        {
            PlayerScore1++;
        } else {   
            PlayerScore2++;
        }
    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI () {
        // Definindo o GUISkin
        GUI.skin = layout;

        // Exibe o placar com a cor definida no GUISkin
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            theBall.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }

        if (PlayerScore1 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        } 
        else if (PlayerScore2 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER TWO WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }

    // Start é chamado uma vez antes da primeira execução do Update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball"); // Busca a referência da bola
    }

    // Update é chamado uma vez por frame
    void Update()
    {
    }
}

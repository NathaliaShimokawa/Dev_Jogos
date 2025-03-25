using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitGame()
    {
        // Carrega a cena de "Win_Game" quando o jogador atingir o score 20
        SceneManager.LoadScene("Game");
    }
}

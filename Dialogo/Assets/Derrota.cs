using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Derrota : MonoBehaviour
{
    public GUISkin layout;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 50, 160, 100), "You Lose");

        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2, 120, 53), "RESTART"))
        {
            SceneManager.LoadScene("Inicial");
        }
    }
}

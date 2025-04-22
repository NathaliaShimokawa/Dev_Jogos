using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLose : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     [SerializeField]
    private string cena;

    // Update is called once per frame
    public void changeScene()
    {
        
    SceneManager.LoadScene(this.cena);
        
    }
}

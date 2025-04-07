using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Carnes");
        print(gos.Length);
        if(gos.Length == 0){
            if (scene.name == "cena1"){
                SceneManager.LoadScene("cena2");
            }
            if (scene.name == "cena2")
            {
                SceneManager.LoadScene("Win");
            }
        }
    }

}

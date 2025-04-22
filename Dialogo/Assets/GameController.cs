using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gc;
    public Text estrelaText;
    public Text vidasText;
    public int estrelas;
    public int vidas;

    void Awake()
    {
        if (gc == null){
            gc = this;
        }
        else if (gc != this){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

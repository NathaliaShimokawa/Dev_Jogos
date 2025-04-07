using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlle : MonoBehaviour
{
    public static GameControlle gc;
    public Text carneText;
    public int carnes;
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

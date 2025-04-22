using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class transicaocena : MonoBehaviour
{
    [SerializeField]
    private string cena;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player")
    {
        SceneManager.LoadScene(this.cena);
    }

    }

}

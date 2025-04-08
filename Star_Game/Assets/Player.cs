using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpforce;
    private bool inFloor = true;

    private GameController gcPlayer;
    // Start is called before the first frame update
    void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.vidas = 3;
        gcPlayer.estrelas = 0;
        playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate(){
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void MovePlayer()
    {
        float horizontalMoviment = Input.GetAxisRaw("Horizontal");
        rbPlayer.linearVelocity = new Vector2(horizontalMoviment * speed, rbPlayer.linearVelocity.y);
    
        if (horizontalMoviment > 0){
            playerAnim.SetBool("Walk", true);
            sr.flipX = false;
        }
        else if (horizontalMoviment < 0){
            playerAnim.SetBool("Walk", true);
            sr.flipX = true;
        }
        else{
            playerAnim.SetBool("Walk", false);
        }
    }

    void Jump(){
        if (Input.GetButtonDown("Jump") && inFloor){
            playerAnim.SetBool("Jump", true);
            rbPlayer.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            inFloor = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap"){
            playerAnim.SetBool("Jump", false);
            inFloor = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Estrelas")
    {
        Destroy(collision.gameObject);
        gcPlayer.estrelas++;
        gcPlayer.estrelaText.text = gcPlayer.estrelas.ToString();
    }
    else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    {
        gcPlayer.vidas--;
        gcPlayer.vidasText.text = gcPlayer.vidas.ToString();
        Destroy(collision.gameObject);

        if (gcPlayer.vidas <= 0)
        {
            SceneManager.LoadScene("derrota");
        }
    }
    if (collision.gameObject.tag == "saida" && gcPlayer.estrelas >= 12)
    {
        SceneManager.LoadScene("Cena2");
        Debug.Log("Sua mensagem aqui" + gcPlayer.estrelas);

    }
}
}

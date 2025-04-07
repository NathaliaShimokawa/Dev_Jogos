using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpforce;
    private bool inFloor = true;

    private GameControlle gcPlayer;
    // Start is called before the first frame update
    void Start()
    {
        gcPlayer = GameControlle.gc;
        gcPlayer.carnes = 0;
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
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Estrelas"){
            Destroy(collision.gameObject);
            gcPlayer.carnes++;
            gcPlayer.carneText.text = gcPlayer.carnes.ToString();
        }
    }
}

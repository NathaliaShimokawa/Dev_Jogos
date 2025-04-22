using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpforce;
    private bool inFloor = true;

    private GameController gcPlayer;

    // UI de diálogo
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    private bool shown7 = false;
    private bool shown12 = false;

    void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.vidas = 3;
        gcPlayer.estrelas = 0;
        playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
        dialogBox.SetActive(false);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        Jump();

        // Diálogo das 7 estrelas
        if (gcPlayer.estrelas == 7 && !shown7)
        {
            dialogBox.SetActive(true);
            dialogText.text = "Você coletou 7 estrelas! Faltam só 5 para a próxima fase!";
            shown7 = true;
        }
        else if (gcPlayer.estrelas > 7 && gcPlayer.estrelas < 12 && dialogBox.activeSelf)
        {
            dialogBox.SetActive(false);
        }

        // Diálogo das 12 estrelas (antes de trocar de fase)
        if (gcPlayer.estrelas == 12 && !shown12)
        {
            dialogBox.SetActive(true);
            dialogText.text = "Parabéns! Você coletou todas as estrelas!";
            shown12 = true;
            StartCoroutine(NextSceneAfterDelay(2f)); // Espera 2 segundos antes de trocar de cena
        }
    }

    void MovePlayer()
    {
        float horizontalMoviment = Input.GetAxisRaw("Horizontal");
        rbPlayer.linearVelocity = new Vector2(horizontalMoviment * speed, rbPlayer.linearVelocity.y);

        if (horizontalMoviment > 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = false;
        }
        else if (horizontalMoviment < 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = true;
        }
        else
        {
            playerAnim.SetBool("Walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && inFloor)
        {
            playerAnim.SetBool("Jump", true);
            rbPlayer.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            inFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
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
            // Agora a cena será trocada pela coroutine
            // Esta parte pode ser mantida como segurança, ou removida se quiser forçar apenas pela coroutine
        }
    }

    IEnumerator NextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Cena2");
    }
}

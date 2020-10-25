using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float hozMovement;
    public float verMovement;
    public float speed;
    public Text score;
    public Text lives;
    public Text winText;

    private int scoreValue = 0;
    private int liveValue = 3;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        lives.text = "Lives: " + liveValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hozMovement = Input.GetAxis("Horizontal");
        verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }

    void Update()
    {
    
        if ((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.D)))
        {

            anim.SetInteger("State", 1);

        }
        
        if ((Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)))
        {

            anim.SetInteger("State", 0);

        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();

           if (scoreValue == 4)
            {
                transform.position = new Vector2(50f, 0.0f);

                liveValue = 3;
                lives.text = "Lives: " + liveValue.ToString();
            }

            if (scoreValue == 8)
            {
                winText.text = "You Win! \n Game Created by Mitchel Smith";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = false;
            }

            Destroy(collision.collider.gameObject);
        }

        else if (collision.collider.tag == "Enemy")
        {
            liveValue -= 1;
            lives.text = "Lives: " + liveValue.ToString();

            if (liveValue == 0)
            {
                winText.text = "You Lose! \n Reload to Try Again";
                Destroy(this.gameObject);
            }

            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping", false);
            }
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

}
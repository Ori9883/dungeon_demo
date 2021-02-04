using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public AudioSource audioSource,runAudioSource;
    public AudioClip bgm, bossbgm;
    private Animator animator;
    private Vector2 movement;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;


    [Header("Sprite")]
    public Sprite front;
    public Sprite back;

    [Header("SPrite State")]
    private bool frontside;
    private bool run;

    [Header("health")]
    public int health;
    public bool isDead;

    public void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        run = false;

        UIManager.instance.SetPlayerHealth(health);
    }

    public void Update()
    {
        animator.SetBool("dead", isDead);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y < 0)
        {
            frontside = false;
            GetComponent<SpriteRenderer>().sprite = front;
        }
        else if (movement.y > 0)
        {
            frontside = true;
            GetComponent<SpriteRenderer>().sprite = back;
        }

        if (movement.x != 0 || movement.y != 0)
        {
            run = true;
        }
        else
        {
            run = false;
        }

        animator.SetBool("running", run);
        animator.SetBool("front", frontside);

        // Flip sprite
        var flipSprite = spriteRenderer.flipX ? movement.x > 0.01f : movement.x < -0.01f;
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        UIManager.instance.UpdatePlayerHealth(health);

        if(run == true && runAudioSource.isPlaying == false)
        {
            runAudioSource.Play();
        }
        if(run == false)
        {
            runAudioSource.Stop();
        }

        //quit
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameEND();
        }
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * MoveSpeed * Time.fixedDeltaTime);
    }

    public Vector3 TransPosition()
    {
        return transform.position;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBull"))
        {
            harm();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            harm();
        }
    }
    public void harm()
    {
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("hit"))
        {
            AudioManager.instance.PlayerHit();
            health -= 1;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            animator.SetTrigger("hit");
        }
    }

    public void treat()
    {
        health = health + 1;
    }

    public void changeBGM()
    {
        audioSource.clip = bossbgm;
        audioSource.Play();
    }

    public void GameEND()
    {
        Application.Quit();
    }
}


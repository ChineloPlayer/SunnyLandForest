using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private Rigidbody2D     bodyPlayer;
    private Animator        pAnimations;
    private SpriteRenderer  blinkHero;
    //public GameObject       pDie;
    public AudioSource      jumpSound;
    public AudioSource      playerAuds;
    public AudioSource      deathEnemy;
    public AudioSource      carrotSound;
    public AudioClip        playerDeath;


    [Header("Controladores")]
    public Transform        groundCheck;
    public bool             isGrounded = false;
    public bool isDead { get; private set; }

    public bool             fRight = true;
    private bool            invincible;

    private float           speed = 10;
    public float            touchRun = 0.0f;

    public int              life = 3;
    public Color            hiton;
    public Color            noHit;

    //private ControlManager _controler;

    [Header("Saltos")]
    public bool             jump = false;
    public int              forceJump = 0;
    public float            maxJump = 2;
    public int              numberJump = 0;


    public ParticleSystem _poeira;

  
    void Start()
    {
        bodyPlayer = GetComponent<Rigidbody2D>();
        pAnimations = GetComponent<Animator>();
        blinkHero = GetComponent<SpriteRenderer>();
        
        isDead = false;
        //_controler = FindObjectOfType<ControlManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        touchRun = Input.GetAxisRaw("Horizontal");
        Movimentos();
        

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

    }


    void ExecMovement( float movementH )
    {
        
        bodyPlayer.velocity = new Vector2( movementH * speed, bodyPlayer.velocity.y);
        if (movementH < 0 && fRight || (movementH > 0 && !fRight))
        {
           Flip();
        }
      
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        ExecMovement(touchRun);
        if (jump)
        {
            JumpPlayer();
        }

    }


    void JumpPlayer()
    {
        //verifica se o personagem está no chão
        if (isGrounded)
        {
            numberJump = 0;
            CreateDust();
        }
        //verifica se o personagem está no chão e conta a quantidade de pulos que ele pode dar
        //se o "numberJump for menor que o "maxJump" e o player estiver no ar, o player poderá pular novamente, caso contrário não
        if (isGrounded || numberJump < maxJump)
        {
            bodyPlayer.velocity = Vector2.up * forceJump;
            jumpSound.Play();
            isGrounded = false;
            numberJump++;
            CreateDust();
        }
        jump = false;
    }

    


    void Movimentos()
    {
        pAnimations.SetBool("Walk", bodyPlayer.velocity.x != 0);
        if(!isGrounded && !isDead)
            pAnimations.SetBool("Jump", true);
        else
            pAnimations.SetBool("Jump", false);
    }

    void Flip()
    {
        CreateDust();
        fRight = !fRight;
        Vector3 theScale = transform.localScale;
        theScale *= -1;
        transform.localScale = new Vector3(theScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //condição para o programa reconhecer as tags
        switch (collision.gameObject.tag)
        {
            //condição para o programa reconhecer que qualquer gameObject que tenha a tag "Coletaveis" será destruida
            //caso entre em contato com o player.
            case "Coletaveis":
                collision.gameObject.GetComponent<Colectables>().AddScore();
                break;

                //destrói o inimigo quando pula em cima dele
            case "Enemy":
                collision.gameObject.GetComponentInParent<Enemys>().TakeDamage(bodyPlayer);
                break;

            case "Damage":
                Hurt();
                break;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Hurt();
                break;

            case "Plataforma":
                this.transform.parent = collision.transform;
                break;

            case "Player":
                Hurt();
                break;

        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Plataforma":
                this.transform.parent = null;
                break;
        }
    }


    public void Hurt()
    {
        if (!invincible && !isDead)
        {
            invincible = true;
            life--;
            ControlManager.Instance.lifeChanger(life);
            if (life < 1)
            {
                StartCoroutine(Death());
            }
            else
            {
                StartCoroutine(Dano());

            }
        }

    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Dano()
    {

        blinkHero.color = noHit;
        for(float i = 0; i<1; i += 0.1f)
        {
            blinkHero.enabled = false;
            yield return new WaitForSeconds(0.10f);
            blinkHero.enabled = true;
            yield return new WaitForSeconds(0.10f);
        }

        blinkHero.color = Color.white;
        invincible = false;
    }

    IEnumerator Death()
    {
        isDead = true;
        AudioClip audiosAssistentPlayer = playerDeath;
        playerAuds.clip = playerDeath;
        playerAuds.Play();
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        bodyPlayer.velocity = Vector2.zero;
        bodyPlayer.bodyType = RigidbodyType2D.Kinematic;
        blinkHero.sortingOrder = 100;
        pAnimations.SetBool("Death", isDead);

        yield return new WaitForSeconds(1f);
        bodyPlayer.bodyType = RigidbodyType2D.Dynamic;
        bodyPlayer.AddForce(new Vector2(5f, 15f), ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(2f);
        LoadGame();
    }


    void CreateDust()
    {
        _poeira.Play();
    }

}

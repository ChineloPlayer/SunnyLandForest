using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class MovePlayer : MonoBehaviour
{
    
    private Rigidbody2D     bodyPlayer;
    private Animator        pAnimations;
    private SpriteRenderer  blinkHero;
    public GameObject       pDie;

    [Header("Controladores")]
    public Transform        groundCheck;
    public bool             isGrounded = false;

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
        //_controler = FindObjectOfType<ControlManager>();

    }

    // Update is called once per frame
    void Update()
    {

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
            AudioManager.Instance.JumpSound();
            isGrounded = false;
            numberJump++;
            CreateDust();
        }
        jump = false;
    }

    


    void Movimentos()
    {
        pAnimations.SetBool("Walk", bodyPlayer.velocity.x != 0);
        pAnimations.SetBool("Jump", !isGrounded);
        
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
                
                AudioManager.Instance.CarrotSound();
                ControlManager.Instance.pontuacao(1);
                Destroy(collision.gameObject);
                break;

                //destrói o inimigo quando pula em cima dele
            case "Enemy":
                GameObject tempExplosao = Instantiate(ControlManager.Instance.hitPrefab, transform.position, transform.localRotation);
                Destroy(tempExplosao, 0.5f);
                Rigidbody2D fSalto = GetComponentInParent<Rigidbody2D>();
                //estabiliza a posição X normal e adiciona o valor 0 ao Y
                fSalto.velocity = new Vector2(fSalto.velocity.x, 0);
                //adiciona força ao eixo Y fazendo com que o personagem pule quando entrar em contato com 
                //o inimigo;
                fSalto.AddForce(new Vector2(0, 600));

                AudioManager.Instance.DeathEnemy();

                Destroy(collision.gameObject);
                
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


    void Hurt()
    {
        if (!invincible)
        {
            invincible = true;
            life--;
            StartCoroutine("Dano");
            ControlManager.Instance.lifeChanger(life);
            if (life < 1)
            {
                GameObject pDieOnly = Instantiate(pDie, transform.position, Quaternion.identity);
                Rigidbody2D rbDie = pDieOnly.GetComponent<Rigidbody2D>();
                rbDie.AddForce(new Vector2(150f, 500f));
                AudioManager.Instance.DeathPlayer();
                Invoke("LoadGame", 4f);
                gameObject.SetActive(false);

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


    void CreateDust()
    {
        _poeira.Play();
    }

}

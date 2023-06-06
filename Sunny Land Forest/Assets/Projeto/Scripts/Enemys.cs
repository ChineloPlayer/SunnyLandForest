using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    public   Transform          enemy;
    public   SpriteRenderer     spriteEnemy;
    public   Transform[]        position;
    public   float              velocity;
    public   bool               isRight;
    public   GameObject         hitPrefab;
    
    private  int                idTarget;
    private AudioSource         audioS;
    private bool                isDead = false;

    void Start()
    {
        //o spriteEnemy vai pegar as propriedades do objeto que está dentro da váriavel enemy
        //ou seja, o objeto que estiver vinculado ao Enemy no inspector.
        spriteEnemy = enemy.gameObject.GetComponent<SpriteRenderer>();
        enemy.position = position[0].position;
        idTarget = 1;
        audioS = GetComponent<AudioSource>();

    }

    void Update()
    {
        //se o objeto enemy estiver diferente de vazio, então vai ser executado tudo oque está dentro da condição if
        if(enemy != null && !isDead)
        {
            //vai fazer o inimigo se mover de um ponto até o outro com uma velocidade X
            enemy.position = Vector3.MoveTowards(enemy.position, position[idTarget].position, velocity * Time.deltaTime);
            //verifica se o personagem chegou na posição desejada
            if(enemy.position == position[idTarget].position)
            {
                //incrementa o valor do target para ele ir para a posição seguinte
                idTarget += 1;
                //se o personagem estiver na posição final predefinida pelo position.length, ele irá para a posição 0
                if(idTarget == position.Length)
                {
                    idTarget = 0;
                }
            }
            //verifica para qual posição ele está indo e se está olhando para a direita
            //altera o lado do personagem quando chega na posição de destino
            //ou seja, se o personagem chegar a posição A olhando para a esquerda
            //quando for retornar, ele virará para a direita e continuará seu caminho
            if(position[idTarget].position.x < enemy.position.x && isRight == true)
            {
                Flip();
            }
            //mesma coisa aqui, porém para a esquerda
            else if (position[idTarget].position.x > enemy.position.x && isRight == false)
            {
                Flip();
            }
        }
    }


    void Flip()
    {
        isRight = !isRight;
        spriteEnemy.flipX = !spriteEnemy.flipX;
    }

    public void TakeDamage(Rigidbody2D playerRB)
    {
        isDead = true;
        //estabiliza a posição X normal e adiciona o valor 0 ao Y
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        //adiciona força ao eixo Y fazendo com que o personagem pule quando entrar em contato com 
        //o inimigo;
        playerRB.AddForce(new Vector2(0, 600));

        StartCoroutine(EnemyDeath());
    }

    IEnumerator EnemyDeath()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        var boxColliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var item in boxColliders)
        {
            item.enabled = false;
        }
        Instantiate(hitPrefab, enemy.transform);
        audioS.Play();
        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALesma : MonoBehaviour
{
    public   Transform          enemy;
    public   SpriteRenderer     spriteEnemy;
    public   Transform[]        position;
    public   float              velocity;
    public   bool               isRight;

    
    private  int                idTarget;

    void Start()
    {
        //o spriteEnemy vai pegar as propriedades do objeto que est� dentro da v�riavel enemy
        //ou seja, o objeto que estiver vinculado ao Enemy no inspector.
        spriteEnemy = enemy.gameObject.GetComponent<SpriteRenderer>();
        enemy.position = position[0].position;
        idTarget = 1;

    }

    void Update()
    {
        //se o objeto enemy estiver diferente de vazio, ent�o vai ser executado tudo oque est� dentro da condi��o if
        if(enemy != null)
        {
            //vai fazer o inimigo se mover de um ponto at� o outro com uma velocidade X
            enemy.position = Vector3.MoveTowards(enemy.position, position[idTarget].position, velocity * Time.deltaTime);
            //verifica se o personagem chegou na posi��o desejada
            if(enemy.position == position[idTarget].position)
            {
                //incrementa o valor do target para ele ir para a posi��o seguinte
                idTarget += 1;
                //se o personagem estiver na posi��o final predefinida pelo position.length, ele ir� para a posi��o 0
                if(idTarget == position.Length)
                {
                    idTarget = 0;
                }
            }
            //verifica para qual posi��o ele est� indo e se est� olhando para a direita
            //altera o lado do personagem quando chega na posi��o de destino
            //ou seja, se o personagem chegar a posi��o A olhando para a esquerda
            //quando for retornar, ele virar� para a direita e continuar� seu caminho
            if(position[idTarget].position.x < enemy.position.x && isRight == true)
            {
                Flip();
            }
            //mesma coisa aqui, por�m para a esquerda
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

}

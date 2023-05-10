using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour
{

    //controlador da camera do player
    public float offsetX = 3;
    public float suavidadeCam = 0.1f;

    public float limitedUp = 10f;
    public float limitedDown = 0f;
    public float limitedRight = 0f;
    public float limitedLeft = 100f;


    private Player player;
    private float playerX;
    private float playerY;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null && !player.isDead)
        {
            playerX = Mathf.Clamp(player.transform.position.x + offsetX, limitedLeft, limitedRight);
            playerY = Mathf.Clamp(player.transform.position.y + offsetX, limitedDown, limitedUp);

            //retornar o valor entre 2 pontos
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerX, playerY, transform.position.z), suavidadeCam);
        }
    }
}

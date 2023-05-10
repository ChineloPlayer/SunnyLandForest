using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectables : MonoBehaviour
{
    AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void AddScore()
    {
        ControlManager.Instance.pontuacao(1);
        audioS.Play();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(this.gameObject, .5f);  
    }
}

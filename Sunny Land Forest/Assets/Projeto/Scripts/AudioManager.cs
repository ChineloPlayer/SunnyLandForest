using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
   
    public static AudioManager Instance;
    [SerializeField] AudioSource somDaCenoura;
    public AudioSource morteEnemy;
    //public AudioSource mortePlayer;

    //audios do player
    #region
    [SerializeField] AudioSource somDoPulo;
    public AudioSource mortePlayer;


    #endregion
    private void Awake()
    {
        if (Instance != this && Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }

    public void CarrotSound()
    {
        somDaCenoura.Play();
    }

    public void JumpSound()
    {
        somDoPulo.Play();
    }

    public void DeathEnemy()
    {
        morteEnemy.Play();
    }

    public void DeathPlayer()
    {
        mortePlayer.Play();
    }



}

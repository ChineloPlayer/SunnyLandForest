using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   
    public static AudioManager Instance;
    public AudioMixer soundControler, sfxSoundControl;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }

        else
        {
            Instance = this;
        }


    }

    public void SetVolume(float audioSetController)
    {
        soundControler.SetFloat("Sound", audioSetController);
    }

    public void SFXVolum(float audioSFXController)
    {
        sfxSoundControl.SetFloat("EffectSound", audioSFXController);
    }
}

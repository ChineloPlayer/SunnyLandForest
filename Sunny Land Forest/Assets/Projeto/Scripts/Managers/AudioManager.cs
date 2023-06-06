using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   
    public static AudioManager Instance;
    public static float volume = 0.1f;
    public static float sfxVolume = 0.1f;
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
            DontDestroyOnLoad(this.gameObject);
        }

    }

    public void SetVolume(float audioSetController)
    {
        volume = audioSetController;
        soundControler.SetFloat("Sound", volume);
    }

    public void SFXVolum(float audioSFXController)
    {
        sfxVolume = audioSFXController;
        sfxSoundControl.SetFloat("EffectSound", sfxVolume);
    }
}

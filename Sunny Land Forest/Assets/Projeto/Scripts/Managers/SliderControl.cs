using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{

    public Slider slider, sfxSlider;


    void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            slider.value = AudioManager.volume;
            sfxSlider.value = AudioManager.sfxVolume;
        }
    }

    public void ChangedSliderValue(float value)
    {
        AudioManager.Instance.SetVolume(value);
    }
    public void ChangedSliderValueSetFXMVolum(float value)
    {
        AudioManager.Instance.SFXVolum(value);
    }
}

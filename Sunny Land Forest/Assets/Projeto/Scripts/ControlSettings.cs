using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{


    public bool isFullScreen;


    public Toggle fullScreenToggle;

    private void OnEnable()
    {
        fullScreenToggle.onValueChanged.AddListener(delegate { FullScreen(); });
        //musicVol.onValueChanged.AddListener(delegate { onMusicSlider(); });
    }


    public void FullScreen()
    {
        Screen.fullScreen = fullScreenToggle.isOn;
    }

}

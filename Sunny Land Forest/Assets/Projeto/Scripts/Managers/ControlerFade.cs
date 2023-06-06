using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlerFade : MonoBehaviour
{

    public static ControlerFade _instanceFade;


    public Image _fadeImg;
    public Color _initialColor;
    public Color _finalColor;

    public float _durationFade;
    public bool _isFade;
    private float _timeFade;
    public GameObject desligarFade;

    void Awake()
    {
        _instanceFade = this;
    }

    IEnumerator StartFade()
    {
        _isFade = true;
        _timeFade = 0f;


        while (_timeFade <= _durationFade)
        {
            _fadeImg.color = Color.Lerp(_initialColor, _finalColor, _timeFade / _durationFade);
            _timeFade += Time.deltaTime;

            yield return null;
        }
        Destroy(desligarFade, 3f);
        _isFade = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

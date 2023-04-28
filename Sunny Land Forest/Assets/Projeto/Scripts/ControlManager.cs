using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance;

    private int score;
    public TMP_Text txtScore;
    public GameObject hitPrefab;

    public Sprite[] life;
    public Image countLife;


    private void Awake()
    {
        if (Instance != this && Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }

    public void pontuacao(int contScore)
    {
        score += contScore;
        txtScore.text = score.ToString();
    }

    public void lifeChanger( int changer )
    {
        countLife.sprite = life[changer];
    }




}

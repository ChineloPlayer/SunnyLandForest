using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatControl : MonoBehaviour
{


    //controlador das plataformas
    #region
    [SerializeField]
    public Transform plataforma, pontA, pontB;
    public float velocidadePlat;

    public Vector3 destino;
    public GameObject plataformaPlayer;

    #endregion

    void Start()
    {
        plataforma.position = pontA.position;
        destino = pontB.position;
    }

    void Update()
    {
        if (plataforma.position == pontA.position)
        {
            destino = pontB.position;
        }
        if (plataforma.position == pontB.position)
        {
            destino = pontA.position;
        }

        plataforma.position = Vector3.MoveTowards(plataforma.position, destino, velocidadePlat);

    }
}

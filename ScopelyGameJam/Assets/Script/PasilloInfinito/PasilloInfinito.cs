using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasilloInfinito : MonoBehaviour
{
    public GameObject TriggerPasilloInfinito;

    private Vector3 posicionInicial;
    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == TriggerPasilloInfinito)
        {
            this.gameObject.transform.position = posicionInicial;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estante : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D livroFlutuante)
    {
        Transform livroNaEstante = transform.GetChild(transform.childCount - 1);

        Livro livro = livroFlutuante.GetComponent<Livro>();
        livro.MudandoDeEstante(livroNaEstante.gameObject, livroFlutuante.gameObject);
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Objeto saiu da área: {other.gameObject.name}");
    }

}

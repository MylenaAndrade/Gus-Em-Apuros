using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chegada : MonoBehaviour
{
    //[SerializeField] private string nomeCena;
    [SerializeField] private EditorDeCodigo editor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fim"))
        {
            Debug.Log("Chegou no destino!");
            editor.chegouNoFim = true;
        }
        else if (other.CompareTag("Obstaculo"))
        {
            Debug.Log("Colidiu com obstáculo!");
            editor.colidiuComObstaculo = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvancarCena: MonoBehaviour
{
    [SerializeField] private string nomeProximaFase;
    public static void IrProximaFase(string nomeProximaFase)
    {
        Debug.Log(nomeProximaFase);
        SceneManager.LoadScene(nomeProximaFase);
    }

    public void MudarDeCena(string nomeProximaFase)
    {
         SceneManager.LoadScene(nomeProximaFase);
    }
        public void TesteEvento()
    {
        Debug.Log("Evento foi chamado!");
    }

}

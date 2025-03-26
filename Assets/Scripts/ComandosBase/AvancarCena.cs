using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvancarCena
{
    public static void IrProximaFase(string nomeProximaFase)
   {
       Debug.Log(nomeProximaFase);
        SceneManager.LoadScene(nomeProximaFase);
   }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrucoesDeMovimento : MonoBehaviour
{
   public void TipoDeMovimeto(string tipoDeMovimento)
   {
      
       switch (tipoDeMovimento)
       {
           case "direita":
               Debug.Log("andar");
               break;
           case "esquerda":
               Debug.Log("correr");
               break;
           case "cima":
               Debug.Log("pular");
               break;
           case "baixo":
               Debug.Log("agachar");
               break;
       }
  }

  public void VirarParaEsquerda()
  {
      
  }                            
}

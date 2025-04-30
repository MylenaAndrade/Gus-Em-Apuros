using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FalaDoDialogo
{
   [SerializeField, HideInInspector]
   private string _identificador;

   [SerializeField]
   private Ator _ator;

   [SerializeField, TextArea(3,10)]
   private string _texto;

    public Ator Ator{
          get{
                return this._ator;
          }
    }
    
    public string Texto{
          get{
                return this._texto;
          }
    }

    public void AtualizarIdentificador(){
      if((this._ator != null) && (this._texto != null)){
            this._identificador = "[" + this._ator.Nome + "]" + ": " + this._texto;
      }
    }
}

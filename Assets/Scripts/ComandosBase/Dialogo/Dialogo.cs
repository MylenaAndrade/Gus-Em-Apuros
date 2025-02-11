using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script que define um diálogo e é utilizado pelo ScriptObjects*/
[CreateAssetMenu(fileName = "Novo dialogo", menuName = "Sistema de diálogo/Novo dialogo")]
public class Dialogo : ScriptableObject
{
    [SerializeField]
    private FalaDialogo[] _falasDialogo;

    private int _indiceFalaAtual;

    private void OnValidate(){
        foreach(FalaDialogo _fala in this._falasDialogo){
            _fala.AtualizarIdentificador();
        }
    }

    public void Iniciar(){
        this._indiceFalaAtual = 0;
    }
    
    public FalaDialogo FalaAtual{
        get{
            if(this._indiceFalaAtual < this._falasDialogo.Length){
                return this._falasDialogo[this._indiceFalaAtual];
            }
            return null;
        }
    }

    public void Avancar(){
        if(TemProximaFala()){
            this._indiceFalaAtual++;
        }
    }

    public bool TemProximaFala(){
        if(this._indiceFalaAtual < this._falasDialogo.Length - 1){
            return true;
        }
        return false;
    }
}

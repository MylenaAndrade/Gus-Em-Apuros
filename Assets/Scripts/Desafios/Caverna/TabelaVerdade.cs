using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabelaVerdade : MonoBehaviour
{
    private static TabelaVerdade _instancia;

    private void Awake(){
        _instancia = this;
        Esconder();
    }

    public void Exibir(GameObject _gameObject){
        _gameObject.SetActive(true);
        
    }

    public static TabelaVerdade Instancia{
        get{
            return _instancia;
        }  
    }

    public void Esconder(){
        gameObject.SetActive(false);
    }
}

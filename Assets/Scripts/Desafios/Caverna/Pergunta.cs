using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pergunta : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pergunta;

    private static Pergunta _instancia;


    private void Awake(){
        _instancia = this;
        Esconder();
    }

    public void Exibir(GameObject _gameObject){
        _gameObject.SetActive(true);
        
    }

    public static Pergunta Instancia{
        get{
            return _instancia;
        }  
    }
    public void Esconder(){
        gameObject.SetActive(false);
    }

     public void Resetar()
    {
        // Limpa o texto da pergunta
        _pergunta.text = string.Empty;

        // Garante que o GameObject esteja oculto
        Esconder();
    }
}

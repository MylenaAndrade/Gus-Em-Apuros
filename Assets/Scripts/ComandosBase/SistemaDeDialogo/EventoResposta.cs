using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventoResposta
{
    [SerializeField] public string nome;
    [SerializeField] private UnityEvent respostaEscolhida;
    
   public UnityEvent RespostaEscolhida => respostaEscolhida;
}
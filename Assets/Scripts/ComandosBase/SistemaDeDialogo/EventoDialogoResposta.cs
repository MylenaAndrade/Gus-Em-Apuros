using UnityEngine;
using System;

public class EventoDialogoResposta : MonoBehaviour
{
    [SerializeField] private ObjetoDialogo objetoDialogo;
    [SerializeField] private EventoResposta[] eventos;

    public ObjetoDialogo ObjetoDialogo => objetoDialogo;
    public EventoResposta[] Eventos => eventos;

    [SerializeField] private ObjetoDialogo coisa;
    private bool bolinha;

  public void OnValidate()
  {
     if(objetoDialogo == null) return;
     if(objetoDialogo.Respostas == null) return;
     if(eventos != null && eventos.Length == objetoDialogo.Respostas.Length) return;

     do{
      int quantidadeObjetos = 0;
      if(coisa == null)
      {
        for(int i = 0; i < objetoDialogo.Respostas.Length; i++)
        {
           if(objetoDialogo.Respostas[i].ObjetoDialogo == null)
           {
              quantidadeObjetos++;
           }else{
              coisa = objetoDialogo.Respostas[i].ObjetoDialogo;
           }
        }
      }
      else
      {
        for(int i = 0; i < coisa.Respostas.Length; i++)
        {
           if(coisa.Respostas[i].ObjetoDialogo == null)
           {
              quantidadeObjetos++;
           }else{
              coisa = coisa.Respostas[i].ObjetoDialogo;
           }
        }
      }
      if(coisa == null)
      {
        bolinha = quantidadeObjetos == objetoDialogo.Respostas.Length;
      }else{
        bolinha = quantidadeObjetos == coisa.Respostas.Length;
      }
    }while(bolinha == false);

     if(eventos == null)
     {
      eventos = new EventoResposta[coisa.Respostas.Length];
     }
     else
     {
        Array.Resize(ref eventos, coisa.Respostas.Length);
     }

   

    for (int i = 0; i < coisa.Respostas.Length; i++)
    {
       Resposta resposta = coisa.Respostas[i];

       if(eventos[i] != null)
       {
         eventos[i].nome = resposta.TextoResposta;
         continue;
       }

       eventos[i] = new EventoResposta() {nome = resposta.TextoResposta};
    }

  }

}
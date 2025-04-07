using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class GerirResposta : MonoBehaviour
{
    [SerializeField] private RectTransform caixaDeResposta;
    [SerializeField] private RectTransform modeloBotaoResposta;
    [SerializeField] private RectTransform containerResposta;
    private DialogoUI dialogoUI;
    private EventoResposta[] eventosResposta;
    List<GameObject> tempBotaoResposta = new List<GameObject>();

    private void Start()
    {
        dialogoUI = GetComponent<DialogoUI>();
    }

    public void AdicionarEventosDeRespostas(EventoResposta[] eventosResposta)
    {
        this.eventosResposta = eventosResposta;
    }

    public void MostrarRespostas(Resposta[] respostas){
        float alturaCaixaDeResposta = 0;
        float larguraExtra = 0;
        modeloBotaoResposta.gameObject.SetActive(false);
        for(int i = 0; i < respostas.Length; i++){
            Resposta resposta = respostas[i];
            int respostaIndex = i;

            GameObject botaoResposta = Instantiate(modeloBotaoResposta.gameObject, containerResposta);
            botaoResposta.SetActive(true);
            botaoResposta.GetComponent<TMP_Text>().text = resposta.TextoResposta;
            larguraExtra = (resposta.TextoResposta.Length / 10) * 160;
            botaoResposta.GetComponent<Button>().onClick.AddListener(() => RespostaEscolhida(resposta, respostaIndex));

            tempBotaoResposta.Add(botaoResposta);

            alturaCaixaDeResposta += modeloBotaoResposta.sizeDelta.y;
        }
        

        caixaDeResposta.sizeDelta = new Vector2(caixaDeResposta.sizeDelta.x + larguraExtra, alturaCaixaDeResposta);
        caixaDeResposta.gameObject.SetActive(true);
    }

    private void RespostaEscolhida(Resposta resposta, int respostaIndex){
        caixaDeResposta.gameObject.SetActive(false);

        foreach(GameObject button in tempBotaoResposta){
            Destroy(button);
        }
        tempBotaoResposta.Clear();

        if(eventosResposta != null && respostaIndex <= eventosResposta.Length){
            eventosResposta[respostaIndex].RespostaEscolhida?.Invoke();
        }

        eventosResposta = null;

        if(resposta.ObjetoDialogo == null) 
        {
            dialogoUI.MostrarDialogo(resposta.ObjetoDialogo);
        }
        else
        {
            dialogoUI.FecharCaixaDeDialogo();
        }

    }
}

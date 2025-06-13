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
    private List<GameObject> tempBotaoResposta = new List<GameObject>();
    
    [SerializeField] private ObjetoResposta objResposta = new() { Objeto = null, Clicou = false };
    public ObjetoResposta ObjResposta => objResposta;

    public class ObjetoResposta
    {
        public ObjetoDialogo Objeto { get; set; }
        public bool Clicou { get; set; }
    }

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
        objResposta.Clicou = false;
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
        
        objResposta.Clicou = true;
        foreach(GameObject button in tempBotaoResposta){
            Destroy(button);
        }
        tempBotaoResposta.Clear();

        if(eventosResposta != null && respostaIndex <= eventosResposta.Length){
            eventosResposta[respostaIndex].RespostaEscolhida?.Invoke();
        }
        eventosResposta = null;

        if(resposta.ObjetoDialogo) 
        {
            objResposta.Objeto = resposta.ObjetoDialogo;
            dialogoUI.MostrarDialogo(resposta.ObjetoDialogo);
        }
        else
        {
            objResposta.Objeto = null;
            dialogoUI.FecharCaixaDeDialogo();
        }

    }
}

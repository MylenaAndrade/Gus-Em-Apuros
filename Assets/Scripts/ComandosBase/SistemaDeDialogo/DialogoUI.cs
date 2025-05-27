using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogoUI : MonoBehaviour
{
    [SerializeField] private GameObject caixaDeDialogo;
    [SerializeField] private TMP_Text areaDoTexto;
    [SerializeField] private Image fotoAtor;
    [SerializeField] private TextMeshProUGUI textoNomeAtor;
    [SerializeField] private TimelineController controladorTimeline;
    [SerializeField] private GameObject objeto;
    [SerializeField] private bool trocarDeCena = false; //preciso melhorar isso, gambiarra feita por falta de tempo
    [SerializeField] private string cenaParaTrocar;

    public bool estaAberto { get; private set; }
    private GerirResposta gerirResposta;
    public GerirResposta GerirResposta => gerirResposta;
    
    private EfeitoDeEscrita efeitoDeEscrita;
    private bool bloquearTeclado = false;
    bool timelineTerminou = false;


    private void Start()
    {
        efeitoDeEscrita = GetComponent<EfeitoDeEscrita>();
        gerirResposta = GetComponent<GerirResposta>();
        FecharCaixaDeDialogo();
    }

    public void MostrarDialogo(ObjetoDialogo objetoDialogo)
    {
        if (EntradaBloqueada.tecladoBloqueado) return; // Bloqueia se a timeline estiver tocando

        estaAberto = true;
        caixaDeDialogo.SetActive(true);
        StartCoroutine(EtapasDoDialogo(objetoDialogo));
    }


    public void AdicionarEventosDeRespostas(EventoResposta[] eventosResposta)
    {
        gerirResposta.AdicionarEventosDeRespostas(eventosResposta);
    }

    private IEnumerator EtapasDoDialogo(ObjetoDialogo objetoDialogo)
    {

        for (int i = 0; i < objetoDialogo.Dialogo.Length; i++)
        {
            FalaDialogo falaAtual = objetoDialogo.Dialogo[i];
            Ator ator = falaAtual.Ator;

            this.fotoAtor.sprite = falaAtual.Ator.Foto;
            this.textoNomeAtor.text = ator.Nome;
            string dialogo = objetoDialogo.Dialogo[i].Texto;
            yield return efeitoDeEscrita.Rodar(dialogo, areaDoTexto);

            yield return new WaitUntil(() => !bloquearTeclado && Input.GetKeyDown(KeyCode.E));

            // Se essa fala ativa uma timeline:
            if (objetoDialogo.IndicesQueAtivamTimeline != null &&
                System.Array.Exists(objetoDialogo.IndicesQueAtivamTimeline, index => index == i))
            {
                // Fecha a caixa imediatamente
                FecharCaixaDeDialogo();

                EntradaBloqueada.tecladoBloqueado = true;


                controladorTimeline.aoTerminarTimeline += () => timelineTerminou = true;
                controladorTimeline.IniciarTimeline();

                yield return new WaitUntil(() => timelineTerminou);
                EntradaBloqueada.tecladoBloqueado = false;

                // Reabrir a caixa e continuar de onde parou
                caixaDeDialogo.SetActive(true);
                estaAberto = true;
                objeto.SetActive(false);

                 if(trocarDeCena == true && timelineTerminou == true)
                {
                    AvancarCena.IrProximaFase(cenaParaTrocar);
                }
                
            }

            if (objetoDialogo.IndicesQueAtivamObjetos != null &&
                System.Array.Exists(objetoDialogo.IndicesQueAtivamObjetos, index => index == i))
            {
                objeto.SetActive(true);
            }
            
            // Se for a última fala com resposta, para antes
            if (i == objetoDialogo.Dialogo.Length - 1 && objetoDialogo.temResposta)
                break;
        }

        if (objetoDialogo.temResposta)
        {
            gerirResposta.MostrarRespostas(objetoDialogo.Respostas);
        }
        else
        {
            FecharCaixaDeDialogo();
        }
    }


    public void FecharCaixaDeDialogo()
    {
        estaAberto = false;
        caixaDeDialogo.SetActive(false);
        areaDoTexto.text = string.Empty;
        
    }

}

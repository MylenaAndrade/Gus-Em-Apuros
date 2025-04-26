using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogoUI : MonoBehaviour
{
    [SerializeField] private GameObject caixaDeDialogo;
    [SerializeField] private TMP_Text areaDoTexto;
    [SerializeField] private TimelineController controladorTimeline;
    [SerializeField] private GameObject gameObjectParaDesligar;

    public bool estaAberto { get; private set; }
    private GerirResposta gerirResposta;
    public GerirResposta GerirResposta => gerirResposta;
    
    private EfeitoDeEscrita efeitoDeEscrita;
    private bool bloquearTeclado = false;

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
            string dialogo = objetoDialogo.Dialogo[i];
            yield return efeitoDeEscrita.Rodar(dialogo, areaDoTexto);

            yield return new WaitUntil(() => !bloquearTeclado && Input.GetKeyDown(KeyCode.E));

            // Se essa fala ativa uma timeline:
            if (objetoDialogo.IndicesQueAtivamTimeline != null &&
                System.Array.Exists(objetoDialogo.IndicesQueAtivamTimeline, index => index == i))
            {
                // Fecha a caixa imediatamente
                FecharCaixaDeDialogo();

                EntradaBloqueada.tecladoBloqueado = true;
                bool timelineTerminou = false;

                controladorTimeline.aoTerminarTimeline += () => timelineTerminou = true;
                controladorTimeline.IniciarTimeline();

                yield return new WaitUntil(() => timelineTerminou);
                EntradaBloqueada.tecladoBloqueado = false;

                // Reabrir a caixa e continuar de onde parou
                caixaDeDialogo.SetActive(true);
                estaAberto = true;
                gameObjectParaDesligar.SetActive(false);
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

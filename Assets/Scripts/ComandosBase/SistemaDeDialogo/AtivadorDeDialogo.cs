using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class AtivadorDeDialogo : MonoBehaviour, IInteracaoDialogo
{
    [SerializeField] private ObjetoDialogo objetoDialogo;
    [SerializeField] private GameObject aperteE;
    private GameObject objetoArmazenado;

    private PersonagemController playerConfig;

    private bool podeVerificarResposta;


    private void Start()
    {
        podeVerificarResposta = true;
    }

    private void Update()
    {

        if (playerConfig != null)
        {
            bool clicou = playerConfig.DialogoUI.GerirResposta.ObjResposta.Clicou;
            if (!clicou) podeVerificarResposta = true;
            if (clicou)
            {
                if (podeVerificarResposta)
                {
                    ObjetoDialogo objeto = playerConfig.DialogoUI.GerirResposta.ObjResposta.Objeto;
                    if(objeto != null){
                        EventoObjetoDialogo(objeto);
                    } 
                    podeVerificarResposta = false;
                }
            }
        }


    }
    public void AtualizarObjetoDialogo(ObjetoDialogo objetoDialogo)
    {
        this.objetoDialogo = objetoDialogo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PersonagemController player))
        {
            if(aperteE != null){
                objetoArmazenado = Instantiate(aperteE, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }
            
            player.InteracaoD = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PersonagemController player))
        {
            if(objetoArmazenado != null){
                Destroy(objetoArmazenado);
            }
            
            if (player.InteracaoD is AtivadorDeDialogo dialogueActivator && dialogueActivator == this)
            {
                player.InteracaoD = null;
            }
        }
    }

    public void Interagir(PersonagemController player)
    {
        playerConfig = player;
        EventoObjetoDialogo(objetoDialogo);
        
        player.DialogoUI.MostrarDialogo(objetoDialogo);
       
    }

    public void EventoObjetoDialogo(ObjetoDialogo objetoDialogoEvento)
    {
        foreach(EventoDialogoResposta eventosResposta in GetComponents<EventoDialogoResposta>())
        {
            if (eventosResposta.ObjetoDialogo == objetoDialogoEvento)
            {
                playerConfig.DialogoUI.AdicionarEventosDeRespostas(eventosResposta.Eventos);
                break;
            }

        }
    }
}
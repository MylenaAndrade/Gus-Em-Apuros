using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class AtivadorDeDialogo : MonoBehaviour, IInteracaoDialogo
{
    [SerializeField] private ObjetoDialogo objetoDialogo;
    [SerializeField] private GameObject aperteE;
    [SerializeField] private ObjetoDialogo objetoEvento;
    private GameObject objetoArmazenado;
    public int numero;

    private void Start()
    {
        
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
        foreach(EventoDialogoResposta eventosResposta in GetComponents<EventoDialogoResposta>())
        {
            if (eventosResposta.ObjetoDialogo == objetoDialogo)
            {
                player.DialogoUI.AdicionarEventosDeRespostas(eventosResposta.Eventos);
                break;
            }

        }
      UnityEngine.Debug.Log("aconteceu");
        player.DialogoUI.MostrarDialogo(objetoDialogo);
       
    }
}
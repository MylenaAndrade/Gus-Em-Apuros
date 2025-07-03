using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class AtivadorDeObjetos : MonoBehaviour, IInteracaoObjeto
{
    [SerializeField] private GameObject aperteE;
    public enum TipoInteracao { PerguntasCaverna, Porta, irParaFloresta }
    [SerializeField] private TipoInteracao tipoInteracao;
    [SerializeField] private GameObject objeto;
    [SerializeField] private string nomeProximaFase;
    [SerializeField] private TimelineController controladorTimeline;

    private GameObject objetoArmazenado;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && other.TryGetComponent(out PersonagemController player))
        {
            if (aperteE != null)
            {
                objetoArmazenado = Instantiate(aperteE, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }

            player.InteracaoObjeto = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PersonagemController player))
        {
            if (objetoArmazenado != null)
            {
                Destroy(objetoArmazenado);
            }

            if (player.InteracaoObjeto is AtivadorDeObjetos interacaoObject && interacaoObject == this)
            {
                player.InteracaoObjeto = null;
            }
            Debug.Log(player.InteracaoObjeto == null ? "Interação removida com sucesso." : "Erro ao remover interação.");
        }
    }

    public void Interagir(PersonagemController player)
    {
        switch (tipoInteracao)
        {
            case TipoInteracao.PerguntasCaverna:
                TabelaVerdade.Exibir(objeto);
                break;
            case TipoInteracao.Porta:
                AvancarCena.IrProximaFase(nomeProximaFase);
                break;
            case TipoInteracao.irParaFloresta:
                controladorTimeline.IniciarTimeline();
                break;
        }


    }
    
}

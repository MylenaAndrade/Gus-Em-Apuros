using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class AtivadorDeObjetos : MonoBehaviour, IInteracaoObjeto
{
    [SerializeField] private GameObject aperteE;
    [SerializeField] private string nomeProximaFase;

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

            if (player.InteracaoD is AtivadorDeObjetos dialogueObject && dialogueObject == this)
            {
                player.InteracaoObjeto = null;
            }
        }
    }
    
    public void Interagir(PersonagemController player)
    {
       AvancarCena.IrProximaFase(nomeProximaFase);
       
    }
}

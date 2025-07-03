using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Limbo : MonoBehaviour
{
    [SerializeField] private GameObject canvasDeInteracao;
    [SerializeField] private Light2D luz;
    [SerializeField] private GameObject objeto; // Referência ao objeto que será ativado

    void Start()
    {
        if (luz != null)
        {
            luz.enabled = false;
        }
    }

    void Update()
    {
        if (!canvasDeInteracao.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clique detectado!");

            if (luz != null)
            {
                luz.enabled = true;
                objeto.SetActive(false);
                Debug.Log("Luz ativada!");
            }
            else
            {
                Debug.LogWarning("Referência da luz não está atribuída!");
            }
        }
    }
}

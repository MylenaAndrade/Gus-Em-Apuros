using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerificarEstantes : MonoBehaviour
{
    [SerializeField]
    private string nomeProximaFase;

    private GameObject[] estantes;

    private void Start()
    {
        estantes = GameObject.FindGameObjectsWithTag("Estante");
        StartCoroutine(VerificarEstantesPeriodicamente());
    }
    IEnumerator VerificarEstantesPeriodicamente()
    {
        while (true)
        {
            if (VerificarEstante())
            {
                Debug.Log("Estantes corretas");
                IrProximaFase();
                yield break; // Para a Coroutine se a condição for verdadeira
            }

            yield return new WaitForSeconds(1f); // Define o intervalo de verificação (1 segundo)
        }
    }

    private bool VerificarEstante()
    { 
        foreach (GameObject estante in estantes)
        {
            if(!VerificarLivros(estante))  
            {
                return false;
            }
        }
        
        return true;
    }

    private bool VerificarLivros(GameObject estante)
    {
        if(estante.transform.childCount == 4)
        {
           string firstChild = estante.transform.GetChild(0).gameObject.tag;
            for (int i = 1; i < estante.transform.childCount; i++)
            {
                if(!estante.transform.GetChild(i).gameObject.tag.Equals(firstChild))
                {
                    Debug.Log(firstChild);
                    return false;
                }
            }
            return true;

        }else if(estante.transform.childCount > 0)
        {
            return false;
        }
        return true;
        
    }
    private void IrProximaFase()
    {
        SceneManager.LoadScene(this.nomeProximaFase);
    }
}

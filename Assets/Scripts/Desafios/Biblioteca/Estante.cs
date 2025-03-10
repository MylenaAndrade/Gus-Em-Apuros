using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessário para LayoutRebuilder

public class Estante : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D livroFlutuante)
    {
        Livro livro = livroFlutuante.GetComponent<Livro>();
        Debug.Log($"Objeto entrou na área: {livroFlutuante.gameObject.name}");
        // Verifica se a prateleira tem pelo menos um livro
        if (transform.childCount > 0)
        {
            Transform livroNaEstante = transform.GetChild(transform.childCount - 1); // Último livro da estante
            

            if (livro != null)
            {
                livro.MudandoDeEstante(livroNaEstante.gameObject, livroFlutuante.gameObject);
                AjustarLayout();
            }
        }else
        {
            // Se não há livros na estante, adiciona diretamente
            Debug.Log("Adicionando livro diretamente");
            livro.transform.SetParent(transform);
            Debug.Log($"Pai do Livro: {livro.transform.parent.name}");
            
            AjustarLayout();
        }
    }

    private void OnTriggerExit2D(Collider2D livroFlutuante)
    {
        livroFlutuante.transform.SetParent(transform.root);
    }

    public void AjustarLayout()
    {
        // Garante que o HorizontalLayoutGroup seja atualizado
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}

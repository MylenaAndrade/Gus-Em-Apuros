using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Livro : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;  // Guarda a posição inicial do livro
    private Transform originalShelf;   // Guarda a prateleira original

    private Transform referencia; // Referência para a posição da prateleira
    
    private Transform estantePai; // Referência à prateleira

    private static Livro _instancia;


    private void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false; // Desabilita o collider
        SetEstantePai(transform.parent);     // Salva a prateleira original
        originalPosition = transform.position; // Salva a posição inicial do livro
        PodeSerArrastado(); // Se não for o último, bloqueia o arrasto
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Collider2D>().enabled = true; // Habilita o collider
        transform.SetParent(transform.root);  // Remove da prateleira temporariamente
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = eventData.position; // Segue o cursor enquanto arrasta
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Collider2D>().enabled = false; // Desabilita o collider
        
        if(transform.parent == transform.root) // Se não foi solto em uma prateleira
        {
            ReturnToOriginalPosition(); // Retorna à posição original
            return;
        } 
        
    }

    private void ReturnToOriginalPosition()
    {
        transform.SetParent(GetEstantePai());
        transform.position = originalPosition; // Retorna à posição original
    }

    private void PodeSerArrastado()
    {
        if (transform.GetSiblingIndex() == transform.parent.childCount - 1)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true; // Se for o último, permite arrastar
        }
        else
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false; // Se não for o último, bloqueia o arrasto
        }
    }

    public Transform GetEstantePai()
    {
        return estantePai;
    }

    public void SetEstantePai(Transform estante)
    {
        estantePai = estante;
    }

    public void MudandoDeEstante(GameObject gameObjectLivroEstante, GameObject gameObjectFlutuante)
    {
        Debug.Log($"O 1 livro é {gameObjectFlutuante.name}");
        Debug.Log($"O livro é {gameObjectLivroEstante.name}");
        if(gameObjectFlutuante.name == gameObjectLivroEstante.name)
        {
            gameObjectFlutuante.transform.SetParent(gameObjectLivroEstante.transform.parent);
          //  Vector3 novaPosicao = gameObjectLivroEstante.transform.TransformPoint(new Vector3(2, 0, 0));
          //  gameObjectFlutuante.transform.position = novaPosicao;
            Debug.Log($"Filho: {gameObjectFlutuante.transform.parent.name}");

            PodeSerArrastado();
            Debug.Log("Os livros são iguais");
            
        }
        else
        {
            Debug.Log("Os livros são diferentes");
        }
    }
    
    
    

    
    
}

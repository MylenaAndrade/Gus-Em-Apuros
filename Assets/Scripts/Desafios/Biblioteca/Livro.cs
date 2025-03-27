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

    private void Update()
    {
        PodeSerArrastado();
    }

    private void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false; // Desabilita o collider
        SetEstantePai(transform.parent);     // Salva a prateleira original
        PodeSerArrastado(); // Se não for o último, bloqueia o arrasto
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Collider2D>().enabled = true; // Habilita o collider
       // transform.SetParent(transform.root);  // Remove da prateleira temporariamente
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

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(GetEstantePai());
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

        Debug.Log($"Livro: {gameObjectFlutuante.name} - Estante: {gameObjectLivroEstante.transform.parent.name}");
        if(gameObjectFlutuante.name == gameObjectLivroEstante.name)
        {
            gameObjectFlutuante.transform.SetParent(gameObjectLivroEstante.transform.parent);
            SetEstantePai(gameObjectLivroEstante.transform.parent);

        }
        else if(gameObjectLivroEstante.tag == "Estante" && gameObjectFlutuante.transform.parent.name == "Canvas"){
            gameObjectFlutuante.transform.SetParent(gameObjectLivroEstante.transform);
            SetEstantePai(gameObjectLivroEstante.transform);
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }
    
    
    

    
    
}

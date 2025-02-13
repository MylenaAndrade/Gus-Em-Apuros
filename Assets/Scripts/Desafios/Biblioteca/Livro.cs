using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Livro : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;  // Guarda a posição inicial do livro
    private Transform originalShelf;   // Guarda a prateleira original
    
    private Estante estante; // Referência à prateleira

    private void Start()
    {
        PodeSerArrastado(); // Se não for o último, bloqueia o arrasto
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
        originalPosition = transform.position; // Salva a posição inicial do livro
        originalShelf = transform.parent;     // Salva a prateleira original
        transform.SetParent(transform.root);  // Remove da prateleira temporariamente
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Segue o cursor enquanto arrasta
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnToOriginalPosition();  // Se não soltou em uma prateleira, volta ao local original
    }

    private void ReturnToOriginalPosition()
    {
        transform.SetParent(originalShelf);
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
    
}

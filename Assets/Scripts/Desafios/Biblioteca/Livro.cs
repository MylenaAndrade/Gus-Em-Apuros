using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Livro : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;  // Guarda a posição inicial do livro
    private Transform originalShelf;   // Guarda a prateleira original
    public string bookTag;             // Tag do livro para validação

    public int maxBooksPerShelf = 5;   // Limite de livros por prateleira

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
        GameObject target = eventData.pointerCurrentRaycast.gameObject; // Objeto onde o livro foi solto

        if (target != null && target.CompareTag("Shelf"))  // Verifica se o alvo é uma prateleira válida
        {
            Transform shelf = target.transform;

            // Verifica se a prateleira tem espaço e se a tag corresponde
            if (CanPlaceOnShelf(shelf))
            {
                transform.SetParent(shelf);  // Define a nova prateleira como pai
                AdjustBookPosition(shelf);   // Ajusta a posição visual na prateleira
            }
            else
            {
                ReturnToOriginalPosition();  // Retorna à prateleira original
            }
        }
        else
        {
            ReturnToOriginalPosition();  // Se não soltou em uma prateleira, volta ao local original
        }
    }

    private bool CanPlaceOnShelf(Transform shelf)
    {
        int bookCount = shelf.childCount; // Conta quantos livros já estão na prateleira
        
        if (bookCount >= maxBooksPerShelf) // Verifica se há espaço na prateleira
        {
            return false;
        }

        // Obtém o último livro na prateleira
        Transform lastBook = shelf.childCount > 0 ? shelf.GetChild(shelf.childCount - 1) : null;
        if (lastBook != null)
        {
            Livro lastBookScript = lastBook.GetComponent<Livro>();
            if (lastBookScript != null && lastBookScript.bookTag != this.bookTag)
            {
                return false; // Só permite adicionar se a Tag for igual
            }
        }

        return true;
    }

    private void AdjustBookPosition(Transform shelf)
    {
        transform.position = shelf.position + new Vector3(0, -shelf.childCount * 0.3f, 0);
    }

    private void ReturnToOriginalPosition()
    {
        transform.SetParent(originalShelf);
        transform.position = originalPosition; // Retorna à posição original
    }
}

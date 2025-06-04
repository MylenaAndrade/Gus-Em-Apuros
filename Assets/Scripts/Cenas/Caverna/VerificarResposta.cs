using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerificarResposta : MonoBehaviour
{

    [SerializeField]
    private Sprite _estatuaAcesa;

    [SerializeField]
    private Sprite[] _estatuasApagadas;

    [SerializeField]
    private GameObject _estatuaGameObject;
    
    [SerializeField]
    private string nomeProximaFase;

    private Collider2D _collider;

    
    public void MudarCor()
    {
        _estatuaGameObject.GetComponent<SpriteRenderer>().sprite = _estatuaAcesa;
        gameObject.SetActive(false);

        _collider = ObterCollider(_estatuaGameObject);
        _collider.enabled = false;
        Debug.Log(_collider.enabled);

        if (VerificarEstatuas())
        {
            IrProximaFase();
        }

    }

    private bool VerificarEstatuas()
    {
        GameObject[] objetosComTag = GameObject.FindGameObjectsWithTag("Estatua");

        foreach (GameObject objeto in objetosComTag)
        {
            _collider = ObterCollider(objeto);

            if (_collider.enabled)
            {
                return false;
            }
        }

        return true;
    }
    private Collider2D ObterCollider(GameObject _gameObject)
    {
        return _gameObject.GetComponent<CapsuleCollider2D>();
    }

    private GameObject ObterGameObject(string nome)
    {
        return GameObject.Find(nome);
    }

   public void ResetarPergunta()
   {
        GameObject[] objetosComTag = GameObject.FindGameObjectsWithTag("Estatua");
        int cont = 0;

        foreach (GameObject objeto in objetosComTag)
        {
            Debug.Log(objeto.name + cont);
            objeto.GetComponent<SpriteRenderer>().sprite = _estatuasApagadas[cont];
            _collider = ObterCollider(objeto);
            _collider.enabled = true;
            cont++;
        }

        gameObject.SetActive(false);
   }

   private void IrProximaFase()
   {
        SceneManager.LoadScene(this.nomeProximaFase);
   }
}

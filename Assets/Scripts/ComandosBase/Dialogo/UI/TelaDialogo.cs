using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TelaDialogo : MonoBehaviour
{
    [SerializeField]
    private Image _fotoAtor;

    [SerializeField]
    private TextMeshProUGUI _textoNomeAtor;

    [SerializeField]
    private TextMeshProUGUI _textoFalaDialogo;

    [SerializeField]
    private float _intervaloTempoEntreLetrasEmSegundos;

    private bool _estaDigitando;
    private Coroutine _preencherTextoCoroutine;
    private Dialogo _dialogo;
    private static TelaDialogo _instancia;

    private void Awake(){
        _instancia = this;
        Esconder();
    }

    public static TelaDialogo Instancia{
        get{
            return _instancia;
        }  
    }
    
    public void Exibir(Dialogo _dialogo){
        this._dialogo = _dialogo;
        this._dialogo.Iniciar();

        this.gameObject.SetActive(true);
        ExibirFalaAtual();
    }

    public void Avancar(){
         if (_estaDigitando)
        {
            // Se ainda estiver digitando, parar e mostrar todo o texto imediatamente
            StopCoroutine(_preencherTextoCoroutine);
            _textoFalaDialogo.text = _dialogo.FalaAtual.Texto;
            _estaDigitando = false;
        }
        else if(this._dialogo.TemProximaFala())
        {
            this._dialogo.Avancar();
            ExibirFalaAtual();
        }
        else{
            Esconder();
        }
        
    }

    private void Esconder(){
        this.gameObject.SetActive(false);
    }

    private void ExibirFalaAtual(){
        FalaDialogo _falaAtual = this._dialogo.FalaAtual;
        Ator _ator = _falaAtual.Ator;

        this._fotoAtor.sprite = _falaAtual.Ator.Foto;
        this._textoNomeAtor.text = _ator.Nome;
        
        if(this._preencherTextoCoroutine != null)
        {
            StopCoroutine(this._preencherTextoCoroutine);
        }
        
        this._preencherTextoCoroutine = StartCoroutine(PreencherConteudoTexoAosPoucos(_falaAtual.Texto));
        
    }

    private IEnumerator PreencherConteudoTexoAosPoucos(string _texto)
    {
        this._textoFalaDialogo.text = "";
        _estaDigitando = true;

        for (int i = 0; i < _texto.Length; i++)
        {
            this._textoFalaDialogo.text += _texto[i];
            yield return new WaitForSeconds(this._intervaloTempoEntreLetrasEmSegundos);
        }   

        _estaDigitando = false;
    }

    public bool Visivel
    {
        get{
            return this.gameObject.activeSelf;
        }
    }
}

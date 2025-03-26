using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interacao : MonoBehaviour
{
    public enum TipoInteracao
    {
        Dialogo,
        Desafio,
        TabelaVerdade,
        Porta
    }

    [SerializeField]
    private TipoInteracao tipoInteracao;

    [SerializeField]
    private Dialogo _dialogo;

    [SerializeField]
    private GameObject _gameObject;
    
    [SerializeField]
    private string _nomeProximaFase;


    private void OnMouseDown()
    {
        Debug.Log("Clicou");
        switch (tipoInteracao)
        {
            case TipoInteracao.Dialogo:
                ExibirDialogo();
                break;
            case TipoInteracao.Desafio:
                IniciarDesafio();
                break;
            case TipoInteracao.TabelaVerdade:
                ExibirTabelaVerdade();
                break;
            case TipoInteracao.Porta:
                ProximaFase();
                break;
            
        }
    }

    private void ExibirDialogo()
    {
        if (_dialogo != null)
        {
            TelaDialogo.Instancia.Exibir(this._dialogo);
        }
    }

    private void IniciarDesafio()
    {
        if(_gameObject != null)
        {
            Debug.Log(_gameObject.name);
            
            Pergunta.Instancia.Exibir(this._gameObject);
        }
    }

    private void ExibirTabelaVerdade()
    {
        if(_gameObject != null)
        {
            TabelaVerdade.Instancia.Exibir(this._gameObject);
        }
    }

    private void ProximaFase()
    {
        if (_nomeProximaFase != null)
        {
            AvancarCena.IrProximaFase(this._nomeProximaFase);
        }
    }
}


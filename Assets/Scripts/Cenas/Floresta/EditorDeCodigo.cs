using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EditorDeCodigo : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string nomeCena;
    [SerializeField] private TMP_Text editorText;
    [SerializeField] private Transform personagem; // Referência ao personagem
    [SerializeField] private float distanciaMovimento = 1f;
    [SerializeField] private float intervaloMovimento = 0.5f;
    [SerializeField] private GameObject aviso;
    public bool chegouNoFim = false;
    public bool colidiuComObstaculo = false;

    private List<string> comandos = new List<string>();
    private bool executando = false;

    [SerializeField] private DialogoUI dialogoUI;
    public DialogoUI DialogoUI => dialogoUI;
    private Rigidbody2D personagemRb;

    private Vector3 posicaoOriginal;


    public void ComandoDireita() => AdicionarComando("MoverParaDireita");
    public void ComandoEsquerda() => AdicionarComando("MoverParaEsquerda");
    public void ComandoCima() => AdicionarComando("MoverParaCima");
    public void ComandoBaixo() => AdicionarComando("MoverParaBaixo");

    private void Start()
    {
        posicaoOriginal = personagem.position;
        personagemRb = personagem.GetComponent<Rigidbody2D>();

    }

    private void AdicionarComando(string nomeFuncao)
    {
        if (dialogoUI.estaAberto) return;
        if (executando) return;

        comandos.Add(nomeFuncao);
        AtualizarEditor();
    }

    private void AtualizarEditor()
    {
        editorText.text = "";
        for (int i = 0; i < comandos.Count; i++)
        {
            editorText.text += $"{i + 1}. {comandos[i]}();\n";
        }
    }

    public void LimparEditor()
    {
        if (executando) return;

        comandos.Clear();
        AtualizarEditor();
    }

    public void ExecutarComandos()
    {
        if (!executando)
            StartCoroutine(ExecutarSequencia());
    }

    private IEnumerator ExecutarSequencia()
    {
        executando = true;

        // Ativar modo automático
        PersonagemController controller = personagem.GetComponent<PersonagemController>();
        if (controller != null) controller.movimentoAutomatico = true;

        // 1. Salvar posição original
        //Vector3 posicaoOriginal = personagem.position;

        chegouNoFim = false;
        colidiuComObstaculo = false;

        foreach (var comando in comandos)
        {
            Vector3 direcao = Vector3.zero;

            switch (comando)
            {
                case "MoverParaDireita": direcao = Vector3.right; break;
                case "MoverParaEsquerda": direcao = Vector3.left; break;
                case "MoverParaCima": direcao = Vector3.up; break;
                case "MoverParaBaixo": direcao = Vector3.down; break;
            }

            Vector3 destino = personagem.position + direcao * distanciaMovimento;

            yield return StartCoroutine(MoverPersonagem(destino));

            yield return null; // espera OnTriggerEnter2D ser chamado

            if (chegouNoFim)
            {
                // Cena será trocada, então não precisa voltar
                SceneManager.LoadScene(this.nomeCena);
                yield break;
            }
            else if (colidiuComObstaculo)
            {
                aviso.SetActive(true);
                Debug.Log("Execução interrompida por obstáculo.");
                break;
            }
            aviso.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        if (!chegouNoFim)
        {
            yield return StartCoroutine(MoverPersonagem(posicaoOriginal));
        }

        // Desativar modo automático
        if (controller != null) controller.movimentoAutomatico = false;

        // 3. Resetar flags e encerrar
        chegouNoFim = false;
        colidiuComObstaculo = false;
        executando = false;
    }

    private IEnumerator MoverPersonagem(Vector3 destino)
    {
        float duracao = intervaloMovimento;
        float tempo = 0f;
        Vector3 origem = personagem.position;
        Vector3 direcao = (destino - origem).normalized;

        // Enviar os mesmos parâmetros do controle por teclado
        animator.SetFloat("AxisX", direcao.x);
        animator.SetFloat("AxisY", direcao.y);
        animator.SetInteger("Movement", 1); // está se movendo

        while (tempo < duracao)
        {
            Vector3 novaPosicao = Vector3.Lerp(origem, destino, tempo / duracao);
            personagemRb.MovePosition(novaPosicao);
            tempo += Time.deltaTime;
            yield return null;
        }

        personagemRb.MovePosition(destino);
        animator.SetInteger("Movement", 0); // parou
    }

    public void RemoverUltimoComando()
    {
        if (comandos.Count > 0)
        {
            comandos.RemoveAt(comandos.Count - 1);
            AtualizarEditor();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemController : MonoBehaviour
{
    [SerializeField] private DialogoUI dialogoUI;
    public DialogoUI DialogoUI => dialogoUI;
    public IInteracaoDialogo InteracaoD { get; set; }
    private Rigidbody2D personagemRigidbody2D;
    public float        personagemVelocidade;
    private Vector2     personagemDirecao;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        personagemRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (dialogoUI.estaAberto) return;

         if (Input.GetKeyDown(KeyCode.E))
        {
            InteracaoD?.Interagir(this);
        }
    }

    void FixedUpdate()
    {
        if (EntradaBloqueada.tecladoBloqueado) return;
        if (dialogoUI.estaAberto) return;
          personagemDirecao = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(personagemDirecao.sqrMagnitude > 0.1)
        {

            animator.SetFloat("AxisX", personagemDirecao.x);
            animator.SetFloat("AxisY", personagemDirecao.y);
            
            animator.SetInteger("Movement", 1);
        }
        else
        {
            animator.SetInteger("Movement", 0);
        }
        
        personagemRigidbody2D.MovePosition(personagemRigidbody2D.position + personagemDirecao * personagemVelocidade * Time.fixedDeltaTime);

    }

    void Flip()
    {
        if(personagemDirecao.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if(personagemDirecao.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }  
    }
}

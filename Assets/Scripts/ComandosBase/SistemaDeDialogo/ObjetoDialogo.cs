using UnityEngine;

[CreateAssetMenu(menuName = "Dialogo/ObjetoDialogo")]
public class ObjetoDialogo : ScriptableObject
{
    [SerializeField] private FalaDialogo[] dialogo;
   // [SerializeField] [TextArea] private string[] dialogo;
    [SerializeField] private Resposta[] respostas;
    [SerializeField] private int[] indicesQueAtivamTimeline;

    public int[] IndicesQueAtivamTimeline => indicesQueAtivamTimeline;
    
  //  public string[] Dialogo => dialogo;

    public bool temResposta => Respostas != null && Respostas.Length > 0;

    public Resposta[] Respostas => respostas;

    public FalaDialogo[] Dialogo => dialogo;

    private int _indiceFalaAtual;

    private void OnValidate(){
        foreach(FalaDialogo _fala in this.dialogo){
            _fala.AtualizarIdentificador();
        }
    }

    public void Iniciar(){
        this._indiceFalaAtual = 0;
    }
    
    public FalaDialogo FalaAtual{
        get{
            if(this._indiceFalaAtual < this.dialogo.Length){
                return this.dialogo[this._indiceFalaAtual];
            }
            return null;
        }
    }

    public void Avancar(){
        if(TemProximaFala()){
            this._indiceFalaAtual++;
        }
    }

    public bool TemProximaFala(){
        if(this._indiceFalaAtual < this.dialogo.Length - 1){
            return true;
        }
        return false;
    }
}

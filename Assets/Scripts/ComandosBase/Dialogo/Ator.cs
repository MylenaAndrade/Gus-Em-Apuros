using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script que define um ator que pode ser utilizado em um diálogo e é utilizado pelo ScriptObjects*/
[CreateAssetMenu(fileName = "Novo ator", menuName = "Sistema de diálogo/Novo ator")]
public class Ator : ScriptableObject
{
   [SerializeField]
   private string _nome;
   [SerializeField]
   private Sprite _foto;

   public string Nome{
        get{
            return this._nome;
        }
   }
   public Sprite Foto{
        get{
            return this._foto;
        }
   }
}

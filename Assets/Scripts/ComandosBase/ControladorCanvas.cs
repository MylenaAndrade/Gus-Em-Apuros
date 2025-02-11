using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCanvas : MonoBehaviour
{
    private void Awake(){
        ProcurarTelas();
    }

    private void ProcurarTelas(){
        TelaDialogo[] _telasEncontradas = GetComponentsInChildren<TelaDialogo>(true);
        Debug.Log("Quantidade de telas encontradas: " + _telasEncontradas.Length);
    }
}

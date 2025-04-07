using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EventoDialogoResposta))]
public class EventoDialogoRespostaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EventoDialogoResposta eventosResposta = (EventoDialogoResposta)target;

        if(GUILayout.Button("Refresh"))
        {
            eventosResposta.OnValidate();
        }
    }
}

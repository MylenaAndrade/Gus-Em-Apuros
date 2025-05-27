using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineStarter : MonoBehaviour
{
    public TimelineController timelineController;
    public string nomeCenaDestino;

    void Start()
    {
        if (timelineController != null)
        {
            timelineController.aoTerminarTimeline += TrocarCena;
            timelineController.IniciarTimeline();
        }
    }


    void TrocarCena()
    {
        SceneManager.LoadScene(nomeCenaDestino);
    }

    void OnDestroy()
    {
        // Boa prática: desinscrever o evento
        if (timelineController != null)
            timelineController.aoTerminarTimeline -= TrocarCena;
    }
}

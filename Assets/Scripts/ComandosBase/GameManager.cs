using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int numeroLegal = 1;
    public ObjetoDialogo salvarObjetoDialogo;

    private void Awake()
    {
        // Garante que apenas uma instância do GameManager existe
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre as cenas
        }
        else
        {
            Destroy(gameObject); // Destroi duplicatas
        }
    }

    public void SalvarObjetoDialogo(ObjetoDialogo objetoDialogo)
    {
        salvarObjetoDialogo = objetoDialogo;
    }

}
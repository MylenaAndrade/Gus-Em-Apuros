using UnityEngine;

public class AtivarParticulaAoAparecer : MonoBehaviour
{
    public GameObject particula;

    public void Ativar()
    {
        particula.SetActive(true);
    }

    public void Desativar()
    {
        particula.SetActive(false);
    }
}

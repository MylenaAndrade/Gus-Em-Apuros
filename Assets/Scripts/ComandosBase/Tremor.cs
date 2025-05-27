using UnityEngine;
using Cinemachine;

public class Tremor : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    public float duracao = 0.5f;
    public float intensidade = 1f;

    private float tempoRestante;
    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        // Pega o componente de ruído da câmera
        noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

      //  noise.m_AmplitudeGain = 0;
      //  noise.m_FrequencyGain = 0;
    }

    void Update()
    {
        if (tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;
            if (tempoRestante <= 0)
            {
                // Desativa o tremor
                noise.m_AmplitudeGain = 0;
                noise.m_FrequencyGain = 0;
            }
        }
    }

    public void IniciarTremor(float intensidadePersonalizada, float duracaoPersonalizada)
    {
        noise.m_AmplitudeGain = intensidadePersonalizada;
        noise.m_FrequencyGain = 2f; // você pode ajustar isso também
        tempoRestante = duracaoPersonalizada;
    }

    // Atalho para usar os valores padrão do inspetor
    public void IniciarTremorPadrao()
    {
        IniciarTremor(intensidade, duracao);
    }
}
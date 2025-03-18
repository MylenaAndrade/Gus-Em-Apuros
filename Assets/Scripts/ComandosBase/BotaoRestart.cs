using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoRestart : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Biblioteca");
    }
}

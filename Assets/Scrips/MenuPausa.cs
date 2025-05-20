using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuPausaFondo;

    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Resume();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        Time.timeScale = 0.0f;
        menuPausa.SetActive(true);
        menuPausaFondo.SetActive(true);
        juegoPausado = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        menuPausa.SetActive(false);
        menuPausaFondo.SetActive(false);
        juegoPausado = false;
    }

    public void Reiniciar()
    {
        Time.timeScale=1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

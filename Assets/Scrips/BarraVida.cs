using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image fill;
    [SerializeField] private PlayerController playerController;
    private float vidaMaxima;
    // Start is called before the first frame update
    void Start()
    {
        vidaMaxima = playerController.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = playerController.currentHealth/vidaMaxima;
    }
}

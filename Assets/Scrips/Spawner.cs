using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float minX, minY, maxX, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private float tiempoEnemigos;
    private float tiempoSiguienteEnemigo;

    // Start is called before the first frame update
    void Start()
    {
        maxX = puntos.Max(puntos => puntos.position.x);
        maxY = puntos.Max(puntos=>puntos.position.y);
        minX = puntos.Min(puntos => puntos.position.x);
        minY = puntos.Min(puntos=>puntos.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        tiempoSiguienteEnemigo += Time.deltaTime;

        if (tiempoSiguienteEnemigo >= tiempoEnemigos) 
        {
            tiempoSiguienteEnemigo = 0;

            CrearEnemigo();
        }
    }

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);
        Vector2 posicionAleatoria = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));

        Instantiate(enemigos[numeroEnemigo],posicionAleatoria,Quaternion.identity);
    }
}

using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private float tiempoEnemigos = 2f;
    private float tiempoSiguienteEnemigo = 4f;
    private int enemigosCreados = 0;
    public int maxEnemigos = 5;

    private SpawnerManager spawnerManager;

    public void SetSpawnerManager(SpawnerManager manager)
    {
        spawnerManager = manager;
    }

    void Start()
    {
        StartCoroutine(CrearEnemigosConTimer());

    }

    IEnumerator CrearEnemigosConTimer()
    {
        while (enemigosCreados < maxEnemigos)
        {
            yield return new WaitForSeconds(tiempoSiguienteEnemigo);
            CrearEnemigo();
            enemigosCreados++;
        }

        if (spawnerManager != null)
        {
            spawnerManager.InstanciarSiguienteSpawner();
        }

        Destroy(gameObject);
    }

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);
        Instantiate(enemigos[numeroEnemigo], puntos[0].position, Quaternion.identity);
    }
}
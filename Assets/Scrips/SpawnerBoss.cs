using System.Collections;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{

    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject enemigos;

    private SpawnerManager spawnerManager;

    public void SetSpawnerManager(SpawnerManager manager)
    {
        spawnerManager = manager;
    }

    void Start()
    {
       

    }

    private void Update()
    {
        CrearEnemigo();
    }

    private void CrearEnemigo()
    {
        Instantiate(enemigos, puntos[0].position, Quaternion.identity);
        Destroy(gameObject);
    }
}
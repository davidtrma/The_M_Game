using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnerPrefabs; // 3 prefabs distintos
    private int spawnerActual = 0;

    void Start()
    {
        InstanciarSiguienteSpawner();
    }

    public void InstanciarSiguienteSpawner()
    {
        if (spawnerActual < spawnerPrefabs.Length)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            Instantiate(spawnerPrefabs[spawnerActual], randomPosition, Quaternion.identity)
                .GetComponent<Spawner>()
                .SetSpawnerManager(this); // Pasar referencia
            spawnerActual++;
        }
        else
        {
            Debug.Log("Todos los spawners han terminado.");
        }
    }
}
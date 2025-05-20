using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnerPrefabs; // 3 prefabs distintos
    [SerializeField] private GameObject spawnerBoss;
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
            Invoke(nameof(InstanciarBoss), 15f);
        }
    }

    public void InstanciarBoss()
    {
        Vector3 BossPosition = new Vector3(0,8,0);
        Instantiate(spawnerBoss, BossPosition, Quaternion.identity)
               .GetComponent<SpawnerBoss>();
        MusicManager.Instance.CambiarMusica(MusicManager.Instance.musicaBoss);
    }
}
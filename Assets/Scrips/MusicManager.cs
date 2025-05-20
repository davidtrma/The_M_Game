using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSource;
    public AudioClip musicaNormal;
    public AudioClip musicaBoss;
    public AudioClip musicaVictoria;
    public AudioClip musicaDerrota;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CambiarMusica(musicaNormal);
    }

    public void CambiarMusica(AudioClip nuevaMusica)
    {
        if (audioSource.clip != nuevaMusica)
        {
            audioSource.Stop();
            audioSource.clip = nuevaMusica;
            audioSource.Play();
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioClip BGM1; // Stage1¿« πË∞Ê¿Ωæ«
    public AudioClip BGM2; // Stage2¿« πË∞Ê¿Ωæ«
    public AudioClip BGM3; // BossStage¿« πË∞Ê¿Ωæ«

    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Stage1":
                audioSource.clip = BGM1;
                break;
            case "Stage2":
                audioSource.clip = BGM2;
                break;
            case "BossStage":
                audioSource.clip = BGM3;
                break;
        }
        audioSource.Play();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

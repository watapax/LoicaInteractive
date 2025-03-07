using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    public void ReproducirSfx(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

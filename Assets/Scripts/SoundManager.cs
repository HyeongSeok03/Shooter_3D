using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        source = GetComponent<AudioSource>();
    }

    public AudioClip[] audios;

    public void AudioStart(int value)
    {
        source.PlayOneShot(audios[value]);
    }
}

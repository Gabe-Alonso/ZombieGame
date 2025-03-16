using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip defaultMusic;
    public AudioClip bossMusic;

    private AudioSource _speaker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speaker = GetComponent<AudioSource>();
        defaultMusic = _speaker.clip;
    }

    public void PlayBossMusic()
    {
        if (_speaker.clip != bossMusic)
        {
            _speaker.clip = bossMusic;
            _speaker.Play();
        }
    }

    public void PlayDefaultMusic()
    {
        if (_speaker.clip != defaultMusic)
        {
            _speaker.clip = defaultMusic;
            _speaker.Play();
        }
    }
}

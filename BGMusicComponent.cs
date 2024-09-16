using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicComponent : MonoBehaviour
{
    public AudioClip[] MusicClips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource=GetComponent<AudioSource>();
        PlayNextClip();
        
    }

   void PlayNextClip()
    {
        if (MusicClips.Length == 0)
            return;

        // Назначаем текущий аудиоклип
        audioSource.clip=MusicClips[Random.Range(0,MusicClips.Length-1)];
        audioSource.Play();

        // Запланировать проигрывание следующего трека после завершения текущего
        Invoke("PlayNextClip", audioSource.clip.length);
    }
}

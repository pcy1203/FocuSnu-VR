using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("-------- Audio Sources --------")]
    [SerializeField] public AudioSource musicSource;

    [Header("-------- Audio Clips --------")]
    [SerializeField] public AudioClip[] musicClips;

    private void Start(){
        int randNum = Random.Range(0, musicClips.Length);
        musicSource.clip = musicClips[randNum];
        // musicSource.Play();
    }
    public void PlayMusic(){
        if (!musicSource.isPlaying){
            int randNum = Random.Range(0, musicClips.Length);
            musicSource.clip = musicClips[randNum];
            musicSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip soul;
    public AudioClip basketEmpty;
    public AudioClip harpBroken;
    public AudioClip playHarp;
    public AudioClip changeDiaper;
    public AudioClip[] babyCry;
    public AudioClip eating;
    public AudioClip backInTune;
    public AudioClip clic;
    public AudioClip end_sentence;
    public AudioClip end_level;
    public AudioClip success;
    public AudioClip game_over;
    public AudioClip round_1;
    public AudioClip next_round;
    public AudioClip final_round;
    public AudioClip fire;
    public AudioClip music0;
    public AudioClip music1;
    public AudioClip music2;

    [SerializeField] AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(AudioClip c) {
        music.clip = c;
        music.Play();
    }
}

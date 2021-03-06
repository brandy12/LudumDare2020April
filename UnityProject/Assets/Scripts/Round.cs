﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour {

    [SerializeField] List<Sprite> sprites_round1;
    [SerializeField] List<Sprite> sprites_last_round;
    [SerializeField] List<Sprite> sprites_next_round;
    [SerializeField] List<Sprite> sprites_game_over;
    [SerializeField] List<Sprite> sprites_success;

    Animation2D anim_round1;
    Animation2D anim_last_round;
    Animation2D anim_next_round;
    Animation2D anim_game_over;
    Animation2D anim_success;

    [SerializeField] Image img;
    AudioManager audio_manager;

    bool visible;
    Animation2D current_anim;

    // Start is called before the first frame update
    void Start()
    {
        visible = false;
        audio_manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        anim_round1 = new Animation2D("round1", sprites_round1, 2.0f, false);
        anim_next_round = new Animation2D("next round", sprites_next_round, 2.0f, false);
        anim_last_round = new Animation2D("last round", sprites_last_round, 2.0f, false);
        anim_game_over = new Animation2D("game over", sprites_game_over, 3.0f, false);
        anim_success = new Animation2D("success", sprites_success, 5.0f, false);
        current_anim = null;

    }

    // Update is called once per frame
    void Update()
    {
    
        
        if (visible) {

            if (current_anim.evolve()) {
                visible = false;
            }

            img.sprite = current_anim.currentSprite();
        }

        if (visible) {
            img.gameObject.SetActive(true);
        } else {
            img.gameObject.SetActive(false);
         }
    }

    public void PlayRound(int i) {
        if (i == 1) {
            current_anim = anim_round1;
            audio_manager.GetComponent<AudioSource>().PlayOneShot(audio_manager.round_1);
        } else if (i == 5) {
            current_anim = anim_last_round;
            audio_manager.GetComponent<AudioSource>().PlayOneShot(audio_manager.final_round);
        } else {
            current_anim = anim_next_round;
            audio_manager.GetComponent<AudioSource>().PlayOneShot(audio_manager.next_round);
        }
        visible = true;
        current_anim.Initialize();
    }

    public void PlayGameOver() {
        current_anim = anim_game_over;
        visible = true;
        current_anim.Initialize();
    }

    public void PlaySuccess() {
        current_anim = anim_success;
        visible = true;
        current_anim.Initialize();
    }
}

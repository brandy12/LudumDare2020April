using System.Collections;
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

    bool visible;
    Animation2D current_anim;

    // Start is called before the first frame update
    void Start()
    {
        visible = false;
        Debug.Log("Start");

        anim_round1 = new Animation2D("round1", sprites_round1, 2.0f, false);
        anim_next_round = new Animation2D("next round", sprites_next_round, 2.0f, false);
        anim_last_round = new Animation2D("last round", sprites_last_round, 2.0f, false);
        anim_game_over = new Animation2D("game over", sprites_game_over, 2.0f, false);
        anim_success = new Animation2D("success", sprites_success, 2.0f, false);
        current_anim = null;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Visible : " + visible);

        if (visible) {

            if (current_anim.evolve()) {
                visible = false;
                Debug.Log("FINISH");
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
        } else if (i == 5) {
            current_anim = anim_last_round;
        } else {
            current_anim = anim_next_round;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    MainGame mainGame;
    [SerializeField] Button btn;
    [SerializeField] Text txt;

    [SerializeField] List<Sprite> sprites_basket;
    [SerializeField] SpriteRenderer sprite_renderer;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start() {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        btn.onClick.AddListener(() => {
            mainGame.EmptyBasket();
            audioManager.GetComponent<AudioSource>().PlayOneShot(audioManager.basketEmpty);
        });
    }

    // Update is called once per frame
    void Update() {
        txt.text = mainGame.dirty_diapers + "/" + mainGame.capacity_basket;
        int index = 2;

        if (mainGame.dirty_diapers < mainGame.capacity_basket) {
            if (mainGame.dirty_diapers < (mainGame.capacity_basket - 1) / 2.0f) {
                index = 0;
            } else {
                index = 1;
            }
            
        }
        

        sprite_renderer.sprite = sprites_basket[index];

    }
}

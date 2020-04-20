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


    bool is_mouse_hover;
    [SerializeField] SpriteRenderer sprite_mouse_hover;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start() {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        btn.onClick.AddListener(() => {
            mainGame.EmptyBasket();
        });

        is_mouse_hover = false;
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

        MouseHoverManagement();
    }



    void MouseHoverManagement() {

        if (is_mouse_hover) {
            sprite_mouse_hover.gameObject.SetActive(true);
        } else {
            sprite_mouse_hover.gameObject.SetActive(false);
        }
    }

    public void MouseEnter()
    {
        is_mouse_hover = true;
    }

    public void MouseExit()
    {
        is_mouse_hover = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harpe : MonoBehaviour
{

    MainGame mainGame;

    [SerializeField] SpriteRenderer sprite_renderer;

    [SerializeField] Button btn;
    [SerializeField] Sprite sprite_normal;
    [SerializeField] Sprite sprite_broken;


    bool is_mouse_hover;
    [SerializeField] SpriteRenderer sprite_mouse_hover;

    // Start is called before the first frame update
    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

        is_mouse_hover = false;

        btn.onClick.AddListener(() => {
            mainGame.TuneHarp();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (mainGame.is_harp_broken) {
            sprite_renderer.sprite = sprite_broken;
        } else {
            sprite_renderer.sprite = sprite_normal;
        }

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

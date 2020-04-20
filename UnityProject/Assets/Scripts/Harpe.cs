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

    [SerializeField] GameObject tuto_repair;

    // Start is called before the first frame update
    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

        is_mouse_hover = false;

        btn.onClick.AddListener(() => {
            mainGame.TuneHarp();
        });

        tuto_repair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainGame.is_harp_broken) {
            sprite_renderer.sprite = sprite_broken;
            if (mainGame.Number_babies == 1)
            {
                tuto_repair.SetActive(true);
            }
        } else {
            sprite_renderer.sprite = sprite_normal;
            if (mainGame.Number_babies == 1)
            {
                tuto_repair.SetActive(false);
            }
        }

        MouseHoverManagement();
        if (!mainGame.Playing)
        {
            tuto_repair.SetActive(false);
        }
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

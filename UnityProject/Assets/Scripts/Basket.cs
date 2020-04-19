using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    MainGame mainGame;
    [SerializeField] Button btn;
    [SerializeField] Text txt;


    // Start is called before the first frame update
    void Start() {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

        btn.onClick.AddListener(() => {
            mainGame.EmptyBasket();
        });
    }

    // Update is called once per frame
    void Update() {
        txt.text = mainGame.dirty_diapers + "/" + mainGame.capacity_basket;
    }
}

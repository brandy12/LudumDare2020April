using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    MainGame mainGame;
    [SerializeField] Button btn;


    // Start is called before the first frame update
    void Start() {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

        btn.onClick.AddListener(() => {
            //mainGame.EmptyBasket();
            Debug.Log("Empty Basket");
        });
    }

    // Update is called once per frame
    void Update() {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harpe : MonoBehaviour
{

    MainGame mainGame;

    Image img;

    // Start is called before the first frame update
    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        img = GetComponent<Image>();

        GetComponent<Button>().onClick.AddListener(() => {
            mainGame.TuneHarp();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (mainGame.is_harp_broken) {
            img.color = Color.red;
        } else {
            img.color = Color.white;
        }
    }
}

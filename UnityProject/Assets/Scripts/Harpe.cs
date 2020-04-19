using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harpe : MonoBehaviour
{

    MainGame mainGame;


    // Start is called before the first frame update
    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

        GetComponent<Button>().onClick.AddListener(() => {
            //mainGame.TuneHarp();
            Debug.Log("Tune Harp");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

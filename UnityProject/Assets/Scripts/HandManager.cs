using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandManager : MonoBehaviour
{
    EntertainManager entertainManager;
    MainGame mainGame;

    Vector2 initialPos;
    Vector2 targetPos = new Vector2(-0.12f, 4.69f);
   // int counterSouls = 0;

    // Start is called before the first frame update
    void Start()
    {
        entertainManager = GameObject.Find("Entertainment Manager").GetComponent<EntertainManager>();
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!entertainManager.IsOn) // checking that we're not currently inputing text (and spaces)
            {
                StartCoroutine(HandMovement());
            }
        }
    }

    IEnumerator HandMovement()
    {
        transform.DOMove(targetPos, 0.1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.1f);
        transform.DOMove(initialPos, 0.6f).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soul")
        {
            mainGame.counter_souls++;
            collision.GetComponent<Soul>().Grab();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    MainGame mainGame;
    

    Vector2 initialPos;
    Vector2 targetPos = new Vector2(0f, 4.69f);
    // int counterSouls = 0;

    enum State { open, closed};
    State state;
    float threshold = 0.1f;


    //*********************************************************
    //          ANIMATIONS
    //*********************************************************

    Animation2D anim_hand_idle;
    [SerializeField] List<Sprite> sprites_hand_idle;
    [SerializeField] Sprite sprite_hand_closed;

    [SerializeField] SpriteRenderer sprite_renderer_hand;
    [SerializeField] Text textCollectedSouls; // displays number of collected souls

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start()
    {
        //entertainManager = GameObject.Find("Entertainment Manager").GetComponent<IncantationManager>();
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        initialPos = transform.position;

        anim_hand_idle = new Animation2D("hand_idle", sprites_hand_idle, 1.0f, true);
        

        Initialize();
    }

    public void Initialize() {
        state = State.open;
        anim_hand_idle.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);


        //inputs management
        if (state==State.open && Input.GetKeyDown(KeyCode.Space))
        {
            //if (!entertainManager.IsOn) // checking that we're not currently inputing text (and spaces)
            //{
            //    StartCoroutine(HandMovement());
            //}
            
            //StartCoroutine(HandMovement());
        }

        //state management
        /*if (state == State.open) {
            if ((new Vector2(transform.position.x, transform.position.y)-targetPos).magnitude < threshold) {
                state = State.closed;
            }
        } else if (state == State.closed) {
            if ((new Vector2(transform.position.x, transform.position.y) - initialPos).magnitude < threshold) {
                state=State.open;
            }
        }*/

        //animations management
        if (Input.GetMouseButton(0)) {
            state = State.closed;
            sprite_renderer_hand.sprite = sprite_hand_closed;
        }else {
            state = State.open;
            anim_hand_idle.evolve();
            sprite_renderer_hand.sprite = anim_hand_idle.currentSprite();

        }

        textCollectedSouls.text = "x" + mainGame.counter_souls.ToString();
    }

    /*IEnumerator HandMovement()
    {
        transform.DOMove(targetPos, 0.1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.1f);
        transform.DOMove(initialPos, 1.6f).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soul")
        {
            mainGame.counter_souls++;
            collision.GetComponent<Soul>().Grab();
        }
    }*/
}

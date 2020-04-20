using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baby : MonoBehaviour
{

    public int id { get; private set; }

    MainGame mainGame;

    [SerializeField] Button btn;
    Trembling trembling_component;

    AudioManager audioManager;

    bool is_mouse_hover;
    [SerializeField] SpriteRenderer sprite_mouse_hover;

    //*********************************************************
    //          BARS VARIABLES
    //*********************************************************

    //bars from 0 to 100. 0 means completely unsatisfied, 100 means totally satisfied
    public float food;
    public float entertainment;
    public float diaper;

    //amount of bars decreasing by second
    float speed_food;
    float speed_entertainment;
    float speed_diaper;

    float timer_critical_need;
    float duration_critical_need=5.0f;

    //*********************************************************
    //          SPRITE VARIABLES
    //*********************************************************

    [SerializeField] List<Sprite> sprites_food;
    [SerializeField] List<Sprite> sprites_entertainment;
    [SerializeField] List<Sprite> sprites_diaper;

    [SerializeField] List<Sprite> sprites_crying;

    [SerializeField] SpriteRenderer sprite_renderer_food;
    [SerializeField] SpriteRenderer sprite_renderer_entertainment;
    [SerializeField] SpriteRenderer sprite_renderer_diaper;

    [SerializeField] SpriteRenderer sprite_renderer_crying;

    [SerializeField] GameObject bored_animator;
    [SerializeField] GameObject hungry_animator;
    [SerializeField] GameObject[] smelly_animator;

    //*********************************************************
    //          SOUND VARIABLES
    //*********************************************************

    bool playCryingSound = false;

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start() {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        trembling_component = GetComponent<Trembling>();

        btn.onClick.AddListener(() => {
            mainGame.SelectBaby(id);
        });

        Initialize();
    }

    public void Initialize() {
        // initialize variables for the beginning of the party
        // (to be called when trying to replay

        food = UnityEngine.Random.Range(75f, 100f);
        entertainment = UnityEngine.Random.Range(35f, 100f);
        diaper = UnityEngine.Random.Range(35f, 100f);

        speed_food = UnityEngine.Random.Range(0.8f, 1.2f);
        speed_entertainment = UnityEngine.Random.Range(0.8f, 1.2f);
        speed_diaper = UnityEngine.Random.Range(0.8f, 1.2f);

        //speed_food = 0f;
        //speed_entertainment = 0f;
        //speed_diaper = 0f;

        timer_critical_need = duration_critical_need;

        is_mouse_hover = false;

        bored_animator.SetActive(false);
        hungry_animator.SetActive(false);
        smelly_animator[0].SetActive(false);
        smelly_animator[1].SetActive(false);
    }

    void Update()
    {
        BarsManagement();
        SpritesManagement();
        MouseHoverManagement();
        AnimationBabyStates();
    }

    
    //*********************************************************
    //          ID
    //*********************************************************

    public void SetId(int _id) {
        id = _id;
    }

    //*********************************************************
    //          BARS FUNCTIONS
    //*********************************************************

    void SpritesManagement() {
    

        int index_sprite_food = Mathf.FloorToInt(Engine.LinearInterpolation(food, 0, 100, 0, sprites_food.Count));
        int index_sprite_entertainment = Mathf.FloorToInt(Engine.LinearInterpolation(entertainment, 0, 100, 0, sprites_entertainment.Count));
        int index_sprite_diaper = Mathf.FloorToInt(Engine.LinearInterpolation(diaper, 0, 100, 0, sprites_diaper.Count));

        sprite_renderer_food.sprite = sprites_food[index_sprite_food];
        sprite_renderer_entertainment.sprite = sprites_entertainment[index_sprite_entertainment];
        sprite_renderer_diaper.sprite = sprites_diaper[index_sprite_diaper];

        if (CriticalNeed())
        {
            sprite_renderer_crying.sprite = sprites_crying[0];
            if (playCryingSound)
            {
                playCryingSound = false;
                GetComponent<AudioSource>().clip = audioManager.babyCry[Random.Range(0, audioManager.babyCry.Length)];
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            sprite_renderer_crying.sprite = sprites_crying[1];
            playCryingSound = true;
        }



    }

    private void AnimationBabyStates()
    {
        if (IsBored())
        {
            bored_animator.SetActive(true);
        }
        else
        {
            bored_animator.SetActive(false);
        }

        if (IsHungry())
        {
            hungry_animator.SetActive(true);
        }
        else
        {
            hungry_animator.SetActive(false);
        }

        if (IsDirty())
        {
            smelly_animator[0].SetActive(true);
            smelly_animator[1].SetActive(true);
        }
        else
        {
            smelly_animator[0].SetActive(false);
            smelly_animator[1].SetActive(false);
        }
    }

    //*********************************************************
    //          BARS FUNCTIONS
    //*********************************************************

    void BarsManagement() {
        if(food>0)
            food -= speed_food * Time.deltaTime * Random.Range(0.0f, 3.0f);
        if (food < 0)
            food = 0;
            
        if (entertainment > 0)
            entertainment -= speed_entertainment * Time.deltaTime * Random.Range(0.0f, 10.0f);
        if (entertainment < 0)
            entertainment = 0;

        if (diaper > 0)
            diaper -= speed_diaper * Time.deltaTime * Random.Range(0.0f, 10.0f);
        if (diaper < 0)
            diaper = 0;

        if (CriticalNeed()) {
            timer_critical_need -= Time.deltaTime;

            if (timer_critical_need <= 0 && mainGame.Playing) {
                StartCoroutine(mainGame.GameOver());
            }
        } else {
            timer_critical_need = duration_critical_need;
        }
    }

    public bool CriticalNeed() {
        return (food <= 0 || entertainment <= 0 || diaper <= 0) ;
    }


    //*********************************************************
    //          ACTIONS FUNCTIONS
    //*********************************************************

    public void ChangeDiaper() {
        if (IsDirty()) {
            diaper = 100;
            mainGame.dirty_diapers++;
            trembling_component.Tremble("y");
            audioManager.GetComponent<AudioSource>().PlayOneShot(audioManager.changeDiaper);
        } else {
            trembling_component.Tremble();
        }
    }

    public void GiveFood() {
        if (IsHungry()) {
            food = 100;
            mainGame.counter_souls--;
            trembling_component.Tremble("y");
            audioManager.GetComponent<AudioSource>().PlayOneShot(audioManager.eating);
        } else {
            trembling_component.Tremble();
        }
    }

    public void Entertain() {
        if (IsBored()) {
            entertainment = 100;
            trembling_component.Tremble("y");
            mainGame.GenerateNotes();
            audioManager.GetComponent<AudioSource>().PlayOneShot(audioManager.playHarp);
            if (Random.value < mainGame.probability_harp_breaks) {
                mainGame.is_harp_broken = true;
                audioManager.GetComponent<AudioSource>().PlayOneShot(audioManager.harpBroken);
            }
        } else {
            trembling_component.Tremble();
        }
    }

    public bool IsDirty() {
        return Mathf.FloorToInt(Engine.LinearInterpolation(diaper, 0, 100, 0, sprites_diaper.Count))!= sprites_diaper.Count-1;
    }
    public bool IsHungry() {
        return Mathf.FloorToInt(Engine.LinearInterpolation(food, 0, 100, 0, sprites_food.Count)) != sprites_food.Count - 1;
    }
    public bool IsBored() {
        return Mathf.FloorToInt(Engine.LinearInterpolation(entertainment, 0, 100, 0, sprites_entertainment.Count)) != sprites_entertainment.Count - 1;
    }



    void MouseHoverManagement() {

        if (is_mouse_hover) {
            sprite_mouse_hover.gameObject.SetActive(true);
        }else{
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

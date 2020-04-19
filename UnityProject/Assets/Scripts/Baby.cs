using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baby : MonoBehaviour
{

    public int id { get; private set; }

    MainGame mainGame;

    [SerializeField] Button btn;

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

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start() {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

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

        

    }

    void Update() {
        BarsManagement();
        SpritesManagement();
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

        if(CriticalNeed())
            sprite_renderer_crying.sprite = sprites_crying[0];
        else
            sprite_renderer_crying.sprite = sprites_crying[1];        
         

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

            if (timer_critical_need <= 0) {
                mainGame.GameOver();
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
        diaper = 100;
    }

    public void GiveFood() {
        food = 100;
    }

    public void Entertain() {
        entertainment = 100;
    }
}

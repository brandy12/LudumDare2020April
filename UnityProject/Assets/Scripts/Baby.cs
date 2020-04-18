using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{

    public int id { get; private set; }

    //*********************************************************
    //          BARS VARIABLES
    //*********************************************************

    //bars from 0 to 100. 0 means completely unsatisfied, 100 means totally satisfied
    float food;
    float entertainment;
    float diaper;

    //amount of bars decreasing by second
    float speed_food;
    float speed_entertainment;
    float speed_diaper;

    //*********************************************************
    //          SPRITE VARIABLES
    //*********************************************************

    [SerializeField] List<Sprite> sprites_food;
    [SerializeField] List<Sprite> sprites_entertainment;
    [SerializeField] List<Sprite> sprites_diaper;

    [SerializeField] SpriteRenderer sprite_renderer_food;
    [SerializeField] SpriteRenderer sprite_renderer_entertainment;
    [SerializeField] SpriteRenderer sprite_renderer_diaper;

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start() {
        Initialize();
    }

    public void Initialize() {
        // initialize variables for the beginning of the party
        // (to be called when trying to replay)

        id = -1; // -1 means undefined id

        food = 50;
        entertainment = 50;
        diaper = 50;

        speed_food = 1.0f;
        speed_entertainment = 1.0f;
        speed_diaper = 1.0f;

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

        /*
        
        to activate when sprites are ready

        int index_sprite_food = Mathf.FloorToInt(Engine.LinearInterpolation(food, 0, 100, 0, sprites_food.Count - 1));
        int index_sprite_entertainment = Mathf.FloorToInt(Engine.LinearInterpolation(entertainment, 0, 100, 0, sprites_entertainment.Count - 1));
        int index_sprite_diaper = Mathf.FloorToInt(Engine.LinearInterpolation(diaper, 0, 100, 0, sprites_diaper.Count - 1));

        sprite_renderer_food.sprite = sprites_food[index_sprite_food];
        sprite_renderer_entertainment.sprite = sprites_entertainment[index_sprite_entertainment];
        sprite_renderer_diaper.sprite = sprites_diaper[index_sprite_diaper];
        
         */

    }

    //*********************************************************
    //          BARS FUNCTIONS
    //*********************************************************

    void BarsManagement() {
        if(food>0)
            food -= speed_food * Time.deltaTime;
        if (food < 0)
            food = 0;
            
        if (entertainment > 0)
            entertainment -= speed_entertainment * Time.deltaTime;
        if (entertainment < 0)
            entertainment = 0;

        if (diaper > 0)
            diaper -= speed_diaper * Time.deltaTime;
        if (diaper < 0)
            diaper = 0;
    }

    public bool critical_need() {
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

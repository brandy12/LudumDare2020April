using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    //*********************************************************
    //          MOTHER VARIABLES
    //*********************************************************

    float timeSurvived; // time during which the player has survived
    float durationLevel = 100; //duration in number of characters 

    bool playing; // true if a level is currently being played. False when level is finished / not begun

    [SerializeField] Pentagram pentagram;
    [SerializeField] NotesGenerator notes_generator;

    //*********************************************************
    //          MENU VARIABLES
    //*********************************************************

    [SerializeField] GameObject start_menu;
    [SerializeField] GameObject game_over_menu;
    [SerializeField] GameObject victory_menu;
    [SerializeField] GameObject tuto_menu;

    //*********************************************************
    //          BABY VARIABLES
    //*********************************************************

    int number_babies;
    public int id_selected_baby;

    [SerializeField] Transform babies;
    [SerializeField] Baby baby_prefab;
    [SerializeField] Transform baby_highlight;

    //*********************************************************
    //          SOUL VARIABLES
    //*********************************************************

    public int counter_souls;

    [SerializeField] Transform souls;

    //*********************************************************
    //          ACTIONS VARIABLES
    //*********************************************************

    [SerializeField] HandManager hand_manager;
    [SerializeField] int incantation_counter; // increases when player writes correct incantation text
    IncantationManager incantationManager;
    [SerializeField] GameObject basket;
    AudioManager audioManager;

    public int Incantation_counter { get => incantation_counter; set => incantation_counter = value; }

    public int dirty_diapers;
    public int capacity_basket = 5;

    public float probability_harp_breaks = 0.2f;
    public bool is_harp_broken;

    //*********************************************************
    //          ANIMATIONS
    //*********************************************************


    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start() {

        hand_manager = GameObject.Find("Hand").GetComponent<HandManager>();
        incantationManager = GameObject.Find("Incantation Manager").GetComponent<IncantationManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        Initialize();
    }

    public void Initialize() {
        // initialize variables for the beginning of the party
        // (to be called when trying to replay)

        start_menu.SetActive(true);
        game_over_menu.SetActive(false);
        victory_menu.SetActive(false);
        tuto_menu.SetActive(false);
        
        playing = false;
        
        id_selected_baby = 0;

        basket.SetActive(false);
    }

    public void BeginLevel(int _number_babies)
    {
        start_menu.SetActive(false);

        basket.SetActive(true);

        number_babies = _number_babies;
        id_selected_baby = 0;

        timeSurvived = 0f;

        playing = true;

        BabiesGeneration();

        hand_manager.Initialize();
        counter_souls = 0;

        incantationManager.NewSequence();
        incantationManager.GetComponent<AudioSource>().Stop();
        incantationManager.GetComponent<AudioSource>().volume = 0.1f;
        incantationManager.GetComponent<AudioSource>().Play();
        incantation_counter = 0;

        durationLevel = 30 + (number_babies * 30);

        dirty_diapers = 0;

        is_harp_broken = false;

        foreach (Transform t in souls) {
            //Destroy(t.gameObject);
        }
    }

    void Update() {
        if (playing)
        {
            InputsManagement();
            TimeManagement();
            BabyHighlightManagement();
            UIManagement();
            AnimationsManagement();
        }
    }



    //*********************************************************
    //          INPUTS FUNCTIONS
    //*********************************************************

    void InputsManagement() {

        //baby selection

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            id_selected_baby -= 1;
            if (id_selected_baby < 0)
                id_selected_baby = number_babies - 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            id_selected_baby += 1;
            if (id_selected_baby >= number_babies)
                id_selected_baby = 0;
        }

        //actions
        
        if (SelectedBaby() != null) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                if (counter_souls > 0) {
                    SelectedBaby().GiveFood();
                } else {
                    //no more food
                    SelectedBaby().GetComponent<Trembling>().Tremble();
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                if (dirty_diapers < capacity_basket) {
                    SelectedBaby().ChangeDiaper();
                } else {
                    //no more place in the basket
                    SelectedBaby().GetComponent<Trembling>().Tremble();
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (!is_harp_broken) {
                    SelectedBaby().Entertain();
                } else {
                    //harp broken
                    SelectedBaby().GetComponent<Trembling>().Tremble();
                }
            }
        }

    }

    public void Entertain() {

        if (SelectedBaby() != null) {
            SelectedBaby().Entertain();
        }
    }

    public void TuneHarp() {
        if (is_harp_broken) {
            audioManager.GetComponent<AudioSource>().PlayOneShot(audioManager.backInTune);
            //play sound
        }
        is_harp_broken = false;
    }

    public void EmptyBasket() {
        if (dirty_diapers > 0) {

            audioManager.GetComponent<AudioSource>().PlayOneShot(audioManager.basketEmpty);
        }
        dirty_diapers = 0;
    }

    public void GenerateNotes() {
        notes_generator.GenerateNotes();
    }
    //*********************************************************
    //          UI MANAGEMENT
    //*********************************************************

    void UIManagement() {
        //Pentagram
        //pentagram.SetScale(timeSurvived/durationLevel*100.0f);
        //pentagram.SetScale(incantation_counter/durationLevel*100.0f);
        pentagram.SetScale(incantationManager.PercentageCompleted());
        
    }


    //*********************************************************
    //          TIME MANAGEMENT
    //*********************************************************

    void TimeManagement() {
        //timeSurvived += Time.deltaTime;
        //pentagram.SetScale(timeSurvived);
        
        if (incantationManager.PercentageCompleted()>=100.0f) {
            BeginLevel(number_babies + 1);
            Debug.Log("finished level");
        }
    }

    //*********************************************************
    //          BABIES GENERATION
    //*********************************************************

    void BabiesGeneration() {

        DestroyBabies();

        for (int i = 0; i < number_babies; ++i) {
            Baby b = Baby.Instantiate(baby_prefab);

            b.transform.SetParent(babies, false);
            b.SetId(i);
            b.transform.position = PositionPentagram(i, number_babies);

        }
    }

    Vector3 PositionPentagram(float index, float total) {
        float alpha=0;

        if (total == 1) {
            alpha = 0;
        } else if (total == 2) {
            if (index == 0)
                alpha = -72 * 2;
            if (index == 1)
                alpha = 72 * 2;
        } else if (total == 3) {

            if (index == 0)
                alpha = 0;
            if (index == 1)
                alpha = 72 * 2;
            if (index == 2)
                alpha = -72 * 2;
        } else if (total == 4) {

            if (index == 0)
                alpha = -72;
            if (index == 1)
                alpha = 72;
            if (index == 2)
                alpha = 72*2;
            if (index == 3)
                alpha = -72 * 2;
        } else if (total == 5) {
            if (index == 0)
                alpha = 0;
            if (index == 1)
                alpha = 72;
            if (index == 2)
                alpha = 72 * 2;
            if (index == 3)
                alpha = 72 * 3;
            if (index == 4)
                alpha = 72 * 4;

        } else {
            Debug.LogError("Error : the argument total of the function PositionPentagram has an inconsistent value (" + total + ").");
        }

        alpha -= 90.0f;
        alpha *= Mathf.PI / 180.0f;
        return pentagram.transform.position + new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0)*3.0f;
    }


    //*********************************************************
    //          BABY SELECTION
    //*********************************************************

    public void SelectBaby(int id) {
        id_selected_baby = id;
    }


    public Baby SelectedBaby() {
        foreach (Transform t in babies) {
            if (t.GetComponent<Baby>().id == id_selected_baby) {
                return t.GetComponent<Baby>();
            }
        }
        return null;
    }

    public bool BabyInCriticalNeed() {
        foreach (Transform t in babies) {
            if (t.GetComponent<Baby>().CriticalNeed())
                return true;
        }
        return false;
    }

    void BabyHighlightManagement() {
        baby_highlight.transform.position = PositionPentagram(id_selected_baby, number_babies);
    }

    public void DestroyBabies()
    {
        foreach (Transform t in babies)
        {
            Destroy(t.gameObject);
        }

    }


    //*********************************************************
    //          ANIMATIONS
    //*********************************************************

    void AnimationsManagement() {
        
    }


    //*********************************************************
    //          GAME FUNCTIONS
    //*********************************************************

    public void GameOver() {

        playing = false;

        game_over_menu.SetActive(true);
    }


    public void Victory() {

        playing = false;

        victory_menu.SetActive(true);
    }

    public void DisplayTuto()
    {
        tuto_menu.SetActive(true);
        start_menu.SetActive(false);
    }

    public void DisplayStartMenu()
    {
        tuto_menu.SetActive(false);
        start_menu.SetActive(true);
    }
}

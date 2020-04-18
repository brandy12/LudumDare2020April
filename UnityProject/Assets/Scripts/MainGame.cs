using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    //*********************************************************
    //          MOTHER VARIABLES
    //*********************************************************

    public float life;//0 = dead, 100= total life

    float speed_life;// number of life decreasing by second (when a baby is in critical need)

    float speed_pentagram; // speed at which the pentagram fills

    float timeSurvived; // time during which the player has survived

    bool playing; // true if a level is currently being played. False when level is finished / not begun

    [SerializeField] Pentagram pentagram;

    //*********************************************************
    //          BABY VARIABLES
    //*********************************************************

    int number_babies;
    public int id_selected_baby;

    [SerializeField] Transform babies;
    [SerializeField] Baby baby_prefab;
    [SerializeField] Transform baby_highlight;
    [SerializeField] GameObject start_menu;

    //*********************************************************
    //          SOUL VARIABLES
    //*********************************************************

    public int counter_souls;

    //*********************************************************
    //          ACTIONS VARIABLES
    //*********************************************************



    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start() {
        Initialize();
    }

    public void Initialize() {
        // initialize variables for the beginning of the party
        // (to be called when trying to replay)

        life = 100.0f;
        speed_pentagram = 5f;
        timeSurvived = 0f;
        playing = false;

        number_babies = 1;
        id_selected_baby = 0;

        BabiesGeneration();

    }

    public void BeginLevel(int _number_babies)
    {
        start_menu.SetActive(false);

        number_babies = _number_babies;
        id_selected_baby = 0;

        timeSurvived = 0f;

        playing = true;

        BabiesGeneration();
    }

    void Update() {
        if (playing)
        {
            InputsManagement();
            LifeManagement();
            BabyHighlightManagement();
            UIManagement();
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
                    counter_souls--;
                    SelectedBaby().GiveFood();
                } else {
                    //no more food
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                SelectedBaby().ChangeDiaper();
            }
        }

    }

    public void Entertain() {

        if (SelectedBaby() != null) {
            SelectedBaby().Entertain();
        }
    }

    //*********************************************************
    //          UI MANAGEMENT
    //*********************************************************

    void UIManagement() {
        //Pentagram

        if (life > 0) // check that we didn't lose
        {
            timeSurvived += Time.deltaTime * speed_pentagram;
            pentagram.SetScale(timeSurvived);
            if (timeSurvived >= 100f)
            {
                DestroyBabies();
                BeginLevel(number_babies + 1);
                Debug.Log("finished level");
            }
        }
    }

    //*********************************************************
    //          LIFE MANAGEMENT
    //*********************************************************

    void LifeManagement(){
        if (BabyInCriticalNeed()) {
            life -= speed_life * Time.deltaTime;

            if (life <= 0) {
                GameOver();
            }
        }
    }

    //*********************************************************
    //          BABIES GENERATION
    //*********************************************************

    void BabiesGeneration() {
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
        return new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0)*3.0f;
    }


    //*********************************************************
    //          BABY SELECTION
    //*********************************************************

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
    //          GAME FUNCTIONS
    //*********************************************************

    public void GameOver() {
        //...
    }
}

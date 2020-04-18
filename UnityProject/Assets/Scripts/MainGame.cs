using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    //*********************************************************
    //          MOTHER VARIABLES
    //*********************************************************

    float life;//0 = dead, 100= total life

    float speed_life;// number of life decreasing by second (when a baby is in critical need)

    [SerializeField] Pentagram pentagram;

    //*********************************************************
    //          BABY VARIABLES
    //*********************************************************

    int number_babies;
    int id_selected_baby;

    [SerializeField] Transform Babies;
    [SerializeField] Baby baby_prefab;


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

        number_babies = 5;
        id_selected_baby = 0;

        BabiesGeneration();

    }

    void Update() {
        InputsManagement();
        LifeManagement();

        pentagram.SetScale(Engine.LinearInterpolation(Mathf.Cos(Time.time), -1, 1, 0, 100));
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
        if (selected_baby != null) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                selected_baby.GiveFood();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                selected_baby.ChangeDiaper();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                selected_baby.Entertain();
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

            b.transform.SetParent(Babies, false);
            b.SetId(i);
            b.transform.position = Vector3.zero;

        }
    }


    //*********************************************************
    //          BABY SELECTION
    //*********************************************************

    public Baby selected_baby {
        get {
            foreach (Transform t in Babies) {
                if (t.GetComponent<Baby>().id == id_selected_baby)
                    return t.GetComponent<Baby>();
            }
            return null;
        }
    }

    public bool BabyInCriticalNeed() {
        foreach (Transform t in Babies) {
            if (t.GetComponent<Baby>().critical_need())
                return true;
        }
        return false;
    }


    //*********************************************************
    //          GAME FUNCTIONS
    //*********************************************************

    void GameOver() {
        //...
    }
}

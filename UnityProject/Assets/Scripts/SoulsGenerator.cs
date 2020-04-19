using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsGenerator : MonoBehaviour
{

    [SerializeField] Transform souls;
    [SerializeField] Soul soul_prefab;

    float timer_generation;


    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start()
    {
        SetNewTimer();
    }
    
    void Update()
    {
        SoulsGeneration();
    }


    //*********************************************************
    //          SOULS GENERATION
    //*********************************************************

    void GenerateSoul() {
        Soul s = Soul.Instantiate(soul_prefab);
        s.transform.SetParent(souls, false);

        Vector3 perturbation = new Vector3(0, Random.Range(-1.0f, 1.0f), 0);
        s.transform.position = transform.position + perturbation;

        float alpha = Random.value * 1000;
        Vector3 direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0);

        s.SetDirection(direction);
    }

    void SoulsGeneration() {
        if (timer_generation > 0) {
            timer_generation -= Time.deltaTime;
        } else {
            GenerateSoul();
            SetNewTimer();
        }
    }

    void SetNewTimer() {
        timer_generation = 5.0f  + Random.Range(-1.0f, 3.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour
{

    [SerializeField] SpriteMask Mask;

    Vector3 scale_initial;

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start()
    {
        scale_initial = Mask.transform.localScale;
    }
    
    void Update()
    {
        
    }

    public void SetScale(float life) {
        // if life = 0, then scale.h = 0
        // if life = 100 then scale.h = scale_init.h

        Mask.transform.localScale = new Vector3(1, Engine.LinearInterpolation(life, 0, 100, 0, scale_initial.y), 1);

    }
}

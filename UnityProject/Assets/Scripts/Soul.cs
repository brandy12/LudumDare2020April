using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{

    float speed = 1.0f;
    Vector3 direction;

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start()
    {
        speed = speed * Random.Range(0.7f, 1.3f);
    }
    
    void Update()
    {
        MovementManagement();
    }


    //*********************************************************
    //          MOVEMENT FUNCTIONS
    //*********************************************************

    void MovementManagement() {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir) {
        direction = dir.normalized;

        if (direction.x > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //*********************************************************
    //          GRABING SOUL
    //*********************************************************

    public void Grab() {
        Destroy(gameObject);
    }
}

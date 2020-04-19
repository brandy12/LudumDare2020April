using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soul : MonoBehaviour
{

    MainGame mainGame;

    [SerializeField] Button btn;

    float speed = 3.0f;
    Vector3 direction;
    Vector3 trajectory;

    float period_y, seed;

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

        btn.onClick.AddListener(() => {
            mainGame.counter_souls++;
            Grab();
        });

        speed = speed * Random.Range(0.7f, 1.3f);
        trajectory = transform.position;
        
        period_y = Random.Range(6.0f,8.0f);
        seed = Random.value*1000;
    }
    
    void Update()
    {
        MovementManagement();
    }


    //*********************************************************
    //          MOVEMENT FUNCTIONS
    //*********************************************************

    void MovementManagement() {
        trajectory += direction * speed * Time.deltaTime;
        transform.position = trajectory + new Vector3(
                                                        0,
                                                        Mathf.Sin(Time.time * 2 * Mathf.PI / period_y + seed),
                                                        0) * 0.3f;
    }

    public void SetDirection(Vector3 dir) {
        direction = dir.normalized;
        Vector3 s = transform.localScale;

        if (direction.x > 0) {
            transform.localScale = new Vector3(-s.x, s.y, s.z);
        } else {
            transform.localScale = new Vector3(s.x, s.y, s.z);
        }
    }

    //*********************************************************
    //          GRABING SOUL
    //*********************************************************

    public void Grab() {
        Destroy(gameObject);
    }
}

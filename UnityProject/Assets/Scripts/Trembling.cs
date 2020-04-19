using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trembling : MonoBehaviour
{

    bool is_trembling;

    Vector3 position_initial;

    float timer_trembling;
    float duration_trembling = 0.4f;

    float period_x, period_y, seed;

    // Start is called before the first frame update
    void Start()
    {
        is_trembling = false;
        position_initial = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        TremblingManagement();
    }

    public void Tremble() {
        position_initial = transform.position;

        is_trembling = true;

        timer_trembling = 0;


        period_x = Random.Range(0.1f, 0.2f);
        period_y = Random.Range(0.1f, 0.2f);
        seed = Random.value * 1000;
    }

    void TremblingManagement() {
        if (is_trembling) {

            timer_trembling += Time.deltaTime;

            if (timer_trembling >= duration_trembling) {
                is_trembling = false;
            }

            transform.position = position_initial + new Vector3(Mathf.Cos(Time.time * 2 * Mathf.PI / period_x + seed),
                                                        0,
                                                        0)*0.2f;
        } else {
            transform.position = position_initial;
        }
        
    }
}

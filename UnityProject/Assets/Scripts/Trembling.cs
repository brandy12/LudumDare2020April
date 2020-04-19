using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trembling : MonoBehaviour
{

    bool is_trembling;

    Vector3 position_initial;

    float timer_trembling;
    float duration_trembling = 0.4f;
    string type;

    float period_x, period_y, seed;

    // Start is called before the first frame update
    void Start()
    {
        is_trembling = false;
        position_initial = transform.position;
        type = "";

    }

    // Update is called once per frame
    void Update()
    {
        TremblingManagement();
    }

    public void Tremble(string _type="x") {
        position_initial = transform.position;
        type = _type;

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

            if (type == "x") {
                transform.position = position_initial + new Vector3(Mathf.Cos(Time.time * 2 * Mathf.PI / period_x + seed),
                                                            0,
                                                            0) * 0.2f;
            } else if (type == "xy") {
                transform.position = position_initial + new Vector3(Mathf.Cos(Time.time * 2 * Mathf.PI / period_x + seed),
                                                                Mathf.Sin(Time.time * 2 * Mathf.PI / period_y + seed),
                                                                0) * 0.2f;
            } else if (type == "y") {
                transform.position = position_initial + new Vector3(0, 
                                                                Mathf.Sin(Time.time * 2 * Mathf.PI / period_y + seed),
                                                                0) * 0.2f;
            }
        } else {
            transform.position = position_initial;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{

    float period_x;
    float period_y;
    float seed;

    float z;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        period_x = Random.Range(10.9f, 20.1f);
        period_y = Random.Range(10.9f, 20.1f);
        seed = Random.value * 1000;

        z = transform.position.z;
        radius = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Cos(Time.time * 2 * Mathf.PI / period_x + seed),
                                        Mathf.Sin(Time.time * 2 * Mathf.PI / period_y + seed),
                                        0) * radius
                                        + new Vector3(0, 0, z) ;
    }
}

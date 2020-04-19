using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    Vector3 trajectory;

    float timer;
    float duration=1.2f;

    float seed;

    Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

        seed = Random.value * 1000;

        float alpha = Random.Range(120, 240);

        alpha *= Mathf.PI / 180.0f;

        trajectory = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0)*0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        MovementManagement();
        OpacityManagement();

        if (timer > duration) {
            Destroy(gameObject);
        }
    }

    void MovementManagement() {

        trajectory *= 0.99f;

        float period = 0.8f;
        Vector3 perturbation = new Vector3(0, Mathf.Cos(Time.time * 2 * Mathf.PI / period+seed), 0)*0.08f;

        transform.position += trajectory + perturbation;
    }

    void OpacityManagement() {
        timer += Time.deltaTime;

        Color c = img.color;
        img.color = new Color(c.r, c.g, c.b, 1 - timer / duration);

        transform.localScale += new Vector3(1, 1, 1) * 0.03f;


    }
}

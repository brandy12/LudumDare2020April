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

    float period_x, period_y, seed;


    //*********************************************************
    //          ANIMATION
    //*********************************************************

    [SerializeField] List<Sprite> sprites_idle;
    [SerializeField] SpriteRenderer sprite_renderer;

    Animation2D anim_idle;

    //*********************************************************
    //          UNITY FUNCTIONS
    //*********************************************************

    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();

        anim_idle = new Animation2D("soul_idle", sprites_idle, 1.0f, true);

        btn.onClick.AddListener(() => {
            mainGame.counter_souls++;
            Grab();
        });

        speed = speed * Random.Range(0.7f, 1.3f);
        trajectory = transform.position;
        
        period_x = Random.Range(6.0f,8.0f);
        period_y = Random.Range(6.0f,8.0f);
        seed = Random.value*1000;
    }
    
    void Update()
    {
        MovementManagement();

        if ((transform.position).magnitude > 100.0f) {
            Destroy(gameObject);
        }

        //anim_idle.evolve();
        //sprite_renderer.sprite = anim_idle.currentSprite();
    }


    //*********************************************************
    //          MOVEMENT FUNCTIONS
    //*********************************************************

    void MovementManagement() {
        trajectory += direction * speed * Time.deltaTime;
        transform.position = trajectory + new Vector3(
                                                        Mathf.Cos(Time.time * 2 * Mathf.PI / period_x + seed),
                                                        Mathf.Sin(Time.time * 2 * Mathf.PI / period_y + seed),
                                                        0) * 0.3f;
    }

    public void SetDirection(Vector3 dir) {
        direction = dir.normalized;
        Vector3 s = transform.localScale;

        
    }

    //*********************************************************
    //          GRABING SOUL
    //*********************************************************

    public void Grab() {
        Destroy(gameObject);
    }
}

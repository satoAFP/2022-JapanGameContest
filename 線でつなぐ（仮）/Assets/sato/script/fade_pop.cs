using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade_pop : MonoBehaviour
{
    [SerializeField, Header("フェード速度"), Range(0f, 0.02f)] float fade_speed;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //透過度を上げる
        gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, fade_speed);

        //完全に透過したら消す
        if (gameObject.GetComponent<Image>().color.a < 0)
            gameObject.SetActive(false);
    }
}

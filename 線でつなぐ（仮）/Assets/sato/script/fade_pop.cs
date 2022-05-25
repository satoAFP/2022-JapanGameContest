using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade_pop : MonoBehaviour
{
    [SerializeField, Header("文字出したい時")] bool text_fade;

    [SerializeField, Header("フェード速度"), Range(0f, 0.02f)] float fade_speed;

    [SerializeField, Header("フェードさせたいテキスト")] GameObject text_fade_obj;

    //最初だけ表示
    private bool first = true;


    private void Start()
    {
        //表示用テキスト入力
        text_fade_obj.GetComponent<Text>().text = GameObject.Find("stage_clear_check").GetComponent<stage_clear>().text_mem;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //テキスト表示させたいとき
        if (text_fade)
        {
            //テキストが表示され切ったとき
            if (text_fade_obj.GetComponent<Text>().color.a > 1.0f)
            {
                first = false;
            }

            //テキスト表示
            if (first)
                text_fade_obj.GetComponent<Text>().color += new Color(0, 0, 0, fade_speed);
            else
            {
                //テキスト非表示
                text_fade_obj.GetComponent<Text>().color -= new Color(0, 0, 0, fade_speed);

                //透過度を上げる
                gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, fade_speed);

                //完全に透過したら消す
                if (gameObject.GetComponent<Image>().color.a < 0)
                    gameObject.SetActive(false);
            }
        }
        else
        {
            //透過度を上げる
            gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, fade_speed);

            //完全に透過したら消す
            if (gameObject.GetComponent<Image>().color.a < 0)
                gameObject.SetActive(false);
        }
    }
}

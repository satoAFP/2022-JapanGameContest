using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class time_count : MonoBehaviour
{
    [SerializeField, Header("終了時間(秒)")] float total_time;
    [SerializeField, Header("ダメージフェード")] GameObject fade;
    [SerializeField, Header("shut_out")] GameObject shut_out;

    private int minute;
    private int seconds;
    private int count;
    private bool time_move = true;

    [SerializeField, Header("shut_out")] private float fade_speed;

    //連続で押されないための判定
    private bool key_check_E = true;

    // Start is called before the first frame update
    void Start()
    {
        fade_speed = 1 / total_time;

        minute = (int)total_time / 60;
        seconds = (int)total_time % 60;
        total_time = (int)total_time % 60;
        count = (int)total_time - 1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ポーズ時時間を止める
        if (Input.GetKey(KeyCode.E))
        {
            if (key_check_E)
            {
                if (time_move)
                    time_move = false;
                else
                    time_move = true;
            }
            key_check_E = false;
        }
        else { key_check_E = true; }

        //時間のカウント処理
        if (time_move)
        {
            //分の処理
            if (total_time < 0)
            {
                minute--;
                total_time = 60;
            }
            total_time -= Time.deltaTime;
            seconds = (int)total_time;

            //時間の表示
            gameObject.GetComponent<TextMesh>().text = minute.ToString("d2") + ":" + seconds.ToString("d2");

            //フェード移行処理
            if ((int)total_time == count)
            {
                fade.GetComponent<Image>().color += new Color(0, 0, 0, fade_speed);
                count--;
            }

            //死んだときのフェード
            if ((int)total_time <= 0)
                shut_out.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);

            //画面が光ってステージセレクトに戻される
            if(shut_out.GetComponent<Image>().color.a>=0.5f)
                SceneManager.LoadScene("STAGE_SELECT");

        }
    }
}

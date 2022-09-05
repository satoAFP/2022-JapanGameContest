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
    [SerializeField, Header("時計だけ動かしたいときtrue")] bool clock_only;

    [SerializeField, Header("SE_心音")] AudioSource heart_beat;
    [SerializeField, Header("SE_吐息")] AudioSource breath;

    [SerializeField, Header("ゲームオーバーパネル")] GameObject gameover_panel;


    private int minute;             //分
    private int seconds;            //秒
    private float mem_total_time;   //終了時間記憶用
    private int count;              //1秒刻みのカウント
    private bool time_move = true;  //メニュー表示中は出さない
    private float fade_speed;       //ダメージフェードのフェード速度
    private float sound_fade_speed; //SEフェードのフェード速度

    //連続で押されないための判定
    private bool key_check_E = true;

    // Start is called before the first frame update
    void Start()
    {
        fade_speed = 1 / total_time;
        sound_fade_speed = 0.4f / total_time;
        mem_total_time = total_time;
        count = (int)total_time - 1;

        minute = (int)total_time / 60;
        seconds = (int)total_time % 60;
        total_time = (int)total_time % 60;

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
            if (mem_total_time > 0)
            {
                //分の処理
                if (total_time < 0)
                {
                    minute--;
                    total_time = 60;
                }
                total_time -= Time.deltaTime;
                mem_total_time -= Time.deltaTime;
                seconds = (int)total_time;
            }
            else
            {
                heart_beat.volume = 0;
                breath.volume = 0;
            }

            //時間の表示
            gameObject.GetComponent<TextMesh>().text = minute.ToString("d2") + ":" + seconds.ToString("d2");

            //時計だけ動かしたいとき
            if (!clock_only)
            {
                //フェード移行処理
                if ((int)mem_total_time == count)
                {
                    fade.GetComponent<Image>().color += new Color(0, 0, 0, fade_speed);
                    heart_beat.volume += sound_fade_speed;
                    breath.volume += sound_fade_speed;
                    count--;
                }

                //死んだときのフェード
                if ((int)mem_total_time <= 0)
                    shut_out.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);

                //フェードでゲームオーバーが知らされる
                if (shut_out.GetComponent<Image>().color.a >= 1.0f)
                {
                    gameover_panel.GetComponent<Text>().color += new Color(0, 0, 0, 0.01f);

                    //左クリックでステージセレクト
                    if (Input.GetMouseButton(0))
                    {
                        //GameObject.Find("stage_clear_check").GetComponent<stage_clear>().text_mem = "";
                        SceneManager.LoadScene("LOSEENDING");
                    }
                }

            }

        }
    }
}

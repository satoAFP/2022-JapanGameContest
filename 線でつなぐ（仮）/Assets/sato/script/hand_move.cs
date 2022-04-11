using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_move : MonoBehaviour
{
    [SerializeField, Header("手の動く速度"), Range(1, 10)] int ani_speed;
    [SerializeField, Header("true = 右手　false = 左手")] bool hand_check;

    private Vector3[] ani_pos_right = new Vector3[10];      //右手のアニメーション
    private Vector3[] ani_pos_left = new Vector3[10];       //左手のアニメーション
    private int[] ani_count = new int[2];                   //現在のアニメーション番号
    private bool[] ani_check = new bool[2];                 //再生中か逆再生中か
    private int frame = 0;                                  //開始時からのフレーム


    // Start is called before the first frame update
    void Start()
    {
        //手のアニメーション座標決定
        for(int i=0;i<10;i++)
        {
            ani_pos_right[i].x = 0.5f + (0.01f * i);
            ani_pos_right[i].y = 0.7f - (0.005f * i);
            ani_pos_right[i].z = 0.5f - (0.005f * i);
        }
        for (int i = 0; i < 10; i++)
        {
            ani_pos_left[i].x = -0.5f - (0.01f * i);
            ani_pos_left[i].y = 0.7f - (0.005f * i);
            ani_pos_left[i].z = 0.5f - (0.005f * i);
        }

        //配列の初期化
        ani_count[0] = 4;
        ani_count[1] = 5;
        ani_check[0] = true;
        ani_check[1] = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //移動中のみ手が動く(仮)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //右手と左手それぞれの動き
            if (hand_check)
                hand_move_right();
            else
                hand_move_left();
        }

        //フレームの加算
        frame++;
    }


    private void hand_move_right()
    {
        //座標の更新
        this.gameObject.transform.localPosition = ani_pos_right[ani_count[0]];

        //0～9番のアニメーションを行き来させるための処理
        if (ani_count[0] == 9)
            ani_check[0] = false;
        else if (ani_count[0] == 0)
            ani_check[0] = true;

        //指定したフレーム毎にアニメーションを一つ進める
        if (frame % ani_speed == 0)
        {
            if (ani_check[0])
                ani_count[0]++;
            else
                ani_count[0]--;
        }
    }

    private void hand_move_left()
    {
        //座標の更新
        this.gameObject.transform.localPosition = ani_pos_left[ani_count[1]];

        //0～9番のアニメーションを行き来させるための処理
        if (ani_count[1] == 9)
            ani_check[1] = false;
        else if (ani_count[1] == 0)
            ani_check[1] = true;

        //指定したフレーム毎にアニメーションを一つ進める
        if (frame % ani_speed == 0)
        {
            if (ani_check[1])
                ani_count[1]++;
            else
                ani_count[1]--;
        }
    }


}

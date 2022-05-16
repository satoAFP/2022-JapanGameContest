using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_move : MonoBehaviour
{
    //インスペクター設定
    [SerializeField, Header("手の動く速度"), Range(1, 10)] int ani_speed;
    [SerializeField, Header("手の上下の速度"), Range( 0.001f, 0.02f)] float updown_speed;
    [SerializeField, Header("true = 右手　false = 左手")] bool hand_check;

    //ゲームオブジェクトの取得
    [SerializeField, Header("カメラ"), Header("ゲームオブジェクトの取得")] GameObject camera;
    [SerializeField, Header("手に持つブロック")] GameObject catch_block;
    [SerializeField, Header("camera_all")] GameObject camera_all;

    //アニメーション取得
    [SerializeField, Header("hand_pos")] Animator up_anim;

    private Vector3[] move_ani_pos_right = new Vector3[10]; //移動時の右手のアニメーション
    private Vector3[] move_ani_pos_left = new Vector3[10];  //移動時の左手のアニメーション
    private Vector3[] grab_ani_pos_right = new Vector3[10]; //掴んだ時の右手のアニメーション
    private Vector3[] grab_ani_pos_left = new Vector3[10];  //掴んだ時の左手のアニメーション
    private int[] ani_count = new int[2];                   //現在のアニメーション番号
    private bool[] ani_check = new bool[2];                 //再生中か逆再生中か
    private int frame = 0;                                  //開始時からのフレーム
    private Vector3 now_pos;                                //現在の座標
    private float move_amount_y = 0.0f;                     //行動を起こすと手が出てくる移動量
    private bool move_check = false;                        //主人公が移動中かを取得するよう
    private bool grab_check = false;                        //主人公が物を持ってる判定取得
    private Animator wolk_anim;                             //手を動かすアニメーション
    private GameObject grab_block = null;                   //持った物の情報
    private Vector3 grab_size;                              //持ったオブジェクトのサイズ記憶

    // Start is called before the first frame update
    void Start()
    {
        now_pos = this.gameObject.transform.localPosition;

        //アニメーション
        wolk_anim = gameObject.GetComponent<Animator>();

        //手のアニメーション座標決定
        for (int i = 0; i < 10; i++) 
        {
            //移動時の右手のアニメーション
            move_ani_pos_right[i].x = 0.5f + (0.01f * i);
            move_ani_pos_right[i].y = 0.7f - (0.005f * i);
            move_ani_pos_right[i].z = 0.5f - (0.01f * i);
            //移動時の左手のアニメーション
            move_ani_pos_left[i].x = -0.5f - (0.01f * i);
            move_ani_pos_left[i].y = 0.7f - (0.005f * i);
            move_ani_pos_left[i].z = 0.5f - (0.01f * i);

            //掴んだ時の右手のアニメーション
            grab_ani_pos_right[i].x = 0.5f + (0.01f * i);
            grab_ani_pos_right[i].y = 0.7f - (0.005f * i);
            grab_ani_pos_right[i].z = 0.5f - (0.01f * i);
            //掴んだ時の左手のアニメーション
            grab_ani_pos_left[i].x = -0.5f - (0.01f * i);
            grab_ani_pos_left[i].y = 0.7f - (0.005f * i);
            grab_ani_pos_left[i].z = 0.5f - (0.01f * i);
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
        //欲しい情報の更新
        move_check = transform.root.gameObject.GetComponent<player>().Move_check;
        grab_check = camera.GetComponent<BoxCastRayTest>().grab;
        grab_block = camera.GetComponent<BoxCastRayTest>().Target;
        if (grab_block != null)
        {
            //grab_block.transform.parent = camera_all.gameObject.transform;
            //grab_block.transform.localPosition = new Vector3(0.5f, -0.25f, 1.0f);
        }


        //移動中の手の動き--------------------------------------------------------
        if (move_check)
        {
            //右手と左手それぞれの動き
            wolk_anim.SetBool("move_ani_start", true);

            if (!grab_check)
                up_anim.SetBool("hand_move", true);
            else
                up_anim.SetBool("hand_move", false);

        }
        else
        {
            wolk_anim.SetBool("move_ani_start", false);

            up_anim.SetBool("hand_move", false);

            
        }
        //------------------------------------------------------------------------
        if(grab_check)
        {
            wolk_anim.SetBool("catch", true);
            //catch_block.SetActive(true);
        }
        else
        {
            wolk_anim.SetBool("catch", false);
            //catch_block.SetActive(false);
        }


        //物を動かすときの手の動き------------------------------------------------
        //if (move_check)
        //{
        //    //右手と左手それぞれの動き
        //    if (hand_check)
        //        hand_move_right(now_pos.y);
        //    else
        //        hand_move_left(now_pos.y);

        //    now_pos = this.gameObject.transform.localPosition;

        //    move_amount_y += updown_speed;
        //    if (move_amount_y <= 0.2f)
        //        now_pos.y += updown_speed;
        //    else
        //        move_amount_y = 0.2f;
        //}
        //else
        //{
        //    move_amount_y -= updown_speed;
        //    if (move_amount_y >= 0)
        //        now_pos.y -= updown_speed;
        //    else
        //        move_amount_y = 0;
        //}
        //------------------------------------------------------------------------


        //現在の計算後の座標代入
        this.gameObject.transform.localPosition = now_pos;

        //フレームの加算
        frame++;
    }

    //移動時の右手の動き
    private void hand_move_right(float y)
    {
        //座標の更新
        move_ani_pos_right[ani_count[0]].y = y;
        this.gameObject.transform.localPosition = move_ani_pos_right[ani_count[0]];

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

    //移動時の左手の動き
    private void hand_move_left(float y)
    {
        //座標の更新
        move_ani_pos_left[ani_count[1]].y = y;
        this.gameObject.transform.localPosition = move_ani_pos_left[ani_count[1]];

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


    //掴んでいるときの右手の動き
    private void hand_grab_left(float y)
    {
        //座標の更新
        move_ani_pos_left[ani_count[1]].y = y;


    }


}

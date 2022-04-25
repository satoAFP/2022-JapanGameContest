using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class player : MonoBehaviour
{
    //インスペクター設定
    [SerializeField, Header("主人公の移動量"), Header("主人公のステータス"), Range(0, 10)]     float move_power;
    [SerializeField, Header("マウス感度"), Range(100, 300)]          float mouse_power;
    [SerializeField, Header("ジャンプ力"), Range(0, 10)]             float jump_power;
    [SerializeField, Header("壁を上る速度"), Range(0.01f, 0.05f)]    float climbing_speed;
    [SerializeField, Header("マウス上下の限界"), Range(0, 0.5f)]     float mouse_max_y;
    [SerializeField, Header("フェードの時間"), Range(0.5f, 3.0f)]    float fade_time;

    //ゲームオブジェクトの取得
    [SerializeField, Header("主人公のカメラセット"), Header("ゲームオブジェクトの取得")] GameObject my_camera;
    [SerializeField, Header("通常カメラ")] GameObject camera;
    [SerializeField, Header("fade用image")] GameObject fade;
    [SerializeField, Header("climbing_check_head")]     GameObject head;
    [SerializeField, Header("climbing_check_leg")]      GameObject leg;

    //他のスクリプトとやり取りする変数
    [Header("他のスクリプトとやり取りする変数")] 
    public bool Ground_check = true;                               //着地しているかどうかの判定
    public bool Move_check = false;                                //移動しているかどうかの判定


    //カーソルの移動設定
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);


    //プライベート変数
    private Vector3 velocity;                                       //リジットボディの力
    private Rigidbody rb;                                           //リジッドボディを取得するための変数
    private float mem_camera_rotato_y = 0;                          //カメラのY軸回転記憶
    private Transform camTransform;                                 //cameraのtransform
    private Vector3 startMousePos;                                  //マウス操作の始点
    private Vector3 presentCamRotation;                             //カメラ回転の始点情報
    private Vector3 cursol_pos_check;                               //カーソルの座標記憶
    private Vector3 vertual_cursol_pos = new Vector3(1000, 0, 0);   //実際のカーソルの移動量分の座標
    private bool cursol_reset = false;                              //カーソルの座標がリセットされたときtrue
    private bool cursol_pop = false;                                //カーソルを出現させるかどうか
    private bool climbing_check_head = false;                       //壁のぼりが出来る高さか判定
    private bool climbing_check_leg = false;                        //いつまで壁のぼりするか判定
    private bool camera_change = true;                              //カメラの切り替え
    private bool fade_check = false;                                //フェードするかどうか切り替え
    private bool fade_updown = true;                                //透過レベルが上がるか下がるか
    private bool first = true;                                      //一番最初の処理だけ実行


    //連続で押されないための判定
    private bool key_check_E = true;
    private bool key_check_C = true;
    private bool key_check_Space = true;



    // Start is called before the first frame update
    void Start()
    {
        //リジッドボディを取得
        rb = GetComponent<Rigidbody>();
        
        //カーソルを消して、中央にロック
        Cursor.visible = false;
        SetCursorPos(1024, 576);
        //my_camera.transform.rotation = Quaternion.identity;

        //カメラ関係初期化
        camTransform = this.gameObject.transform;
        //startMousePos = Input.mousePosition;
        presentCamRotation.x = camTransform.transform.eulerAngles.x;
        presentCamRotation.y = camTransform.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //情報の更新--------------------------------------------------------------------------------------------
        //主人公が壁を上るときの判定の更新
        climbing_check_head = head.GetComponent<climbing_check>().check;
        climbing_check_leg = leg.GetComponent<climbing_check>().check;

        //--------------------------------------------------------------------------------------------
        //Debug.Log("" + Input.mousePosition);

        //カーソルの座標がリセットされたとき、移動量がリセットされないよう
        if (cursol_reset)
        {
            cursol_pos_check = Input.mousePosition;
            cursol_reset = false;
        }


        //カーソルの表示、非表示
        if (Input.GetKey(KeyCode.E))
        {
            if (key_check_E)
            {
                if (cursol_pop)
                {
                    cursol_pop = false;
                    Cursor.visible = false;
                }
                else
                {
                    cursol_pop = true;
                    Cursor.visible = true;
                    SetCursorPos(1024, 576);
                }
            }
            key_check_E = false;
        }
        else{ key_check_E = true; }


        //メニュー表示中は動けない
        if (!cursol_pop)
        {
            //マウス操作-----------------------------------------------------------------------------------------
            CameraRotationMouseControl();



            //左右上下の移動処理---------------------------------------------------------------------------------
            if (Input.GetKey(KeyCode.W))
            {
                velocity = gameObject.transform.rotation * new Vector3(0, 0, move_power);
                Move(velocity * Time.deltaTime);
                Move_check = true;

                if (!climbing_check_head)
                {
                    if (climbing_check_leg)
                    {
                        this.gameObject.transform.position += new Vector3(0, climbing_speed, 0);
                    }
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity = gameObject.transform.rotation * new Vector3(-move_power, 0, 0);
                Move(velocity * Time.deltaTime);
                Move_check = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity = gameObject.transform.rotation * new Vector3(0, 0, -move_power);
                Move(velocity * Time.deltaTime);
                Move_check = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity = gameObject.transform.rotation * new Vector3(move_power, 0, 0);
                Move(velocity * Time.deltaTime);
                Move_check = true;
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
                Move_check = false;
        }


        //地面の着地しているかどうか判定
        //着地しているとき
        if (Ground_check)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (key_check_Space)
                {
                    rb.AddForce(new Vector3(0, jump_power * 100, 0)); //上に向かって力を加える
                    key_check_Space = false;
                }
            }
            else
                key_check_Space = true;
        }


        //カーソルの座標記憶
        cursol_pos_check = Input.mousePosition;



        //グレースケールカメラ切り替え
        if (Input.GetKey(KeyCode.C))
        {
            if (key_check_C)
            {
                //フェードオン
                fade_check = true;

                //カメラ切り替え
                if (camera_change)
                {
                    camera.GetComponent<PostEffect>().enabled = true;
                    camera_change = false;
                }
                else
                {
                    camera.GetComponent<PostEffect>().enabled = false;
                    camera_change = true;
                }
            }
            key_check_C = false;
        }
        else { key_check_C = true; }

        //フェード処理
        if(fade_check)
        {
            //フェード実行
            if (fade_updown)
                fade.GetComponent<Image>().color += new Color(0, 0, 0, 0.1f);
            else
                fade.GetComponent<Image>().color -= new Color(0, 0, 0, 0.1f);

            //透過レベルの上げ下げ切り替え
            if (fade.GetComponent<Image>().color.a >= fade_time)
            {
                fade_updown = false;
            }
            else if (fade.GetComponent<Image>().color.a <= 0)
            {
                fade_updown = true;
                fade_check = false;
            }
        }
    }



    //移動処理関数
    private void Move(Vector3 vec)
    {
        this.gameObject.transform.position += vec;
    }

    

    //カメラコントロール関数
    private void CameraRotationMouseControl()
    {
        //my_camera.transform.rotation = Quaternion.identity;

        //実際のカーソルの移動量計算
        vertual_cursol_pos.x += Input.mousePosition.x - cursol_pos_check.x;
        vertual_cursol_pos.y += Input.mousePosition.y - cursol_pos_check.y;
        
        if (first)
        {
            startMousePos = vertual_cursol_pos;
            first = false;
        }
        Debug.Log(startMousePos);

        //(移動開始座標 - 実際のカーソルの座標) / 解像度 で正規化
        float x = (-startMousePos.x + vertual_cursol_pos.x) / Screen.width;
        float y = mem_camera_rotato_y;
        
        
        
        //Y軸の回転は一定値(mouse_max_y)で止まる
        if (((startMousePos.y - vertual_cursol_pos.y) / Screen.height) <= mouse_max_y &&
            ((startMousePos.y - vertual_cursol_pos.y) / Screen.height) >= -mouse_max_y)
        {
            //(移動開始座標 - 実際のカーソルの座標) / 解像度 で正規化
            y = (startMousePos.y - vertual_cursol_pos.y) / Screen.height;
            mem_camera_rotato_y = y;
        }
        else
        {
            //Y軸の移動限界まで来たとき、そこからvertual_cursol_pos.yが進まないように固定
            if (((startMousePos.y - vertual_cursol_pos.y) / Screen.height) > mouse_max_y)
            {
                vertual_cursol_pos.y = -(mouse_max_y * Screen.height - startMousePos.y);
            }
            if (((startMousePos.y - vertual_cursol_pos.y) / Screen.height) < -mouse_max_y)
            {
                vertual_cursol_pos.y = -(-mouse_max_y * Screen.height - startMousePos.y);
            }
        }

        //回転開始角度 ＋ マウスの変化量 * 90
        float eulerX = y * mouse_power;
        float eulerY = x * mouse_power;


        if (!cursol_pop)
        {
            //もしカーソルが子の座標内から出たらカーソルの位置をリセット
            if (Input.mousePosition.x > 950 || Input.mousePosition.x < 35 ||
                Input.mousePosition.y > 540 || Input.mousePosition.y < 40)
            {
                SetCursorPos(1024, 576);
                cursol_reset = true;
            }
        }
        
        //主人公とカメラにそれぞれ、回転量代入
        camTransform.rotation = Quaternion.Euler(0, eulerY, 0);
        my_camera.transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        //Debug.Log(eulerX+" : "+ eulerY);
        
    }
}

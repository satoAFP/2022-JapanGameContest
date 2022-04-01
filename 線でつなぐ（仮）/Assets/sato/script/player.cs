using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class player : MonoBehaviour
{
    //インスペクター設定
    [SerializeField, Header("主人公の移動量"), Range(0, 10)]     float move_power;
    [SerializeField, Header("マウス感度"), Range(100, 300)]      float mouse_power;
    [SerializeField, Header("ジャンプ力"), Range(0, 10)]         float jump_power;
    [SerializeField, Header("マウス上下の限界"), Range(0, 0.5f)] float mouse_max_y;

    [SerializeField, Header("主人公のカメラ")] GameObject my_camera;


    //カーソルの移動設定
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);


    //プライベート変数
    private Vector3 velocity;               //リジットボディの力
    private Rigidbody rb;                   //リジッドボディを取得するための変数
    private bool isGround = true;           //着地しているかどうかの判定
    private float mem_camera_rotato_y = 0;  //カメラのY軸回転記憶
    private Transform camTransform;         //cameraのtransform
    private Vector3 startMousePos;          //マウス操作の始点
    private Vector3 presentCamRotation;     //カメラ回転の始点情報
    private Vector3 cursol_pos_check;       //カーソルの座標記憶
    private Vector3 vertual_cursol_pos = new Vector3(1000, 0, 0);     //実際のカーソルの移動量分の座標
    private bool cursol_reset = false;      //カーソルの座標がリセットされたときtrue
    private bool cursol_pop = false;        //カーソルを出現させるかどうか

    // Start is called before the first frame update
    void Start()
    {
        //リジッドボディを取得
        rb = GetComponent<Rigidbody>(); 

        //カメラ関係初期化
        camTransform = this.gameObject.transform;
        startMousePos = Input.mousePosition;
        presentCamRotation.x = camTransform.transform.eulerAngles.x;
        presentCamRotation.y = camTransform.transform.eulerAngles.y;

        //カーソルを消して、中央にロック
        Cursor.visible = false;
        SetCursorPos(1000, 600);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //カーソルの座標がリセットされたとき、移動量がリセットされないよう
        if(cursol_reset)
        {
            cursol_pos_check = Input.mousePosition;
            cursol_reset = false;
        }


        //カーソルの表示、非表示
        if(Input.GetKeyDown(KeyCode.E))
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
            }
        }


        //カメラの回転 マウス
        CameraRotationMouseControl();


        //左右上下の移動処理
        if (Input.GetKey(KeyCode.W))
        {
            velocity = gameObject.transform.rotation * new Vector3(0, 0, move_power);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity = gameObject.transform.rotation * new Vector3(-move_power, 0, 0);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity = gameObject.transform.rotation * new Vector3(0, 0, -move_power);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity = gameObject.transform.rotation * new Vector3(move_power, 0, 0);
            Move(velocity * Time.deltaTime);
        }


        //地面の着地しているかどうか判定
        //着地しているとき
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGround = false;//  isGroundをfalseにする
                rb.AddForce(new Vector3(0, jump_power*100, 0)); //上に向かって力を加える
            }
        }


        //カーソルロック解除
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }

        //カーソルの座標記憶
        cursol_pos_check = Input.mousePosition;
    }

    void OnCollisionEnter(Collision other)
    {
        //Groundタグのオブジェクトに触れたとき
        if (other.gameObject.tag == "Ground") 
        {
            //isGroundをtrueにする
            isGround = true; 
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
        //実際のカーソルの移動量計算
        vertual_cursol_pos.x += Input.mousePosition.x - cursol_pos_check.x;
        //(移動開始座標 - 実際のカーソルの座標) / 解像度 で正規化
        float x = (startMousePos.x + vertual_cursol_pos.x) / Screen.width;
        float y = mem_camera_rotato_y;

        //実際のカーソルの移動量計算
        vertual_cursol_pos.y += Input.mousePosition.y - cursol_pos_check.y;
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
                SetCursorPos(1000, 600);
                cursol_reset = true;
            }
        }

        //主人公とカメラにそれぞれ、回転量代入
        camTransform.rotation = Quaternion.Euler(0, eulerY, 0);
        my_camera.transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
    }
}

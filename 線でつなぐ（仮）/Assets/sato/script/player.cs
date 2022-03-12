using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //インスペクター設定
    [SerializeField, Header("主人公の移動量"), Range(0, 10)]     float move_power;
    [SerializeField, Header("マウス感度"), Range(100, 300)]      float mouse_power;
    [SerializeField, Header("ジャンプ力"), Range(0, 10)]         float jump_power;
    [SerializeField, Header("マウス上下の限界"), Range(0, 0.5f)] float mouse_max_y;

    [SerializeField, Header("主人公のカメラ")] GameObject my_camera;

    //プライベート変数
    private Vector3 velocity;               //リジットボディの力
    private Rigidbody rb;                   //リジッドボディを取得するための変数
    private bool isGround = true;           //着地しているかどうかの判定
    private float mem_camera_rotato_y = 0;  //カメラのY軸回転記憶
    private Transform camTransform;         //cameraのtransform
    private Vector3 startMousePos;          //マウス操作の始点
    private Vector3 presentCamRotation;     //カメラ回転の始点情報

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //リジッドボディを取得

        //カメラ関係初期化
        camTransform = this.gameObject.transform;
        startMousePos = Input.mousePosition;
        presentCamRotation.x = camTransform.transform.eulerAngles.x;
        presentCamRotation.y = camTransform.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (isGround == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGround = false;//  isGroundをfalseにする
                rb.AddForce(new Vector3(0, jump_power*100, 0)); //上に向かって力を加える
            }
        }
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
        //(移動開始座標 - マウスの現在座標) / 解像度 で正規化
        float x = (startMousePos.x + Input.mousePosition.x) / Screen.width;
        float y = mem_camera_rotato_y;

        //Y軸の回転は一定値(mouse_max_y)で止まる
        if (((startMousePos.y - Input.mousePosition.y) / Screen.height) <= mouse_max_y &&
            ((startMousePos.y - Input.mousePosition.y) / Screen.height) >= -mouse_max_y)
        {
            y = (startMousePos.y - Input.mousePosition.y) / Screen.height;
            mem_camera_rotato_y = y;
        }

        //回転開始角度 ＋ マウスの変化量 * 90
        float eulerX = presentCamRotation.x + y * mouse_power;
        float eulerY = presentCamRotation.y + x * mouse_power;

        //主人公とカメラにそれぞれ、回転量代入
        camTransform.rotation = Quaternion.Euler(0, eulerY, 0);
        my_camera.transform.rotation= Quaternion.Euler(eulerX, eulerY, 0);
    }
}

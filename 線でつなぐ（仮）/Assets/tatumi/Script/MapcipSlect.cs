using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapcipSlect : MonoBehaviour
{
    [SerializeField]
    public Material[] mat = new Material[3];//変更したいマテリアルをセット
    Material[] mats;

    public bool Onblock = false;//自身のマップチップにブロックが乗っているときにtrue

    public bool Onplayer = false;//自身のマップチップにプレイヤーが乗っているときにtrue

    public bool Onobj = false;   //自身のマップチップにオブジェクトが乗っているときにtrue

    //セレクトON,OFFでマテリアル制御
    private bool now_select;

    BoxCastRayTest script;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
        now_select = false;

        script = GameObject.Find("fps_camera").GetComponent<BoxCastRayTest>();
    }

    // Update is called once per frame
    void Update()
    {
        //クリックの明確な起点がないためUpdateで処理（マウスが外れたときなど）
        //いやならRayの外れた時に呼び出される関数を使うこと
        if (now_select == true && Onplayer==false)
        {
            now_select = false;
            //マップチップの上にブロックが置いてあった場合、マップチップの色を変えない
            if (Onblock==true)
            {
               
            }
            else if(Onobj == true)
            {
                Debug.Log("きも");
                mats[0] = mat[2];
            }
            else
            {
                mats[0] = mat[1];
            }
          
            GetComponent<Renderer>().materials = mats;
        }
        //プレイヤーが足元近くにブロック置けなくする
        else if(now_select == true && Onplayer ==true && script.grab==true)
        {
            now_select = false;
            //マップチップの上にブロックが置いてあった場合、マップチップの色を変えない
            if (Onblock == true)
            {
                Debug.Log("53位");
            }
            else if(Onobj==true)
            {
                Debug.Log("黄色");
                mats[0] = mat[2];
            }
            else
            {
                mats[0] = mat[2];
            }

            GetComponent<Renderer>().materials = mats;
        }

       
        else
        {
            if (mats[0] == mat[1])
            {
                mats[0] = mat[0];

                GetComponent<Renderer>().materials = mats;
            }
            else if (mats[0] == mat[2])
            {
                mats[0] = mat[0];

                GetComponent<Renderer>().materials = mats;
            }
        }

    }

    //プレイヤーがマップチップに侵入してた時、そのマップチップに置けないようにする
    public void OnTriggerStay(Collider other)
    {

        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
           // Debug.Log("夢色キッチン☆");
            Onplayer = true;
        }
        //電線＆電気ブロック
        if (other.CompareTag("Conductor"))
        {
            Onobj = true;
        }
        //色用ブロック
        if (other.gameObject.tag == "ColorInput")
        {
            Onobj = true;
        }
        //色用電線
        if (other.gameObject.tag == "ColorOutput")
        {
            Onobj = true;
        }
        //混色オブジェクト
        if (other.gameObject.tag == "ColorMix")
        {
            Onobj = true;
        }
        ////音用ブロック
        //if (other.gameObject.tag == "MusicInput")
        //{
        //    Onobj = true;
        //}
        //音用電線
        if (other.gameObject.tag == "MusicOutput")
        {
            Onobj = true;
        }
        //音混ぜオブジェクト
        if (other.gameObject.tag == "MusicMix")
        {
            Onobj = true;
        }

        //ライトオブジェクトがマップチップの上にある時、ブロックを置けないようにする
        //（マップチップの上に乗っているときのみ！！！！）
        if (other.CompareTag("Power_Supply"))
        {
          //  Debug.Log("バナナ");
            Onobj = true;
        }

        //ライトオブジェクト(出口？)がマップチップの上にある時、ブロックを置けないようにする
        //（マップチップの上に乗っているときのみ！！！！）
        if (other.CompareTag("Noset"))
        {
            //Debug.Log("夢色キッチン☆");
            Onobj = true;
        }

      
    }

    //プレイヤーが侵入したマップチップを離れたとき、置けない処理解除
    public void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
           // Debug.Log("フォークなのにさじ加減！");
            Onplayer = false;
        }

        //電気ブロックが床から離れたときマップチップの色を元に戻す
        if (other.CompareTag("Conductor"))
        {
            Debug.Log("素敵だね");
            Onobj = false;
        }

        //回転ブロックが床から離れたときマップチップの色を元に戻す
        if (other.CompareTag("Rotate"))
        {
         //   Debug.Log("素敵だね");
            Onobj = false;
        }

        //色用ブロックが床から離れたときマップチップの色を元に戻す
        if (other.gameObject.tag == "ColorInput")
        {
            Onobj = false;
        }

        //音用ブロックが床から離れたときマップチップの色を元に戻す
        if (other.gameObject.tag == "MusicInput")
        {
            Onobj = false;
        }
    }

    

    public void ChangeMaterial()
    {
        now_select = true;
    }
}

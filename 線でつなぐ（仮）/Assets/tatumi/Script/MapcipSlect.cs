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
            mats[0] = mat[1];

            GetComponent<Renderer>().materials = mats;
        }
        //プレイヤーが足元近くにブロック置けなくする
        else if(now_select == true && Onplayer ==true && script.grab==true)
        {
            now_select = false;
            mats[0] = mat[2];

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
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
           // Debug.Log("夢色キッチン☆");
            Onplayer = true;
        }
        //if (other.CompareTag("Noset"))
        //{
        //    Debug.Log("夢色キッチン☆");
        //    Onplayer = true;
        //}
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
    }

    

    public void ChangeMaterial()
    {
        now_select = true;
    }
}

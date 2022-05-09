using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapcipSlect : MonoBehaviour
{
    public Material[] mat = new Material[2];//変更したいマテリアルをセット
    Material[] mats;

    public bool Onblock = false;//自身のマップチップにブロックが乗っているときにtrue

    public bool Onplayer = false;//自身のマップチップにプレイヤーが乗っているときにtrue

    //セレクトON,OFFでマテリアル制御
    private bool now_select;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
        now_select = false;
    }

    // Update is called once per frame
    void Update()
    {
        //クリックの明確な起点がないためUpdateで処理（マウスが外れたときなど）
        //いやならRayの外れた時に呼び出される関数を使うこと
        if (now_select == true)
        {
            now_select = false;
            mats[0] = mat[1];

            GetComponent<Renderer>().materials = mats;
        }
        else
        {
            if (mats[0] == mat[1])
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
            Debug.Log("キッチン☆");
            Onplayer = true;
        }
       
    }

    public void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            Debug.Log("フォークなのにさじ加減！");
            Onplayer = false;
        }

    }

    public void ChangeMaterial()
    {
        now_select = true;
    }
}

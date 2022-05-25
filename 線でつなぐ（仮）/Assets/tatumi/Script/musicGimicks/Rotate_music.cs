using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_music : Base_Color_Script
{

    private const int RIGHT = 0;         //このオブジェクト
    private const int LEFT = 1;     //それ以外のオブジェクト

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    GameObject[] AssistObj = new GameObject[2];

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    private bool[] hit_check = new bool[2];         //アシストOBJ(両端の球)が当たってるかどうかチェックするよう

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string OutColor_name;

    [SerializeField, Header("現在認のSE番号")]
    private int music_num = -1;

    void Start()
    {
        OutColor_name = "OutM&C";
    }

    public void SetCheckRight(bool success)
    {
        hit_check[RIGHT] = success;
    }
    public void SetCheckLeft(bool success)
    {
        hit_check[LEFT] = success;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MusicOutput")
        {
            if (hit_check[RIGHT] == true && hit_check[LEFT] == true)
            {

                //電気が通っているかどうか確認。
                if ((collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == false && energization == true))
                {
                    //当たっているObjの優先度(cnt変数)が0(0ならすでに脱色されてる)でなく、このObjより小さいなら、energizationは途切れてるので色を破棄する。
                    energization = false;
                    music_num = -1;

                }
                else if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true && energization == false)
                {

                    //通電情報なので必ず情報を持ってるとする
                    if (collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num()!=-1)
                        music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                    
                    energization = true;
                   
                }
                
            }
            //アシストobjがほかのRelayColorと接触していなければ脱色する。
            else if (energization == true)
            {
                energization = false;
                music_num = -1;
               
            }
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //電気が通っているかどうか確認。
            if ((collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == false && energization == true))
            {
                //当たっているObjの優先度(cnt変数)が0(0ならすでに脱色されてる)でなく、このObjより小さいなら、energizationは途切れてるので色を破棄する。
                energization = false;
                music_num = -1;

            }
            else if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true && energization == false)
            {

                //接触してるRelayColorのカウントより1つ大きい値を取得する（一方通行にするため）

                energization = true;
                //通電情報なので必ず情報を持ってるとする
                if (collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num() != -1)
                    music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MusicOutput")
        {
            if (energization == true)
            {
                energization = false;
                music_num = -1;
              
            }
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            if (energization == true)
            {
                energization = false;
                music_num = -1;
            }
        }
    }
}

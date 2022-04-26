using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMusic_Script : Base_Color_Script
{
    private int music_num;
    GameObject MixObj;
    void Start()
    {
       
    }

    public void OnCollisionEnter(Collision collision)
    {
        //判定対象更新
        if (collision.gameObject.tag == "ColorMix")
        {
            MixObj = collision.gameObject;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MusicInput")
        {
            //電気は電源なので必ず通るので取得＆自身に代入
            music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
            energization = true;
        }
        else if (collision.gameObject.tag == "MusicOutput")
        {
            //電源がOnの相手のみ作動
            if (collision.gameObject.GetComponent<OutputMusic_Script>().energization == true)
            {
                //電気は電源なので必ず通るので取得＆自身に代入
                music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
                energization = true;
            }
            else
            {
                //OFF場合
                music_num =-1;
                energization = false;
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //抜けたとき電源色番号初期化
        if (collision.gameObject.tag == "ColorInput")
        {
            music_num = -1;//何もなし
            energization = false;
        }
    }
}

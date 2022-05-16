using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Conductor : Base_Enegization
{
    //現在送られtるSEの種類を判別＆特定の物しか通さない処理
    [SerializeField, Header("現在のSE番号と特定のSE番号設定(設定した物しか通さない)")]
    private int music_num,Set_music_num;

    //元からあるやつ詳細は森井君側で
    [SerializeField]
    private bool Change, Input_Hit;
    GameObject MixObj;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string  OutColor_name;

    void Start()
    {
        //色＆音楽のoutput管理
        //InColor_name = "InM&C";
        OutColor_name = "OutM&C";
    }

    void Update()
    {
        //通電状況により色変更
        if (energization == true)
            GetComponent<Renderer>().material.color = new Color32(71, 214, 255, 200);
        else
            GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 200);

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
        //SEのみの設定
       if (collision.gameObject.tag == "MusicOutput")
        {
            //電源がOnの相手のみ作動
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                if (Set_music_num == collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num())
                {
                    //電気は電源なので必ず通るので取得＆自身に代入
                    music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                    Input_Hit = true;
                    energization = true;
                }
            }
            else
            {
                if (Input_Hit == false)
                {
                    //OFF場合
                    music_num = -1;
                    Input_Hit = false;
                    energization = false;
                }
            }
        }
       //音＆色の設定名前で判別(out側)
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //電源がOnの相手のみ作動
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                if (Set_music_num == collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num())
                {
                    //電気は電源なので必ず通るので取得＆自身に代入
                    music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                    Input_Hit = true;
                    energization = true;
                }
            }
            else
            {
                if (Input_Hit == false)
                {
                    //OFF場合
                    music_num = -1;
                    Input_Hit = false;
                    energization = false;
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //抜けたとき電源色番号初期化
        if (collision.gameObject.tag == "MusicOutput")
        {
            music_num = -1;//何もなし
            energization = false;
            Input_Hit = false;
        }
        //音＆色の設定名前で判別(out側)
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            music_num = -1;//何もなし
            energization = false;
            Input_Hit = false;
        }
    }

    //自身の色番号を返す
    public int Remusic_num()
    {
        return music_num;
    }
  
}

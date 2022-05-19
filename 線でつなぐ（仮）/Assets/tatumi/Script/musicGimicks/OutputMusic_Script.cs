using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMusic_Script : Base_Enegization
{
    //自身の色番号
    [SerializeField, Header("現在のSE番号")]
    private int music_num;

    //詳細は森井君側
    [SerializeField]
    private bool Change,Input_Hit;
    GameObject MixObj;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string InColor_name,OutColor_name;

    //色同居判定
    private bool MCmode = false;
    void Start()
    {
        //色のIN.OUTどっちも取得する可能性あり
        InColor_name = "InM&C";
        OutColor_name = "OutM&C";
    }

    void Update()
    {
        //色同居の場合色変更は邪魔
        if (MCmode == false)
        {
            if (energization == true)
                GetComponent<Renderer>().material.color = new Color32(71, 214, 255, 200);
            else
                GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 200);
        }

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
        if (collision.gameObject.tag == "Insulator")
        {
            music_num = -1;//何もなし
            energization = false;
            Input_Hit = false;
        }
        //SE判定のみの場合(In)
        else if (collision.gameObject.tag == "MusicInput")
        {
            if (collision.gameObject.GetComponent<InputMusic_Script>().GetEnergization() == true)
            {
                //取得＆自身に代入
                music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
                energization = true;
                Input_Hit = true;
                MCmode = false;
            }
            else
            {
                //OFF場合
                music_num = -1;
                energization = false;
                Input_Hit = false;
                MCmode = false;
            }
        }
        //SE判定のみの場合(Out)
        else if (collision.gameObject.tag == "MusicOutput")
        {
            //電源がOnの相手のみ作動
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                //取得＆自身に代入
                music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                Input_Hit = true;
                energization = true;
                MCmode = false;
            }
            else if(collision.gameObject.GetComponent<OutputMusic_Script>())
            {
                if (Input_Hit == false)
                {
                    //OFF場合
                    music_num = -1;
                    Input_Hit = false;
                    energization = false;
                    MCmode = false;
                }
            }
        }
        //色も判定の場合(In)
        else if (collision.gameObject.name.Contains(InColor_name) == true)
        {
            if (collision.gameObject.GetComponent<InputMusic_Script>().GetEnergization() == true)
            {
                //取得＆自身に代入
                music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
                energization = true;
                Input_Hit = true;
                MCmode = true;
            }
            else
            {
                //OFF場合
                music_num = -1;
                energization = false;
                Input_Hit = false;
                MCmode = true;
            }
        }
        //色も判定の場合(Out)
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //電源がOnの相手のみ作動
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                //取得＆自身に代入
                music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                Input_Hit = true;
                energization = true;
                MCmode = true;
            }
            else if (collision.gameObject.GetComponent<OutputMusic_Script>())
            {
                if (Input_Hit == false)
                {
                    //OFF場合
                    music_num = -1;
                    Input_Hit = false;
                    energization = false;
                    MCmode = true;
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //抜けたとき電源音番号初期化(SE)
        if (collision.gameObject.tag == "MusicInput")
        {
            music_num = -1;//何もなし
            energization = false;
            Input_Hit = false;
            MCmode = false;
        }
        else if (collision.gameObject.tag == "MusicOutput")
        {
            music_num = -1;//何もなし
            energization = false;
            Input_Hit = false;
            MCmode = false;
        }
        //抜けたとき電源音番号初期化(色付き)
        else if (collision.gameObject.name.Contains(InColor_name) == true)
        {
            music_num = -1;//何もなし
            energization = false;
            Input_Hit = false;
            MCmode = true;
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            music_num = -1;//何もなし
            energization = false;
            Input_Hit = false;
            MCmode = true;
        }
    }

    //かえすぅ
    public int Remusic_num()
    {
        return music_num;
    }
   

}

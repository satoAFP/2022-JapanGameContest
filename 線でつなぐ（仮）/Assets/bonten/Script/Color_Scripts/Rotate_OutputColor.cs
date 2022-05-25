using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_OutputColor : Base_Color_Script
{

    private const int RIGHT = 0;         //このオブジェクト
    private const int LEFT = 1;     //それ以外のオブジェクト

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    GameObject[] AssistObj = new GameObject[2];


    [SerializeField]
    private int cnt = 0;

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    private bool[] hit_check = new bool[2];         //アシストOBJ(両端の球)が当たってるかどうかチェックするよう

    //アクセサー
    public int GetPrecedence()
    {
        return cnt;
    }
    public void SetPrecedence(int num)
    {
        Debug.Log(this.gameObject.transform.parent.name);
        cnt = num;
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
        if (collision.gameObject.tag == "ColorOutput")
        {
            if (hit_check[RIGHT] == true && hit_check[LEFT] == true)
            {
                
                //電気が通っているかどうか確認。
                if ((collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true))
                {
                    //Debug.Log(collision.gameObject.transform.parent.name);
                    //当たっているObjの優先度(cnt変数)が0(0ならすでに脱色されてる)でなく、このObjより小さいなら、energizationは途切れてるので色を破棄する。
                    if (collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
                    {
                        energization = false;
                        //ColorInputから色を取得
                        SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                    }
                }
                else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false)
                {
                    //優先度(cnt変数)が0(0なら脱色されてる)でなく、このObjより小さいならそのObjの色を取得する。
                    if ((collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence()) || cnt == 0)
                    {
                        //接触してるRelayColorのカウントより1つ大きい値を取得する（一方通行にするため）
                        cnt = collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() + 1;
                        energization = true;
                        colorchange_signal = true;
                        //接触してるOutputColor(RelayColor)から色を取得
                        SetColor(collision.gameObject, ADDITION);
                        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                    }
                }
            }
            //アシストobjがほかのRelayColorと接触していなければ脱色する。
            else if (energization == true)
            {
                energization = false;
                //ColorInputから色を取得
                SetColor(color,SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ColorOutput")
        {
            if(energization==true)
            {
                energization = false;
                SetColor(color, SUBTRACTION);
            }
        }
    }
}


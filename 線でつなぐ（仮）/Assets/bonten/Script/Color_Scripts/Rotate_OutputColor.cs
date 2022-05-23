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

    private int cnt = 0;


    //アクセサー
    public int GetPrecedence()
    {
        return cnt;
    }
    public void SetPrecedence(int num)
    {
        cnt = num;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorOutput")
        {
            if (AssistObj[RIGHT].GetComponent<Rotate_AssistColor>().GetHitTarget() && AssistObj[LEFT].GetComponent<Rotate_AssistColor>().GetHitTarget())
            {
                //電気が通っているかどうか確認。
                if ((collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true))
                {
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
                    if ((collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence()))
                    {
                        //接触してるRelayColorのカウントより1つ大きい値を取得する（一方通行にするため）
                        cnt = collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() + 1;
                        energization = true;
                        colorchange_signal = true;
                        //ColorInputから色を取得
                        SetColor(collision.gameObject, ADDITION);
                        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                    }
                }
            }
            //アシストobjがほかのRelayColorと接触していなければ脱色する。
            else if (energization == true)
            {
                Debug.Log("1");
                energization = false;
                //ColorInputから色を取得
                SetColor(color,SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    private int[] my_color = new int[3];
    GameObject MixObj;
    [SerializeField]
    private bool mixObj_hit;
    [SerializeField]
    private int cnt;          // 一方通行のための優先度

    //アクセサー
    public int GetPrecedence()
    {
        return cnt;
    }
    public void SetPrecedence(int num)
    {
        cnt = num;
    }

    private void Start()
    {
        colorchange_signal = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ColorMix")
        {
            MixObj = collision.gameObject;
            mixObj_hit = true;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //ColorInputが離れたり、ColorInputに電気が送られなくなった時の処理
            if (collision.gameObject.GetComponent<Base_Enegization>().GetEnergization() == false && energization == true)
            {
                if (mixObj_hit == true)
                {
                    MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                }
                //下記のMixColorObjを脱色できるようにtrueにする
                energization = false;
                //ColorInputから色を取得
                SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);
            }
        }
        else if (collision.gameObject.tag == "ColorOutput")
        {
            //電気が通っているかどうか確認。
            if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true)
            {
                //当たっているObjの優先度(cnt変数)が0(0ならすでに脱色されてる)でなく、このObjより小さいなら、energizationは途切れてるので色を破棄する。
                if (collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
                {
                    energization = false;
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                    }
                    //ColorInputから色を取得
                    SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                    GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);

                }
            }
            else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false)
            {
                //優先度(cnt変数)が0(0なら脱色されてる)でなく、このObjより小さいならそのObjの色を取得する。
                if ((collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence()))
                {
                    cnt = collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() + 1;
                    energization = true;
                    colorchange_signal = true;
                    //ColorInputから色を取得
                    SetColor(collision.gameObject, ADDITION);
                    GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);

                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                        MixObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColorから色を捨てる
        if (collision.gameObject.tag == "ColorInput")
        {
            Debug.Log("かっとばせー！");
            if (mixObj_hit == true)
            {
                MixObj.GetComponent<MixColor_Script>().Decolorization(color);
            }
            energization = false;
            colorchange_signal = false;
            SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);

        }
        else if (collision.gameObject.tag == "ColorMix")
        {
            collision.gameObject.GetComponent<MixColor_Script>().Decolorization(color);
        }
    }
}

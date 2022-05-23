using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_OutputColor : OutputColor_Script
{
    private const int OWN = 0;         //このオブジェクト
    private const int PARTHER = 1;     //それ以外のオブジェクト


    [NamedArrayAttribute(new string[] { "OWN", "PARTHER" })]
    [SerializeField]
    private bool[] vertical = new bool[2];        //縦か横か向いている方向を記憶しておくための変数。trueで0or180,falseで90or270。

    private void Update()
    {
        //自身の角度を判別
        if (this.gameObject.transform.localEulerAngles.y == 0 || this.gameObject.transform.localEulerAngles.y == -180) vertical[OWN] = true;
        else if (this.gameObject.transform.localEulerAngles.y == 90 || this.gameObject.transform.localEulerAngles.y == -90) vertical[OWN] = false;
    }
    public new void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            energization = true;
            cnt = 1;
            SetColor(collision.gameObject, ADDITION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
            if (this.gameObject.GetComponent<ClickObj>() != null)
            {
                this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
            }
            //自身の着色を行った後に、MixColorObjおよびClear判定Objと接触してるか確認し
            //接触してたら着色する
            if (mixObj_hit)
            {
                MixObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                MixObj.GetComponent<Base_Color_Script>().SetColorChange(true);
            }
            else if (clearObj_hit)
            {
                ClearObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
            }
        }
        else if (collision.gameObject.tag == "ColorMix")
        {
            MixObj = collision.gameObject;
            mixObj_hit = true;
        }
        else if (collision.gameObject.tag == "Power_Supply")
        {
            ClearObj = collision.gameObject;
            clearObj_hit = true;
        }
        else if (collision.gameObject.tag == "Relay")
        {
            if (collision.gameObject.transform.localEulerAngles.y == 0 || collision.gameObject.transform.localEulerAngles.y == -180) vertical[PARTHER] = true;
            else if (collision.gameObject.transform.localEulerAngles.y == 90 || collision.gameObject.transform.localEulerAngles.y == -90) vertical[PARTHER] = false;
        }

    }
    public new void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //ColorInputが離れたり、ColorInputに電気が送られなくなった時の処理
            if (collision.gameObject.GetComponent<Base_Enegization>().GetEnergization() == false && energization == true)
            {
                //自身の脱色を行う前に、MixColorObjおよびClear判定Objと接触してるか確認し
                //接触してたら先に脱色処理を行う
                if (mixObj_hit)
                {
                    MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                }
                else if (clearObj_hit)
                {
                    ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                }
                //下記のMixColorObjを脱色できるようにtrueにする
                energization = false;
                //ColorInputから色を取得
                SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
            }
        }
        else if (collision.gameObject.tag == "ColorOutput")
        {
            //電気が通っているかどうか確認。
            if ((collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true)|| vertical[OWN] != vertical[PARTHER])
            {
                //当たっているObjの優先度(cnt変数)が0(0ならすでに脱色されてる)でなく、このObjより小さいなら、energizationは途切れてるので色を破棄する。
                if (collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
                {

                    energization = false;
                    //自身の脱色を行う前に、MixColorObjおよびClear判定Objと接触してるか確認し
                    //接触してたら先に脱色処理を行う
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                    }
                    else if (clearObj_hit)
                    {
                        ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                        ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                    //ColorInputから色を取得
                    SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                    GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);

                }
            }
            else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false )
            {
                if(vertical[OWN] == vertical[PARTHER])
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

                        //自身の脱色を行った後に、MixColorObjおよびClear判定Objと接触してるか確認し
                        //接触してたら色を入れる
                        if (mixObj_hit)
                        {
                            MixObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                            MixObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                        }
                        else if (clearObj_hit)
                        {
                            ClearObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                            ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                        }
                    }
                }
                else
                {

                    energization = false;
                    //自身の脱色を行う前に、MixColorObjおよびClear判定Objと接触してるか確認し
                    //接触してたら先に脱色処理を行う
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                    }
                    else if (clearObj_hit)
                    {
                        ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                        ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                    //ColorInputから色を取得
                    SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                    GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                }
            }
        }
    }
}

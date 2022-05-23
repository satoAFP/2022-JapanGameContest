using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    protected int[] my_color = new int[3];
    protected GameObject ClearObj;
    protected GameObject MixObj;
    [SerializeField]
    protected bool mixObj_hit;        //色混ぜるobjと接触してるか確認する用の変数
    [SerializeField]
    protected bool clearObj_hit;      //クリア判定のobjと接触してるか確認する要の変数
    [SerializeField]
    protected int cnt;          // 一方通行のための優先度

    [SerializeField]
    GameObject efflight;

    ParticleSystem.MainModule par;

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
        par = GetComponent<ParticleSystem>().main;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ColorInput")
        {
            energization = true;
            cnt = 1;
            SetColor(collision.gameObject, ADDITION);
            efflight.GetComponent<ColorLight_Script1>().SetLight(color);
            if (this.gameObject.GetComponent<ClickObj>()!=null)
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
        if (collision.gameObject.tag == "ColorMix")
        {
            MixObj = collision.gameObject;
            mixObj_hit = true;
        }
        if (collision.gameObject.tag == "Power_Supply")
        {
            ClearObj = collision.gameObject;
            clearObj_hit = true;
        }

    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //ColorInputが離れたり、ColorInputに電気が送られなくなった時の処理
            if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == false && energization == true)
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
                efflight.GetComponent<ColorLight_Script1>().SetLight(color);
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
                    //自身の脱色を行う前に、MixColorObjおよびClear判定Objと接触してるか確認し
                    //接触してたら先に脱色処理を行う
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                    }
                    else if(clearObj_hit)
                    {
                        ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                        ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                    //ColorInputから色を取得
                    SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
                }
            }
            else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false)
            {
                //優先度(cnt変数)が0(0なら脱色されてる)でなく、このObjより小さいならそのObjの色を取得する。
                if ((collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence()))
                {
                    Debug.Log("はいれよ"+this.gameObject.name);
                    //接触してるRelayColorのカウントより1つ大きい値を取得する（一方通行にするため）
                    cnt = collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() + 1;
                    energization = true;
                    colorchange_signal = true;
                    //ColorInputから色を取得
                    SetColor(collision.gameObject, ADDITION);
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
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
        }
        else if(collision.gameObject.tag == "Rotate")
        {
            //電気が通っているかどうか確認。
            if (collision.gameObject.GetComponent<Rotate_OutputColor>().GetEnergization() == false && energization == true)
            {
                //当たっているObjの優先度(cnt変数)が0(0ならすでに脱色されてる)でなく、このObjより小さいなら、energizationは途切れてるので色を破棄する。
                if (collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence())
                {
                    Debug.Log("2");
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
                    SetColor(collision.gameObject.GetComponent<Rotate_OutputColor>().GetColor());
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
                }
            }
            else if (collision.gameObject.GetComponent<Rotate_OutputColor>().GetEnergization() == true && energization == false)
            {
                //優先度(cnt変数)が0(0なら脱色されてる)でなく、このObjより小さいならそのObjの色を取得する。
                if ((collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence()))
                {
                    //接触してるRelayColorのカウントより1つ大きい値を取得する（一方通行にするため）
                    cnt = collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence() + 1;
                    energization = true;
                    colorchange_signal = true;
                    //ColorInputから色を取得
                    SetColor(collision.gameObject, ADDITION);
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
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
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColorから色を捨てる
        if (collision.gameObject.tag == "ColorInput")
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
                ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
            }
            energization = false;
            colorchange_signal = false;
            SetColor(color, SUBTRACTION);
            efflight.GetComponent<ColorLight_Script1>().SetLight(color);

        }
        else if (collision.gameObject.tag == "ColorMix")
        {
            collision.gameObject.GetComponent<MixColor_Script>().Decolorization(color);
        }
        else if(collision.gameObject.tag == "ColorOutput")
        {
            Debug.Log(this.gameObject.name);
            if(cnt<collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
            {
                collision.gameObject.GetComponent<OutputColor_Script>().SetEnergization(false);
                collision.gameObject.GetComponent<OutputColor_Script>().SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor(), SUBTRACTION);
                efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                if (this.gameObject.GetComponent<ClickObj>() != null)
                {
                    this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                }
            }
        }
    }
}

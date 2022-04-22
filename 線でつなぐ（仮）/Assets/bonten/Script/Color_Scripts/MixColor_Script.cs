using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : Base_Color_Script
{
    //子オブジェクト取得用
    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public bool GetColorChange() => colorchange_signal;

    public void OnCollisionEnter(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、その色に変更
        if (collision.gameObject.tag == "ColorOutput")
        {
            color[COLOR_RED]    += collision.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_BLUE]   += collision.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
            color[COLOR_GREEN]  += collision.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            for (int i = 0; i < COLOR_MAX; i++)
            {
                if (color[i] > COLOR_MAXNUM)
                {
                    //色の値の最大値を超えてたらCOLOR_MAXNUM(255)に固定
                    color[i] = COLOR_MAXNUM;
                }
                else if (color[i] < 0) 
                {
                    //色の値の最小値を下回ってたら0にする
                    color[i] = 0;
                }
            }
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);

            colorchange_signal = true;
            child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、設定された値をこのオブジェクトの変数から引くことで脱色
        if (collision.gameObject.tag == "ColorOutput")
        {
            //その後、離れたColorInputが持っている色の値を今これが持ってる色の値から減らす
            color[COLOR_RED]    -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorRed();
            color[COLOR_BLUE]   -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorBlue();
            color[COLOR_GREEN]  -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorGreen();

            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            colorchange_signal = true;
            child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }

    
}

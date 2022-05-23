using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : Base_Color_Script
{

    //子オブジェクト取得用
    [SerializeField]
    private GameObject child;
    private bool decolor = false;

    //脱色処理
    public void Decolorization(int[] decolor)
    {
        //子脱色オブジェクトに色を出す指令を出す
        child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION, decolor);

        color[COLOR_RED] -= decolor[COLOR_RED];
        color[COLOR_GREEN] -= decolor[COLOR_GREEN];
        color[COLOR_BLUE] -= decolor[COLOR_BLUE];


        for (short i = 0; i < COLOR_MAX; i++)
        {
            if (color[i] < 0)
            {
                color[i] = 0;
            }
        }

        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、その色に変更
        if (collision.gameObject.tag == "ColorOutput")
        {
            //ColorOutoputのenergizationがtrueならここに入る
            if (collision.gameObject.GetComponent<Base_Enegization>().GetEnergization() && colorchange_signal)
            {
                colorchange_signal = false;

                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                //子供objに色を入力する信号を出す
                child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、設定された値をこのオブジェクトの変数から引くことで脱色
        if (collision.gameObject.tag == "ColorOutput")
        {
            //その後、離れたColorInputが持っている色の値を今これが持ってる色の値から減らす
            SetColor(collision.gameObject, SUBTRACTION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
            //子オブジェクトに色を消す指令を出す
            child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }
}

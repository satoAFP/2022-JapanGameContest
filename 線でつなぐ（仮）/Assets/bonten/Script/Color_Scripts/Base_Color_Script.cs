using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//色の基盤スクリプト
public class Base_Color_Script : MonoBehaviour
{
    //定数-------------------------------------------------------
    //色の値の最大値
    protected const int COLOR_MAXNUM = 255;
    //カラーの数
    protected const int COLOR_RED = 0;
    protected const int COLOR_GREEN = 1;
    protected const int COLOR_BLUE = 2;
    protected const int COLOR_MAX = 3;

    protected const bool ADDITION = true;               //加算
    protected const bool SUBTRACTION = false;           //減算

    //-----------------------------------------------------------

    [SerializeField]
    protected bool colorchange_signal = false;

    //要素の名前を変更
    [NamedArrayAttribute(new string[] { "RED", "BRUE", "GREEN" })]
    [SerializeField]
    protected int[] color = new int[COLOR_MAX];

    //ゲームオブジェクトを引数として持ち、そのオブジェクトの色を自身の色に影響させる関数
    //引数1 -> ゲームオブジェクト型の引数
    //引数2 -> 加算代入(true)か、減算代入(false)かきめる
    public void SetColor(GameObject obj, bool col)
    {
        if(col == ADDITION)
        {
            color[COLOR_RED]   += obj.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_GREEN] += obj.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            color[COLOR_BLUE]  += obj.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
        }
        else if(col == SUBTRACTION)
        {
            color[COLOR_RED]   -= obj.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_GREEN] -= obj.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            color[COLOR_BLUE] -= obj.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
        }
    }


    public void SetColorChange(bool change) => colorchange_signal = change;

    //色のセッター
    public void SetColorRed(int red) => color[COLOR_RED] = red;
    public void SetColorBlue(int brue) => color[COLOR_BLUE] = brue;
    public void SetColorGreen(int green) => color[COLOR_GREEN] = green;

    //色のゲッター。引数で要素数を指定して取得

    public int GetColorRed() => color[COLOR_RED];

    public int GetColorBlue() => color[COLOR_BLUE];

    public int GetColorGreen() => color[COLOR_GREEN];
}

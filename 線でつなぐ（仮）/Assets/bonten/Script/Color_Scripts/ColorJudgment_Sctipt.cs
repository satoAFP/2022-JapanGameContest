using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorJudgment_Sctipt : Base_Color_Script
{
    //定数-----------------------------------------------------------------------------


    //---------------------------------------------------------------------------------


    //ゲームクリアとなるカラー
    [NamedArrayAttribute(new string[] { "RED", "GREEN", "BRUE" })]
    [SerializeField]
    private int[] clearColor = new int[COLOR_MAX];

    //光源系の子オブジェクトを取得
    [SerializeField]
    private GameObject Liting;         //こっちが光源のほうのobj
    [SerializeField]
    private GameObject Nonliting;        //こっちが光源じゃないほうのobj

    [SerializeField]
    private bool MCmode;

    // Update is called once per frame
    void Update()
    {
        //色変更の信号を受け取ると
        if (colorchange_signal == true)
        {
            //色変更をする
            Liting.GetComponent<Base_Color_Script>().SetColorChange(true);
            Liting.GetComponent<Base_Color_Script>().SetColor(color);

            //前職が0なら消灯
            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE] == 0)
            {
                //通電ならOFFる
                if (Liting.activeSelf == true)
                {
                    Liting.SetActive(false);
                    Nonliting.SetActive(true);


                    this.gameObject.GetComponent<TurnonPower_Script>().else_Switch_off();
                }
            }
          
            colorchange_signal = false;
        }

        if (MCmode == false)
        {
            if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
            {
                //通電ならONる
                if (Liting.activeSelf == false)
                {
                    Liting.SetActive(true);
                    Nonliting.SetActive(false);
                    this.gameObject.GetComponent<TurnonPower_Script>().else_Switch_on();
                }
            }
        }
        else
        {
            if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
            {
                //ここにクリアの証的なコード
                energization = true;
            }
            else
            {
                energization = false;
            }
        }
    }
}

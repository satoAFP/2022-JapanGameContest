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
    private GameObject OnLight;         //こっちが光源のほうのobj
    [SerializeField]
    private GameObject OffLight;        //こっちが光源じゃないほうのobj

    [SerializeField]
    private bool MCmode;

    private void Start()
    {
        //光源部分のオブジェクトを取得
        //OnLight = transform.Find("OnLight").gameObject;
        //OffLight = transform.Find("OffLight").gameObject;
        OffLight.GetComponent<Renderer>().material.color=new Color32((byte)(clearColor[COLOR_RED]/5), (byte)(clearColor[COLOR_GREEN]/5), (byte)(clearColor[COLOR_BLUE]/5), 255);
    }

    // Update is called once per frame
    void Update()
    {
        //色変更の信号を受け取ると
        if (colorchange_signal == true)
        {
            //色変更をする
            OnLight.GetComponent<Base_Color_Script>().SetColor(color);
            OnLight.GetComponent<Base_Color_Script>().SetColorChange(true);

            //前職が0なら消灯
            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE] == 0)
            {
                OnLight.SetActive(false);
                OffLight.SetActive(true);
            }
            else
            {
                OnLight.SetActive(true);
                OffLight.SetActive(false);
            }

            colorchange_signal = false;
        }

        if (MCmode == false)
        {
            if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
            {
                Debug.Log("くりあー");
                //ここにクリアの証的なコード
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

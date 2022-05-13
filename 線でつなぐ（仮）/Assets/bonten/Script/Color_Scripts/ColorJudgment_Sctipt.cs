using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorJudgment_Sctipt : Base_Color_Script
{
    //定数-----------------------------------------------------------------------------
    

    //---------------------------------------------------------------------------------


    //ゲームクリアとなるカラー
    [NamedArrayAttribute(new string[] { "RED",  "GREEN", "BRUE" })]
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

        OnLight = transform.Find("OnLight").gameObject;
        OffLight = transform.Find("OffLight").gameObject;

        Debug.Log(OnLight.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(colorchange_signal==true)
        {
            Debug.Log("ホヤあそばせ");
            
            OnLight.SetActive(true);
            OffLight.SetActive(false);
            OnLight.GetComponent<Base_Color_Script>().SetColor(color);
            OnLight.GetComponent<Base_Color_Script>().SetColorChange(true);

            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE] == 0)
            {
                OnLight.SetActive(false);
                OffLight.SetActive(true);
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

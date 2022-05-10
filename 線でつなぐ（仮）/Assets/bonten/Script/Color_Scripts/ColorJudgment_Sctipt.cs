using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorJudgment_Sctipt : Base_Color_Script
{
    //ゲームクリアとなるカラー
    [NamedArrayAttribute(new string[] { "RED",  "GREEN", "BRUE" })]
    [SerializeField]
    private int[] clearColor = new int[COLOR_MAX];

    [SerializeField]
    private GameObject[] objs;

    GameObject OffLight;
    GameObject OnLight;

    private void Start()
    {
        OffLight = transform.Find("OffLight").gameObject;
        OnLight = transform.Find("OnLight").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //カラーチェンジの信号が送られたら色を変える。
        if(colorchange_signal==true)
        {
            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE]==0)
            {
                //光源Objを非Activeにし、非光源objをActiveにする
                OffLight.SetActive(true);
                OnLight.SetActive(false);
            }
            else
            {
                //光源ObjをActiveにし、非光源objを非Activeにする
                OffLight.SetActive(false);
                OnLight.SetActive(true);
                //光源Objに色を入力
                OnLight.GetComponent<Base_Color_Script>().SetColor(color);
               
                colorchange_signal = false;
            }
            
        }

        if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
        {
            //ここにクリアの証的なコード
            objs[0].SetActive(false);
            objs[1].SetActive(true);
        }
    }


}

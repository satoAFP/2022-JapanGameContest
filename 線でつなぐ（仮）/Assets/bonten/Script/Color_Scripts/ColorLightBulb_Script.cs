using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLightBulb_Script : Base_Color_Script
{
    //子オブジェクト取得用
    [SerializeField]
    private GameObject PointLight;

    // Update is called once per frame
    void Update()
    {
        if (colorchange_signal)
        {
            //色(emiison)を設定
            this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)255));
          
            //ポイントライトに色を入力
            PointLight.GetComponent<Base_Color_Script>().SetColor(color);
            PointLight.gameObject.GetComponent<Light>().color = new Color((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE]);
            colorchange_signal = false;
        }
    }
}

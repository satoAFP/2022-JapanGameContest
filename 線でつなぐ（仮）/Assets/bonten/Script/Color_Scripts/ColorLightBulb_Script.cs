using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLightBulb_Script : Base_Color_Script
{
    //子オブジェクト取得用
    private GameObject PointLight;

    // Start is called before the first frame update
    void Start()
    {
        //ポイントライトobjを取得
        PointLight = transform.Find("Point Light").gameObject;
        //色を設定
        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
        //ポイントライトに色を入力
        PointLight.GetComponent<Base_Color_Script>().SetColor(color);
        PointLight.gameObject.GetComponent<Light>().color = new Color((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

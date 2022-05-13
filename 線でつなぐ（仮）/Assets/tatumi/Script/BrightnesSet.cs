using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnesSet : MonoBehaviour
{
    //画像（明るさ設定用の白色を入れる）
    [SerializeField]
    Image image;

    Slider m_Slider;//音量調整用スライダー

    void Awake()
    {
        //取得
        m_Slider = GetComponent<Slider>();
        
    }

    void Update()
    {
        //スライダーから現在の値を透明度に反映
        float alpha = m_Slider.value*255.0f;

        image.color = new Color32(0, 0, 0, (byte)alpha);
    }
}

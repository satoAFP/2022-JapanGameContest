using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnesSet : MonoBehaviour
{
    [SerializeField]
    Image image;

    Slider m_Slider;//音量調整用スライダー

    void Awake()
    {
        m_Slider = GetComponent<Slider>();
        
    }

    void Update()
    {
        float alpha = m_Slider.value*255.0f;

        image.color = new Color32(0, 0, 0, (byte)alpha);
    }
}

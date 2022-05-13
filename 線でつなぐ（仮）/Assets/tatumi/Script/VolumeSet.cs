using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//スライダーいじるときあってもいい？
[RequireComponent(typeof(Slider))]
public class VolumeSet : MonoBehaviour
{
    Slider m_Slider;//音量調整用スライダー

    void Awake()
    {
        //スライダー取得
        m_Slider = GetComponent<Slider>();
        m_Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        //音量マスター的なのを取得、反映
        m_Slider.value = AudioListener.volume;
        //スライダーの値が変更されたら音量も変更する
        m_Slider.onValueChanged.AddListener((sliderValue) => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        //かーえて
        m_Slider.onValueChanged.RemoveAllListeners();
    }
}

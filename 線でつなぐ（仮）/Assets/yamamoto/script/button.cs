using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class button : MonoBehaviour, IPointerClickHandler
{

    public bool botton_flag;//ボタンクリックフラグ

    // Start is called before the first frame update
    void Start()
    {
        botton_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // クリックされたときに呼び出されるメソッド
    public void OnPointerClick(PointerEventData eventData)
    {
        print($"オブジェクト {name} がクリックされたよ！");
        // 赤色に変更する
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        //botton_flag = true;//クリックフラグをtrue
    }
}

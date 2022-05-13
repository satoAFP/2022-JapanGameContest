using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coloer : MonoBehaviour
{
    //現在の色値を確認＆上がり下がり切り替え用フラグ
    public int a=0;
    private bool A;

    //画像の色合いを変更
    [SerializeField]
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //色の上下の変更
        if (a < 1)
        {
            A = true;
        }
        else if (a > 255)
        {
            A = false;
        }
        
        //実際に反映
        if (A == true)
            a++;
        else
            a--;

      //代入
        image.color = new Color32(255,255,255,(byte) a);
        
    }
}

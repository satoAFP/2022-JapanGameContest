using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : Base_Color_Script
{
    
    //子オブジェクト取得用
    private GameObject child;

    

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトを取得
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public bool GetColorChange() => colorchange_signal;

    public void OnCollisionStay(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、その色に変更
        if (collision.gameObject.tag == "ColorOutput")
        {
            if (collision.gameObject.GetComponent<Base_Color_Script>().GetEnergization() == true)
            {
                if (colorchange_signal == false)
                {
                    SetColor(collision.gameObject, ADDITION);
                  //GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);

                    //子オブジェクトに色を出す指令を出す
                    child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
                }
            }
            else
            {
                SetColor(collision.gameObject, ADDITION);
               // GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);

                //子オブジェクトに色を消す指令を出す
                child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、設定された値をこのオブジェクトの変数から引くことで脱色
        if (collision.gameObject.tag == "ColorOutput")
        {
            //その後、離れたColorInputが持っている色の値を今これが持ってる色の値から減らす
            SetColor(collision.gameObject, SUBTRACTION);
          //  GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            //子オブジェクトに色を消す指令を出す
            child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }

    
}

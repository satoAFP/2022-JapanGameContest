using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//設定された色に変えるだけ
public class InputColor_Script : Base_Color_Script
{

    // Start is called before the first frame update
    public void Start()
    {
        if(energization == true) GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag=="ColorOutput")
        {
            if (energization == true && collision.gameObject.GetComponent<Base_Enegization>().GetEnergization() == false)
            {
                collision.gameObject.GetComponent<Base_Enegization>().SetEnergization(true);
                //優先度を1(最優先)に設定
                collision.gameObject.GetComponent<OutputColor_Script>().SetPrecedence(1);
                //ColorInputから色を取得
                collision.gameObject.GetComponent<Base_Color_Script>().SetColor(this.gameObject, ADDITION);
                collision.gameObject.GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            }
        }
    }
}

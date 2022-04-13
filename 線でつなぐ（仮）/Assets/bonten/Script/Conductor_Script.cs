using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    //通電変数がtrueになるかどうか確認する関数
    protected void Confimation_Energi()
    {
        if(insulator_hit==false)
        {
            //電源と接触してるとき
            if (power_supply_hit == true)
            {
                //電気が通る
                energization = true;
            }
            //導体と接触してるとき
            else if (energi_investigate == true)
            {
                //電気が通る
                energization = true;
            }
            else
            {
                energization = false;
            }
        }
        else
        {
            energization = false;
        }
    }

    public bool counductor_hit = false;        //導体と当たったか
    public bool insulator_hit = false;         //絶縁体と当たったか
    public bool power_supply_hit = false;      //電源と当たったか
    public bool energization = false;          //通電してるか

    private bool energi_investigate = false;

    public void Energi_On(bool energi)
    {
        energization = energi;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Confimation_Energi();


        if (energi_investigate == false)
        {
            energization = false;
        }
        if (energization == true)
        {
            //オブジェクトの色をシアンにする
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (energization == false) 
        {
            //オブジェクトの色をグレーにする
            GetComponent<Renderer>().material.color = Color.gray;
        }

        
    }

    //ここで触れてるオブジェクトをリストにぶち込む
    void OnCollisionStay(Collision c)
    {
        //if(/*電源ついたよ*/true)
        //{

        //}

        //電源と接触してるとき
        if (c.gameObject.tag == "Power_Supply")
        {
            //power_supply_hitをtrueにする
            power_supply_hit = true;
            energi_investigate = true;
        }
        //絶縁体と接触してるとき
        else if (c.gameObject.tag == "Insulator")
        {
            //insulator_hitをtrueにする
            insulator_hit = true;
            energi_investigate = false;
        }
        //導体と接触してるとき!
        else if (c.gameObject.tag == "Conductor")
        {
            //つながってる導体のenergization(通電確認用変数)で初期化
            energi_investigate = c.gameObject.GetComponent<Conductor_Script>().energization;
            //電気がついてないとき
            //if (energization==false)
            //{
            
                
            //}
                
        }
    }

    //離れたら離れたオブジェクトのリストを削除
    void OnCollisionExit(Collision c)
    {
        //電源と離れたとき
        if (c.gameObject.tag == "Power_Supply")
        {
            //power_supply_hitをtrueにする
            power_supply_hit = false;
        }
        //電線と離れたとき
        else if (c.gameObject.tag == "Insulator")
        {
            //insulator_hitをfalseにする
            insulator_hit = false;
        }
        //電源と接触せずに導体と離れたとき
        else if (c.gameObject.tag == "Conductor")
        {
            //電気ついてるかの確認用変数をfalseにする
            energi_investigate = false;
        }
    }
}

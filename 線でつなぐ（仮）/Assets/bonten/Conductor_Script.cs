using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected
    //通電変数がtrueになるかどうか確認する関数
    void Confimation_Energi()
    {
        //電源と接触してるとき
        if (power_supply_hit == true)
        {
            //電気が通る
            energization = true;
        }
        //導体と接触してるとき
        else if (counductor_hit == true)
        {

            //絶縁体と接触してるとき
            if (insulator_hit == true)
            {
                //電気が通らなくなる
                energization = false;
            }
            else
            {
                //電気が通る
                energization = true;
            }
        }
    }

    public

    bool counductor_hit = false;        //導体と当たったか
    bool insulator_hit = false;         //絶縁体と当たったか
    bool power_supply_hit = false;      //電源と当たったか
    bool energization = false;          //通電してるか

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Confimation_Energi();

        if (energization == true)
        {
            //オブジェクトの色をシアンにする
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            //オブジェクトの色をグレーにする
            GetComponent<Renderer>().material.color = Color.gray;
        }


    }

    //ここで触れてるオブジェクトをリストにぶち込む
    void OnCollisionEnter(Collision c)
    {
        //電源と接触してるとき
        if (c.gameObject.tag == "Power_Supply")
        {
            //power_supply_hitをtrueにする
            power_supply_hit = true;
        }
        //電線と接触してるとき
        else if (c.gameObject.tag == "Insulator")
        {
            //insulator_hitをtrueにする
            insulator_hit = true;
        }
        //絶縁体と接触してるとき!
        else if (c.gameObject.tag == "Conductor")
        {
            
            bool energi_investigate = c.gameObject.GetComponent<Conductor_Script>().energization;

            //つながっている導体のenergizasionがtrueならこのobjのcounductorhitもtrueにする
            if (energi_investigate == true)
            {
                Debug.Log("true");
                //counductor_hitをtrueにする
                counductor_hit = true;
            }
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
            //counductor_hit
            //counductor_hit = false;
        }
    }
}

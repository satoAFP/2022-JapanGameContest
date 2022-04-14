using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    //通電変数がtrueになるかどうか確認する関数
    public bool energization  = false;          //通電してるか
    public bool Conductor_hit = false;          //導体と接触してるか
    public bool Insulator_hit = false;          //絶縁体と接触してるか
    public bool Power_hit     = false;          //電源と接触してるか
    public bool hitting_insulator = false;      //周りに自分が絶縁体と接触していることを伝えるための変数
    public bool hitting_Conductor = false;      //接触していたconductorと離れたことを伝えるための変数
    public int priority;//自らの優先順位
    public int[] prioritys;//相手の優先順位取得用

    public bool oneshot;//つながり先が一つしかない場合
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oneshot == false)
        {
            if (prioritys[0] > priority && prioritys[1] > priority)
            {
                hitting_insulator = false;
            }
            else if (prioritys[0] > prioritys[1])
            {
                hitting_insulator = false;
            }
            else
            {
                hitting_insulator = true;
            }

            if (Insulator_hit == true)
            {
                hitting_insulator = true;
            }
        }

        if (hitting_insulator == true && Power_hit == false) 
        {
            energization = false;
        }
        else if(hitting_insulator == false && Conductor_hit==true)
        {
            energization = true;
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

    private void OnCollisionEnter(Collision c)
    {
        //電源と接触したとき
        if (c.gameObject.tag == "Power_Supply")
        {
            //Power_hitをtrueにする
            Power_hit = true;
            energization = true;
           
        }
        //絶縁体と接触したとき
        else if(c.gameObject.tag=="Insulator")
        {
            //Insulator_hitをtrueにする
            Insulator_hit = true;
        }
    }

    //離れたら離れたオブジェクトのリストを削除
    void OnCollisionExit(Collision c)
    {
        
        //電源と離れたとき
        if (c.gameObject.tag == "Power_Supply")
        {
            //
            energization = false;
        }
        //絶縁体と離れたとき
        if (c.gameObject.tag == "Insulator")
        {
            if (Conductor_hit == true)
            {
                energization = true;
            }
            Insulator_hit = false;
        }
        //電源と接触せずに導体と離れたとき
        if (c.gameObject.tag == "Conductor")
        {
            //電気ついてるかの確認用変数をfalseにする
            Conductor_hit = false;
            if(Power_hit==false)
            {
                energization = false;
            }
          
        }
    }
    
    //ここで触れてるオブジェクトをリストにぶち込む
    void OnCollisionStay(Collision c)
    {
        
        //絶縁体と接触してるとき
        if (c.gameObject.tag == "Insulator")
        {
            //insulator_hitをtrueにする
            energization = false;
            hitting_insulator = true;
        }
        if (c.gameObject.tag == "Conductor")
        {

            if(Conductor_hit==false)
            {
                Conductor_hit = true;
              
              
            }
            


            
            if (Insulator_hit ==true)
            {
                //通電状態をfalseにし、自身が絶縁体と接触していることを周りの導体に伝える
                energization = false;
               
            }
            
            if (hitting_insulator == true)
            {
                if (energization == false)
                {
                    
                    if(c.gameObject.GetComponent<Conductor_Script>().oneshot==true)
                    {
                        c.gameObject.GetComponent<Conductor_Script>().hitting_insulator = hitting_insulator;
                        Debug.Log("i");
                    }
                    else
                    {
                        c.gameObject.GetComponent<Conductor_Script>().prioritys[0] = priority;
                    }

                }
            }
            else if (hitting_insulator == false)
            {
                if (energization == true)
                {
                    if (c.gameObject.GetComponent<Conductor_Script>().oneshot == true)
                    {
                        c.gameObject.GetComponent<Conductor_Script>().hitting_insulator = hitting_insulator;
                        Debug.Log("a");
                    }
                    else
                    {
                        c.gameObject.GetComponent<Conductor_Script>().prioritys[1] = priority;
                    }
                }
            }

        }
    }
}

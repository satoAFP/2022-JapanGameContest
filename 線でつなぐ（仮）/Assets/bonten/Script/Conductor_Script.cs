using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected static int electoric_power = 1;   //1以上で通電
    protected enum Contact
    {
        CONTACT,
        GIVING_POWER,

        MAX_SIZE
    }
    protected void Give_Power_ReSet()
    {
        energization = false;
        power_gave = false;
        giving_conductor = 0;
    }

    //通電変数がtrueになるかどうか確認する関数
    public bool energization  = false;          //通電してるか
    public bool Conductor_hit = false;          //導体と接触してるか
    public bool Insulator_hit = false;          //絶縁体と接触してるか
    public bool Power_hit     = false;          //電源と接触してるか
    public bool hitting_insulator = false;      //周りに自分が絶縁体と接触していることを伝えるための変数
    public bool leaving_Conductor = false;      //接触していたconductorと離れたことを伝えるための変数
   // public bool turn_of_energi = false;         //自身が通電したことを伝えるための変数
    public bool power_gave = false;
    public int power_cnt = 0;                   //電源から何個目の導体かをカウント。小さくなるほど強くなる
    public int leaving_power = 0;               //導体が離れた時に離れた導体の値を受け取る変数
    public int contacing_conductor = 0;         //接触している導体の数
    public int giving_conductor = 0;            //電気を分け与えた導体の数

    //電力の優先度、数小さい程電源に近いので優先する
    public void Set_Power(int set_p)
    {
        if ((set_p < power_cnt || power_cnt == 0) && energization == false) 
        {
            Debug.Log(set_p);
            power_cnt = set_p;
        }
    }

    //絶縁体の処理。セット元より自分のパワーが小さければ絶縁されない
    public void Set_insulator(bool set_insul,int pow)
    {
        if (pow < power_cnt)
        {
            Debug.Log(set_insul);
            hitting_insulator = set_insul;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((hitting_insulator == true && Power_hit == false) || (Insulator_hit == true && Power_hit == false))
        {
            Give_Power_ReSet();
        }
        else if (power_cnt >= electoric_power && (Conductor_hit == true || Power_hit == true)) 
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
            //電源から伸びてる導体は最小に設定し、最優先とする
            power_cnt = 1;
        }
        //絶縁体と接触したとき
        else if(c.gameObject.tag=="Insulator")
        {
            //Insulator_hitをtrueにする
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor")
        {
            contacing_conductor++;
            if(energization==true)
            {
                Give_Power_ReSet();
            }
        }
    }

    //他の特定のオブジェクトが離れた時の処理
    void OnCollisionExit(Collision c)
    {
        
        //電源と離れたとき
        if (c.gameObject.tag == "Power_Supply")
        {
            //
            energization = false;
            Power_hit = false;
        }
        //絶縁体と離れたとき
        else if (c.gameObject.tag == "Insulator")
        {
            Insulator_hit = false;
            Give_Power_ReSet();
        }
        //電源と接触せずに導体と離れたとき
        else if (c.gameObject.tag == "Conductor")
        {
            //電気ついてるかの確認用変数をfalseにする
            Conductor_hit = false;
            if(Power_hit==false)
            {
                c.gameObject.GetComponent<Conductor_Script>().leaving_power = power_cnt;
                power_cnt = 0;
            }
            c.gameObject.GetComponent<Conductor_Script>().leaving_Conductor = true;
        }
    }
    
    //ここで触れてるオブジェクトをリストにぶち込む
    void OnCollisionStay(Collision c)
    {
        
        if (c.gameObject.tag == "Conductor")
        {

            if(Conductor_hit==false)
            {
                Conductor_hit = true;
            }
            if (Insulator_hit == true || hitting_insulator == true)
            {
                c.gameObject.GetComponent<Conductor_Script>().Set_insulator(true, power_cnt);
            }
            else if (energization == true && power_gave == false) 
            {
                //自分が通電状態にある時、周りの接触している導体も通電状態にする
                if(giving_conductor < contacing_conductor)
                {
                    c.gameObject.GetComponent<Conductor_Script>().Set_Power(power_cnt + 1);
                    giving_conductor++;
                    
                }
                else
                {
                    power_gave = true;
                }
                c.gameObject.GetComponent<Conductor_Script>().Set_insulator(false, power_cnt);
            }
            
        }
    }
}

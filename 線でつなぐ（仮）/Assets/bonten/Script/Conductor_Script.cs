using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    private const int ELECTORIC_POWER = 1;   //1以上で通電

    [SerializeField]
    //通電変数がtrueになるかどうか確認する関数
    private bool energization = false;          //通電してるか
    [SerializeField]
    private bool Conductor_hit = false;          //導体と接触してるか
    [SerializeField]
    private bool Insulator_hit = false;          //絶縁体と接触してるか
    [SerializeField]
    private bool Power_hit = false;          //電源と接触してるか
    [SerializeField]
    private bool hitting_insulator = false;      //周りに自分が絶縁体と接触していることを伝えるための変数
    [SerializeField]
    private bool leaving_Conductor = false;      //接触していたconductorと離れたことを伝えるための変数
    [SerializeField]
    private bool energi_check = false;         //自身が通電したことをチェックための変数
    [SerializeField]
    private bool power_gave = false;             //自分が接触してる導体すべてに電気を通せたか確認するための変数
    [SerializeField]
    private int power_save = 0;                  //パワーが0になってもこのオブジェクトの持ってたパワーがもともとどれぐらいだったかを保存しておくための変数
    [SerializeField]
    private int power_cnt = 0;                   //電源から何個目の導体かをカウント。小さくなるほど強くなる
    [SerializeField]
    private int contacing_conductor = 0;         //接触している導体の数
    [SerializeField]
    private int giving_conductor = 0;            //電気を分け与えた導体の数

    //energizationのセッター。
    public bool GetEnergization()
    {
        return energization;
    }
    public void SetEnergization(bool electoric)
    {
        energization = electoric;

    }
    public void GivePowerReSet()
    {
        energization = false;
        power_gave = false;
        giving_conductor = 0;
    }
    //電力の優先度、数小さい程電源に近いので優先する
    //set_p→自身のpower_cnt
    public void SetPower(int set_p)
    {
        //set_pにはこのメソッドを起動したオブジェクトのpower_cntが入り、
        //それがこのオブジェクトのpower_cntより小さければ代入する
        if ((set_p < power_cnt || power_cnt == 0) && energization == false)
        {
            power_cnt = set_p;
        }
    }

    //自身のpower_cntが0になったときに周りも0にするためだけのメソッド
    public void SetPower(int set_p, int pow)
    {
        //上のメソッドのset_pの役割を変数powで代用する。
        if (power_cnt > pow)
        {
            //通電状態ではなくなるので通電している証となる変数を初期化する
            GivePowerReSet();
            power_save = power_cnt;
            power_cnt = set_p;
        }
    }

    //タグPower_Supplyオブジェクトからアクセスするためのメソッド
    public void SetPower(int set_p, bool turn_on)
    {
        power_cnt = set_p;
        Power_hit = turn_on;
    }

    //絶縁体の処理。セット元より自分のパワーが小さければ絶縁されない
    public void SetInsulator(bool set_insul, int pow)
    {
        if (pow < power_cnt && pow != 0) 
        {
            hitting_insulator = set_insul;
        }
    }

    //導体と離れた時の処理
    public void SetLeave(bool leave, int pow)
    {
        if (pow < power_cnt)
        {
            power_save = power_cnt;
            leaving_Conductor = leave;
            Conductor_hit = false;
            //このオブジェクトのpower_cntが0になったことにより
            //このオブジェクトと隣接しているオブジェクトも電力を失うかどうかを確認するため、trueにする
            energi_check = true;
        }
    }

    IEnumerator EffExit()
    {
        for(int i=0;i < 2;i++)
        {
            if(i==0)
            {
                yield return 0.1f;
            }
            else if(i==1)
            {
                PowerOn(power_save);
            }
        }
        
    }

    public void PowerOn(int pow)
    {
        power_cnt = pow;
    }

    public void PowerOff()
    {
        GivePowerReSet();
        power_save = power_cnt;
        power_cnt = 0;
        StartCoroutine(EffExit());
        energization = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((hitting_insulator == true && Power_hit == false) || (Insulator_hit == true && Power_hit == false) || (leaving_Conductor == true && Power_hit == false))
        {
            GivePowerReSet();
            if (leaving_Conductor == true)
            {
                power_cnt = 0;
            }


            leaving_Conductor = false;
        }
        else if (power_cnt >= ELECTORIC_POWER && (Conductor_hit == true || Power_hit == true))
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
            ////Power_hitをtrueにする
            //Power_hit = true;
            ////電源から伸びてる導体は最小に設定し、最優先とする
            //power_cnt = 1;
        }
        //絶縁体と接触したとき
        else if (c.gameObject.tag == "Insulator")
        {
            //Insulator_hitをtrueにする
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor")
        {
            //導体に触れたら、現時点でどれだけの導体と接触しているかカウントする
            contacing_conductor++;

            //新しく導体に触れたら、giving_conductor,power_gave,energizationいったんリセットする
            if (energization == true)
            {
                GivePowerReSet();
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
            GivePowerReSet();
        }
        //電源と接触せずに導体と離れたとき
        else if (c.gameObject.tag == "Conductor")
        {
            //電気ついてるかの確認用変数をfalseにする
            Conductor_hit = false;
            c.gameObject.GetComponent<Conductor_Script>().SetLeave(true, power_cnt);
        }
    }


    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Conductor")
        {
            if (Conductor_hit == false)
            {
                Conductor_hit = true;
            }
            if (Insulator_hit == true || hitting_insulator == true)
            {
                c.gameObject.GetComponent<Conductor_Script>().SetInsulator(true, power_cnt);
            }
            else if (energization == true && power_gave == false)
            {
                
                //自分が通電状態にある時、周りの接触している導体も通電状態にする
                if (giving_conductor < contacing_conductor)
                {
                    //周りの導体のpower_cntには自身のpower_cntより1多い数を代入して差別化を図る
                    c.gameObject.GetComponent<Conductor_Script>().SetPower(power_cnt + 1);
                    giving_conductor++;
                }
                else
                {
                    power_gave = true;
                }
                c.gameObject.GetComponent<Conductor_Script>().SetInsulator(false, power_cnt);
            }
            //このオブジェクトのパワーが0になったことにより、隣のオブジェクトもパワーが0になるかどうか確認する
            if (power_cnt == 0 && energi_check == true)
            {

                //隣のオブジェクトのパワーの大きさがこのオブジェクトより小さければ、そのオブジェクトは絶縁されない
                c.gameObject.GetComponent<Conductor_Script>().SetPower(0, power_save);
                giving_conductor++;
                if (giving_conductor == contacing_conductor)
                {
                    energi_check = false;
                    giving_conductor = 0;
                }
            }

        }
    }
}

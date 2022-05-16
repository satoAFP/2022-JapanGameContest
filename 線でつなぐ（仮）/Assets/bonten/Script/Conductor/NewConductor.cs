using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewConductor : Base_Enegization
{
    protected const int ELECTORIC_POWER = 1;   //1以上で通電

    [SerializeField]
    protected bool Conductor_hit = false;          //導体と接触してるか
    [SerializeField]
    protected bool Insulator_hit = false;          //絶縁体と接触してるか
    [SerializeField]
    protected bool Power_hit = false;          //電源と接触してるか
    [SerializeField]
    protected bool hitting_insulator = false;      //周りに自分が絶縁体と接触していることを伝えるための変数
    [SerializeField]
    protected bool leaving_Conductor = false;      //接触していたconductorと離れたことを伝えるための変数
    [SerializeField]
    protected bool energi_check = false;         //自身が通電したことをチェックための変数
    [SerializeField]
    protected bool power_gave = false;             //自分が接触してる導体すべてに電気を通せたか確認するための変数
    [SerializeField]
    protected int power_save = 0;                  //パワーが0になってもこのオブジェクトの持ってたパワーがもともとどれぐらいだったかを保存しておくための変数
    [SerializeField]
    protected int power_cnt = 0;                   //電源から何個目の導体かをカウント。小さくなるほど強くなる
    [SerializeField]
    protected int contacing_conductor = 0;         //接触している導体の数
    [SerializeField]
    protected int giving_conductor = 0;            //電気を分け与えた導体の数


    public void AlreadyGetEnegy()
    {
        giving_conductor++;
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
        if ((set_p > power_cnt || power_cnt == 0) && energization == false)
        {
            power_cnt = set_p;
        }
    }
    public void SetPower(int set_p, GameObject Obj)
    {
        //set_pにはこのメソッドを起動したオブジェクトのpower_cntが入り、
        //それがこのオブジェクトのpower_cntより小さければ代入する
        if ((set_p > power_cnt || power_cnt == 0) && energization == false)
        {
            power_cnt = set_p;
            Obj.gameObject.GetComponent<Conductor_Script>().AlreadyGetEnegy();
        }
    }

    //自身のpower_cntが0になったときに周りも0にするためだけのメソッド
    public void SetPower(int set_p, int pow)
    {
        //上のメソッドのset_pの役割を変数powで代用する。
        if (power_save < pow)
        {
            //電源と接触している導体だけ問題外とする
            if (Power_hit != true)
            {
                //通電状態ではなくなるので通電している証となる変数を初期化する
                energi_check = true;
                GivePowerReSet();
                power_save = power_cnt;
                power_cnt = set_p;
            }
        }
    }

    //タグPower_Supplyオブジェクトからアクセスするためのメソッド
    public void SetPower(int set_p, bool turn_on)
    {
        power_cnt = set_p;
        Power_hit = turn_on;
    }

    //power_cntのゲッター
    public int GetPower() => power_cnt;


    //絶縁体の処理。セット元より自分のパワーが小さければ絶縁されない
    public void SetInsulator(bool set_insul)
    {
        hitting_insulator = set_insul;
    }

    //導体と離れた時の処理
    public void SetLeave(bool leave, int pow)
    {
        if (pow > power_cnt)
        {
            if (this.gameObject.name == "Cube")
            {
                /*バグ原因
                 電源からつながってるコンダクター(以下シリンダー1)が先にこの処理を通るとCubeコンダクターのパワーが0になる
                 それによって、Cubeコンダクターからつながってるコンダクター(以下シリンダー3)のExit処理に入り、このSetLeave
                 に入ってもCubeパワーが0(引数powの方)になってるのでこのif文に入らない

                フローチャート的に描くと
                Cubeが離れる
                ↓
                先にシリンダー1のExitがCubeに対して反応し、CubeのPower_cntが0になる
                ↓
                その後、CubeのExitがシリンダー3に対して反応し、シリンダー3の電気を消そうとする(SetLeaveに入る)が
                しかしCubeのPower_cntが0になっているのでif文の中に入らない
                ↓
                その結果、電源とつながってる導体が離れても電気がつき続ける

                やりたい処理
                Cubeが離れる
                ↓
                先にCubeのExitがシリンダー1、およびシリンダー3に反応させる
                ↓

                 */
                Debug.Log(this.gameObject.name);
                Debug.Log(power_cnt);
                Debug.Log(pow);
            }

            power_save = power_cnt;
            leaving_Conductor = leave;
            Conductor_hit = false;
            //このオブジェクトのpower_cntが0になったことにより
            //このオブジェクトと隣接しているオブジェクトも電力を失うかどうかを確認するため、trueにする
            energi_check = true;
        }
    }

    public void EffExit()
    {
        PowerOn(power_save);
    }

    public void PowerOn(int a)
    {
        power_cnt = a;
        Debug.Log(a);
    }

    public void PowerOff()
    {
        GivePowerReSet();
        power_save = power_cnt;

        power_cnt = 0;
        energi_check = true;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void OnCollisionEnter(Collision c)
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
    }

    //他の特定のオブジェクトが離れた時の処理
    public void OnCollisionExit(Collision c)
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
        }
        //導体と離れたとき
        else if (c.gameObject.tag == "Conductor")
        {
            if(power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower())
            {
                energization = false;
                power_save = power_cnt;
                power_cnt = 0;
            }
        }
    }


    public void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Conductor")
        {
            //絶縁体と接触している時、
            if ((Insulator_hit || hitting_insulator) && power_cnt > c.gameObject.GetComponent<NewConductor>().GetPower() && energization) 
            {
                c.gameObject.GetComponent<NewConductor>().SetInsulator(true);
                energization = false;
                power_save = power_cnt;
                power_cnt = 0;
            }
            else
            {
                //自身が通電してなくて、接触してるConductorObjが通電してる時、自身も通電させ
                //接触してるConductorObjの電力を1低下させた数値を取得
                if (!energization && c.gameObject.GetComponent<Base_Enegization>().GetEnergization())
                {
                    energization = true;
                    power_cnt = c.gameObject.GetComponent<NewConductor>().GetPower() - 1;
                    c.gameObject.GetComponent<NewConductor>().SetInsulator(false);
                    //オブジェクトの色をシアンにする
                    GetComponent<Renderer>().material.color = new Color32(0, 255, 255, 200);
                }
                //自身が通電しており、接触してるConductorObjが通電してなかったとき、自身も消灯し
                //電力を0にする
                else if (energization && !c.gameObject.GetComponent<Base_Enegization>().GetEnergization() && power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower())
                {
                    energization = false;
                    power_save = power_cnt;
                    power_cnt = 0;
                }
            }
        }
    }
}

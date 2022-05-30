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
    [SerializeField]
    private GameObject efflight;
    [SerializeField]
    private bool Pl_control;

    public void GivePowerReSet()
    {
        energization = false;
        power_gave = false;
        giving_conductor = 0;
    }
    //タグPower_Supplyオブジェクトからアクセスするためのメソッド
    public void SetPower(int set_p, bool turn_on)
    {
        power_cnt = set_p;
        Power_hit = turn_on;
    }

    //power_cntのゲッター
    public int GetPower() => power_cnt;

    //save_cntのゲッター
    public int GetSavePower() => power_save;


    //絶縁体の処理。セット元より自分のパワーが小さければ絶縁されない
    public void SetInsulator(bool set_insul)
    {
        hitting_insulator = set_insul;
    }

    //電力を格納
    public void PowerOn(int a)
    {
        power_cnt = a;
    }

    public void PowerOff()
    {
        GivePowerReSet();
        power_save = power_cnt;

        power_cnt = 0;
        energi_check = true;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //電線?エネルギーライン的なやつをアクティブ、非アクティブに設定する
        if (energization)
        {
            if(efflight != null)
            {
                //オブジェクトの色を表示
                efflight.SetActive(true);
            }
        }
        else
        {
            if (efflight != null)
            {
                //オブジェクトの色を非表示
                efflight.SetActive(false);
            }
            else
            {
                power_cnt = 0;
                power_save = 0;
            }
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        //電源と接触したとき
        if (c.gameObject.CompareTag("Power_Supply"))
        {
            //電気を通す
            energization = true;
        }
        //絶縁体と接触したとき
        else if (c.gameObject.CompareTag("Insulator"))
        {
            //Insulator_hitをtrueにする
            Insulator_hit = true;
        }
    }

    //他の特定のオブジェクトが離れた時の処理
    public void OnCollisionExit(Collision c)
    {

        //電源と離れたとき
        if (c.gameObject.CompareTag("Power_Supply"))
        {
            //Power_hitをfalseにし、energizationをfalseにする
            energization = false;
            Power_hit = false;
        }
        //絶縁体と離れたとき
        else if (c.gameObject.CompareTag("Insulator"))
        {
            Insulator_hit = false;
        }
        //導体と離れたとき
        else if (c.gameObject.CompareTag("Conductor"))
        {
            if (power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower())
            {
                energization = false;
                power_save = power_cnt;
            }
        }
        else if(c.gameObject.CompareTag("Rotate"))
        {
            Debug.Log(this.gameObject.transform.parent.name + "：" + c.gameObject.GetComponent<NewRotate>().GetPower());
            if (power_cnt < c.gameObject.GetComponent<NewRotate>().GetPower())
            {
                energization = false;
                power_save = power_cnt;
            }
        }
    }


    public void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Conductor")
        {
            //絶縁体と接触している時、
            if ((Insulator_hit || hitting_insulator) && (power_cnt != 0 && power_cnt > c.gameObject.GetComponent<NewConductor>().GetPower()) && energization) 
            {
                c.gameObject.GetComponent<NewConductor>().SetInsulator(true);
                energization = false;
                power_save = power_cnt;
            }
            else
            {

                //自身が通電してなくて、接触してるConductorObjが通電してる時、自身も通電させ
                //接触してるConductorObjの電力を1低下させた数値を取得
                if ((!energization && c.gameObject.GetComponent<NewConductor>().GetEnergization()) && power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower()) 
                {
                    energization = true;
                    power_cnt = c.gameObject.GetComponent<NewConductor>().GetPower() - 1;
                    c.gameObject.GetComponent<NewConductor>().SetInsulator(false);
                }
                //自身が通電しており、接触してるConductorObjが通電してなかったとき、自身も消灯し
                //電力を0にする
                else if (energization && !c.gameObject.GetComponent<NewConductor>().GetEnergization() && power_cnt < c.gameObject.GetComponent<NewConductor>().GetSavePower())
                {
                    //Debug.LogError(this.gameObject.transform.parent.name);
                    energization = false;
                    power_save = power_cnt;
                }
            }
        }
        else if (c.gameObject.tag == "Rotate")
        {
            //絶縁体と接触している時、
            if ((Insulator_hit || hitting_insulator) && (power_cnt != 0 && power_cnt > c.gameObject.GetComponent<NewRotate>().GetPower()) && energization)
            {
                c.gameObject.GetComponent<NewRotate>().SetInsulator(true);
                energization = false;
                power_save = power_cnt;
            }
            else
            {
                //自身が通電してなくて、接触してるConductorObjが通電してる時、自身も通電させ
                //接触してるConductorObjの電力を1低下させた数値を取得
                if ((!energization && c.gameObject.GetComponent<NewRotate>().GetEnergization()) && power_cnt < c.gameObject.GetComponent<NewRotate>().GetPower())
                {
                    energization = true;
                    power_cnt = c.gameObject.GetComponent<NewRotate>().GetPower() - 1;
                    c.gameObject.GetComponent<NewRotate>().SetInsulator(false);
                }
                //自身が通電しており、接触してるConductorObjが通電してなかったとき、自身も消灯し
                //電力を0にする
                else if (energization && !c.gameObject.GetComponent<NewRotate>().GetEnergization() && power_cnt < c.gameObject.GetComponent<NewRotate>().GetSavePower())
                {
                    energization = false;
                    power_save = power_cnt;
                }
            }
        }
    }
}

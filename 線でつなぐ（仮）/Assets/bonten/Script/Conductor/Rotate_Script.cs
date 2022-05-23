using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Script : Conductor_Script
{

    private const int RIGHT = 0;         //このオブジェクト
    private const int LEFT = 1;     //それ以外のオブジェクト

    public Material[] mat = new Material[2];//変更したいマテリアルをセット
    Material[] mats;

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    GameObject[] AssistObj = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    public new void Update()
    {
        if (leaving_Conductor == true)
        {
            energization = false;
            power_cnt = 0;
            leaving_Conductor = false;
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

    public new void OnCollisionExit(Collision c)
    {

        if (c.gameObject.tag == "Conductor")
        {
            //導体と接触してる総数を1個へらす
            contacing_conductor--;
            leaving_Conductor = true;
            //電気ついてるかの確認用変数をfalseにする
            Conductor_hit = false;
        }
    }

    public new void OnCollisionEnter(Collision c)
    {
        //絶縁体と接触したとき
        if (c.gameObject.tag == "Insulator")
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

    public new void OnCollisionStay(Collision collision)
    {
        //電気を遮断する処理。絶縁体と接触、自分のオブジェクトよりパワーカウントが大きいオブジェクトが絶縁体と接触していると電気遮断
        if (collision.gameObject.tag == "Conductor")
        {
            if (Insulator_hit || hitting_insulator) 
            {
                energization = false;
            }
            else if (power_cnt >= ELECTORIC_POWER )
            {
                if(AssistObj[RIGHT].GetComponent<RotateAssist>().GetHitConductor()&& AssistObj[LEFT].GetComponent<RotateAssist>().GetHitConductor())
                {
                    energization = true;
                    power_save = power_cnt;
                    collision.gameObject.GetComponent<Conductor_Script>().SetPower(power_cnt - 1);
                    collision.gameObject.GetComponent<Conductor_Script>().SetEnergization(true);
                }
                else
                {
                    energization = false;
                    if(power_cnt>collision.gameObject.GetComponent<Conductor_Script>().GetPower())
                    {
                        collision.gameObject.GetComponent<Conductor_Script>().SetPower(0, power_cnt);
                    }
                }
            }
        }
    }
}

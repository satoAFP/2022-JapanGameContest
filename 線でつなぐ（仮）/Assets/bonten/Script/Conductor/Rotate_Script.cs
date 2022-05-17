using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Script : Conductor_Script
{

    private const int OWN = 0;         //このオブジェクト
    private const int PARTHER = 1;     //それ以外のオブジェクト

    public Material[] mat = new Material[2];//変更したいマテリアルをセット
    Material[] mats;

    [NamedArrayAttribute(new string[] { "OWN", "PARTHER"})]
    [SerializeField] 
    private bool[] vertical=new bool[2];        //縦か横か向いている方向を記憶しておくための変数。trueで0or180,falseで90or270。

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        //自身の角度を判別
        if (this.gameObject.transform.localEulerAngles.y == 0 || this.gameObject.transform.localEulerAngles.y == 180) vertical[OWN] = true;
        else if (this.gameObject.transform.localEulerAngles.y == 90 || this.gameObject.transform.localEulerAngles.y == 270) vertical[OWN] = false;

        //電気を遮断する処理。絶縁体と接触、自分のオブジェクトよりパワーカウントが大きいオブジェクトが絶縁体と接触していると電気遮断
        if ((hitting_insulator == true || Insulator_hit == true || leaving_Conductor == true || contacing_conductor == 0 || vertical[OWN] != vertical[PARTHER]) && Power_hit == false)
        {
            GivePowerReSet();
            if (leaving_Conductor == true)
            {
                power_cnt = 0;
                leaving_Conductor = false;
            }
        }
        else if (power_cnt >= ELECTORIC_POWER && (Conductor_hit == true || Power_hit == true))
        {
            if(vertical[OWN]==vertical[PARTHER])
            {
                energization = true;
            }
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

            if (c.gameObject.transform.localEulerAngles.y == 0 || c.gameObject.transform.localEulerAngles.y == 180) vertical[PARTHER] = true;
            else if (c.gameObject.transform.localEulerAngles.y == 90 || c.gameObject.transform.localEulerAngles.y == 270) vertical[PARTHER] = false;


                //新しく導体に触れたら、giving_conductor,power_gave,energizationいったんリセットする
                if (energization == true)
                {
                    GivePowerReSet();
                }
        }
    }
}

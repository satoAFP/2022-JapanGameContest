using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーが操作する導体のスクリプト
public class PL_Conductor : Conductor_Script
{
    public new void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "Insulator")
        {
            //Insulator_hitをtrueにする
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor")
        {
            //導体に触れたら、現時点でどれだけの導体と接触しているかカウントする
            contacing_conductor++;

            //すでに電気が通ってる導体に触ったら既に電気を渡した回数を数える変数を+1する
            if (c.gameObject.GetComponent<Conductor_Script>().GetEnergization() == true)
            {
                //giving_conductor++;
            }

            //新しく導体に触れたら、giving_conductor,power_gave,energizationいったんリセットする
            if (energization == true)
            {
                GivePowerReSet();
            }
        }
        
    }
    public new void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Insulator")
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
            power_cnt = 0;
            contacing_conductor--;
        }
    }
}

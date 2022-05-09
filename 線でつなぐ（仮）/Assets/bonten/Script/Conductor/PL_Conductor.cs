using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーが操作する導体のスクリプト
public class PL_Conductor : Conductor_Script
{
    private void OnCollisionExit(Collision c)
    {
        //電源と接触せずに導体と離れたとき
        if (c.gameObject.tag == "Conductor")
        {
            contacing_conductor--;

            //電気ついてるかの確認用変数をfalseにする
            Conductor_hit = false;
            c.gameObject.GetComponent<Conductor_Script>().SetLeave(true, power_cnt);

            PowerOff();
        }
    }
}

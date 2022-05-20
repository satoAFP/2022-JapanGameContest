using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAssist : MonoBehaviour
{
    //アシストする回転オブジェクト
    [SerializeField]
    private GameObject RotateObj;

    private GameObject ConductorObj;

    //導体と接触してるかの成否
    [SerializeField]
    private bool hitting_conductor;

    //
    private int power = 0;

    public bool GetHitConductor() => hitting_conductor;

    public void Update()
    {
        if(ConductorObj!=null)
        {
            //接触しているConductorObjが絶縁体と離れたとき、RotateObjに電気を通し、ConductorObjを破棄
            if(!ConductorObj.GetComponent<Conductor_Script>().GetInsulator())
            {
                hitting_conductor = true;
                //電力があるか確認
                if (ConductorObj.GetComponent<Conductor_Script>().GetPower() > 1)
                {
                    power = ConductorObj.gameObject.GetComponent<Conductor_Script>().GetPower();
                    RotateObj.GetComponent<Rotate_Script>().SetPower(power);
                    ConductorObj = null;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Conductor")
        {
            hitting_conductor = true;
            //絶縁体と接触していたらオブジェクトを取得
            if (collision.gameObject.GetComponent<Conductor_Script>().GetInsulator())
            {
                hitting_conductor = false;
                ConductorObj = collision.gameObject;
            }
            //RotateObjに電気を通す
            else if(collision.gameObject.GetComponent<Conductor_Script>().GetEnergization())
            {
                power = collision.gameObject.GetComponent<Conductor_Script>().GetPower();
                RotateObj.GetComponent<Rotate_Script>().SetPower(power);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Conductor")
        {
            hitting_conductor = false;
            if(ConductorObj!=null)
            {
                ConductorObj = null;
            }
        }
    }
}

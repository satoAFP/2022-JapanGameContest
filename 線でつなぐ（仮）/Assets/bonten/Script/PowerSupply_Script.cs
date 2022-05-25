using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply_Script : MonoBehaviour
{

    [SerializeField]
    private bool power_on = false;
    [SerializeField]
    private bool gave_power = false;
    [SerializeField]
    private int  list_count = 0;


    [SerializeField]
    private List<GameObject> obj_list=new List<GameObject>();

    [SerializeField]
    private int electoric_power;            //接触している、タグConductorオブジェクトに譲渡する電力の値
    
    public void SetPowerSupply()
    {
        if (power_on == false)
        {
            power_on = true;
        }
        else if(power_on == true)
        {
            power_on = false;
        }
        list_count = 0;
        int i = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        electoric_power = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (power_on != gave_power)
        {
            SetPowerSupply();
        }

        if (power_on == true && list_count < obj_list.Count)
        {
            //導体に電気を通す
            obj_list[list_count].GetComponent<Conductor_Script>().SetPower(electoric_power, true);
            list_count++;
        }
        else if (power_on == false && list_count < obj_list.Count)
        {
            //電源を切って電気を消す。
            obj_list[list_count].GetComponent<Conductor_Script>().PowerOff();
            list_count++;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //触れたConductorObjをリストに追加
        if (collision.gameObject.tag == "Conductor")
        {
            obj_list.Add(collision.gameObject);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //離れたときにConductorオブジェクトをリストから削除&電源を消す
        if (collision.gameObject.tag == "Conductor")
        {
            //ConductorObjの電気を消して該当Objのリストも消す
            collision.gameObject.GetComponent<Conductor_Script>().PowerOff();
            obj_list.Remove(collision.gameObject);
            //list_count < obj_listの数 にならないようにする
            list_count = obj_list.Count;
        }
    }
}

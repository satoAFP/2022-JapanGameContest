using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply_Script : MonoBehaviour
{

    [SerializeField]
    private bool power_on = false;
    private bool gave_power = true;
    private int  list_count = 0;


    [SerializeField]
    private List<GameObject> obj_list=new List<GameObject>();

    [SerializeField]
    private int electoric_power = 1;            //�ڐG���Ă���A�^�OConductor�I�u�W�F�N�g�ɏ��n����d�͂̒l

    public void SetPowerSupply(bool turn_on)
    {
        power_on = turn_on;
        list_count = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //power_on��false�̎���obj_list�̗v�f����0�ɂȂ�܂�
        if (power_on == true && list_count < obj_list.Count)
        {
            obj_list[list_count].GetComponent<Conductor_Script>().SetPower(electoric_power, true);
            list_count++;
        }
        else if (power_on == false && list_count < obj_list.Count)
        {
            obj_list[list_count].GetComponent<Conductor_Script>().PowerOff();
            list_count++;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Conductor")
        {
            obj_list.Add(collision.gameObject);

        }
    }
}

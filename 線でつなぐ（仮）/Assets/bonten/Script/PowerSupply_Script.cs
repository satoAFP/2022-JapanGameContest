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
    private int electoric_power;            //�ڐG���Ă���A�^�OConductor�I�u�W�F�N�g�ɏ��n����d�͂̒l
    
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
            //���̂ɓd�C��ʂ�
            obj_list[list_count].GetComponent<Conductor_Script>().SetPower(electoric_power, true);
            list_count++;
        }
        else if (power_on == false && list_count < obj_list.Count)
        {
            //�d����؂��ēd�C�������B
            obj_list[list_count].GetComponent<Conductor_Script>().PowerOff();
            list_count++;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //�G�ꂽConductorObj�����X�g�ɒǉ�
        if (collision.gameObject.tag == "Conductor")
        {
            obj_list.Add(collision.gameObject);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //���ꂽ�Ƃ���Conductor�I�u�W�F�N�g�����X�g����폜&�d��������
        if (collision.gameObject.tag == "Conductor")
        {
            //ConductorObj�̓d�C�������ĊY��Obj�̃��X�g������
            collision.gameObject.GetComponent<Conductor_Script>().PowerOff();
            obj_list.Remove(collision.gameObject);
            //list_count < obj_list�̐� �ɂȂ�Ȃ��悤�ɂ���
            list_count = obj_list.Count;
        }
    }
}

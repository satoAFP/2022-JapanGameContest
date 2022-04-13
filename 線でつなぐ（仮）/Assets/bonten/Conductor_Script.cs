using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected
    //�ʓd�ϐ���true�ɂȂ邩�ǂ����m�F����֐�
    void Confimation_Energi()
    {
        //�d���ƐڐG���Ă�Ƃ�
        if (power_supply_hit == true)
        {
            //�d�C���ʂ�
            energization = true;
        }
        //���̂ƐڐG���Ă�Ƃ�
        else if (counductor_hit == true)
        {

            //�≏�̂ƐڐG���Ă�Ƃ�
            if (insulator_hit == true)
            {
                //�d�C���ʂ�Ȃ��Ȃ�
                energization = false;
            }
            else
            {
                //�d�C���ʂ�
                energization = true;
            }
        }
    }

    public

    bool counductor_hit = false;        //���̂Ɠ���������
    bool insulator_hit = false;         //�≏�̂Ɠ���������
    bool power_supply_hit = false;      //�d���Ɠ���������
    bool energization = false;          //�ʓd���Ă邩

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Confimation_Energi();

        if (energization == true)
        {
            //�I�u�W�F�N�g�̐F���V�A���ɂ���
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            //�I�u�W�F�N�g�̐F���O���[�ɂ���
            GetComponent<Renderer>().material.color = Color.gray;
        }


    }

    //�����ŐG��Ă�I�u�W�F�N�g�����X�g�ɂԂ�����
    void OnCollisionEnter(Collision c)
    {
        //�d���ƐڐG���Ă�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            //power_supply_hit��true�ɂ���
            power_supply_hit = true;
        }
        //�d���ƐڐG���Ă�Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            //insulator_hit��true�ɂ���
            insulator_hit = true;
        }
        //�≏�̂ƐڐG���Ă�Ƃ�!
        else if (c.gameObject.tag == "Conductor")
        {
            
            bool energi_investigate = c.gameObject.GetComponent<Conductor_Script>().energization;

            //�Ȃ����Ă��铱�̂�energizasion��true�Ȃ炱��obj��counductorhit��true�ɂ���
            if (energi_investigate == true)
            {
                Debug.Log("true");
                //counductor_hit��true�ɂ���
                counductor_hit = true;
            }
        }
    }

    //���ꂽ�痣�ꂽ�I�u�W�F�N�g�̃��X�g���폜
    void OnCollisionExit(Collision c)
    {
        //�d���Ɨ��ꂽ�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            //power_supply_hit��true�ɂ���
            power_supply_hit = false;
        }
        //�d���Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            //insulator_hit��false�ɂ���
            insulator_hit = false;
        }
        //�d���ƐڐG�����ɓ��̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Conductor")
        {
            //counductor_hit
            //counductor_hit = false;
        }
    }
}

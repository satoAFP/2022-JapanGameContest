using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Script : Conductor_Script
{

    private const int OWN = 0;         //���̃I�u�W�F�N�g
    private const int PARTHER = 1;     //����ȊO�̃I�u�W�F�N�g

    public Material[] mat = new Material[2];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    [NamedArrayAttribute(new string[] { "OWN", "PARTHER"})]
    [SerializeField] 
    private bool[] vertical=new bool[2];        //�c�����������Ă���������L�����Ă������߂̕ϐ��Btrue��0or180,false��90or270�B

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    public new void Update()
    {
        //���g�̊p�x�𔻕�
        if (this.gameObject.transform.localEulerAngles.y == 0 || this.gameObject.transform.localEulerAngles.y == -180) vertical[OWN] = true;
        else if (this.gameObject.transform.localEulerAngles.y == 90 || this.gameObject.transform.localEulerAngles.y == -90) vertical[OWN] = false;


        if (energization == true)
        {
            //�I�u�W�F�N�g�̐F���V�A���ɂ���
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (energization == false)
        {
            //�I�u�W�F�N�g�̐F���O���[�ɂ���
            GetComponent<Renderer>().material.color = Color.gray;

        }
    }

    public new void OnCollisionEnter(Collision c)
    {
        //�≏�̂ƐڐG�����Ƃ�
        if (c.gameObject.tag == "Insulator")
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor")
        {
            //���̂ɐG�ꂽ��A�����_�łǂꂾ���̓��̂ƐڐG���Ă��邩�J�E���g����
            contacing_conductor++;

            if (c.gameObject.transform.localEulerAngles.y == 0 || c.gameObject.transform.localEulerAngles.y == -180) vertical[PARTHER] = false;
            else if (c.gameObject.transform.localEulerAngles.y == 90 || c.gameObject.transform.localEulerAngles.y == -90) vertical[PARTHER] = true;


                //�V�������̂ɐG�ꂽ��Agiving_conductor,power_gave,energization�������񃊃Z�b�g����
                if (energization == true)
                {
                    GivePowerReSet();
                }
        }
    }

    public new void OnCollisionStay(Collision collision)
    {
        //�d�C���Ւf���鏈���B�≏�̂ƐڐG�A�����̃I�u�W�F�N�g���p���[�J�E���g���傫���I�u�W�F�N�g���≏�̂ƐڐG���Ă���Ɠd�C�Ւf
        if (collision.gameObject.tag == "Conductor")
        {
            if (Insulator_hit || hitting_insulator) 
            {
                energization = false;
            }
            else if (power_cnt >= ELECTORIC_POWER )
            {
                if(vertical[OWN] == vertical[PARTHER])
                {
                    energization = true;
                    power_save = power_cnt;
                    collision.gameObject.GetComponent<Conductor_Script>().SetPower(power_cnt - 1);
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

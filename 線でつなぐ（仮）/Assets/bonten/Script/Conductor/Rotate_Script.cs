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
    void Update()
    {
        //���g�̊p�x�𔻕�
        if (this.gameObject.transform.localEulerAngles.y == 0 || this.gameObject.transform.localEulerAngles.y == 180) vertical[OWN] = true;
        else if (this.gameObject.transform.localEulerAngles.y == 90 || this.gameObject.transform.localEulerAngles.y == 270) vertical[OWN] = false;

        //�d�C���Ւf���鏈���B�≏�̂ƐڐG�A�����̃I�u�W�F�N�g���p���[�J�E���g���傫���I�u�W�F�N�g���≏�̂ƐڐG���Ă���Ɠd�C�Ւf
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

            if (c.gameObject.transform.localEulerAngles.y == 0 || c.gameObject.transform.localEulerAngles.y == 180) vertical[PARTHER] = true;
            else if (c.gameObject.transform.localEulerAngles.y == 90 || c.gameObject.transform.localEulerAngles.y == 270) vertical[PARTHER] = false;


                //�V�������̂ɐG�ꂽ��Agiving_conductor,power_gave,energization�������񃊃Z�b�g����
                if (energization == true)
                {
                    GivePowerReSet();
                }
        }
    }
}

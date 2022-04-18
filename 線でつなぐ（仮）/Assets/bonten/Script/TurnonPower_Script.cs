using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnonPower_Script : MonoBehaviour
{
    public bool turn_on_power = false;
    public GameObject Door;
    public int Clear_num;
    private GameObject NonlitingChild;//�����ƂȂ��ĂȂ��q�I�u�W�F�N�g�擾�p
    private GameObject LiteingChild;//�����ƂȂ��Ă�q�I�u�W�F�N�g�擾�p
    private bool energi_investigate;//�d�C���Ă邩�̊m�F�p�ϐ�
    // Start is called before the first frame update
    void Start()
    {
        NonlitingChild = gameObject.transform.Find("Nonliting").gameObject;
        LiteingChild = gameObject.transform.Find("Liting").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (energi_investigate == false)
        {
            turn_on_power = false;
            //Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;
        }

        if (turn_on_power == true)
        {
            NonlitingChild.SetActive(false);
            LiteingChild.SetActive(true);
        }
        else
        {
            NonlitingChild.SetActive(true);
            LiteingChild.SetActive(false);
        }
    }


    private void OnCollisionStay(Collision c)
    {
        //�^�O��Conductor�ƐڐG���Â��Ă鎞
        if (c.gameObject.tag == "Conductor")
        {
            //�Ȃ����Ă铱�̂�energization(�ʓd�m�F�p�ϐ�)�ŏ�����
            energi_investigate = c.gameObject.GetComponent<Conductor_Script>().GetEnergization();

            //Debug.Log(c.gameObject.name);
            //�Ȃ����Ă��铱�̂�energizasion��true�Ȃ炱��obj��counductorhit��true�ɂ���
            if (energi_investigate == true)
            {
                //counductor_hit��true�ɂ���
                turn_on_power = true;
                //Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = true;
            }
        }
    }


    private void OnCollisionExit(Collision c)
    {
        //�^�O��Conductor�Ɨ��ꂽ�Ƃ�
        if (c.gameObject.tag == "Conductor")
        {
            turn_on_power = false;
            //Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;

        }
    }

}

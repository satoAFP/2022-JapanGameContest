using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTurnOnPower : MonoBehaviour
{
    public bool turn_on_power = false;
    public GameObject Door;
    public int Clear_num;
    private GameObject NonlitingChild;//�����ƂȂ��ĂȂ��q�I�u�W�F�N�g�擾�p
    private GameObject LiteingChild;//�����ƂȂ��Ă�q�I�u�W�F�N�g�擾�p
    private bool isEnergized;        //�ʓd���Ă邩�ǂ����̊m�F�p�ϐ�

    [SerializeField, Header("�d�C�݂̂��ǂ���(prefub�ŊǗ�)")]
    private bool enegeronly;

    //�����擾
    [SerializeField]
    private AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (enegeronly == true)
        {
            NonlitingChild = gameObject.transform.Find("Nonliting").gameObject;
            LiteingChild = gameObject.transform.Find("Liting").gameObject;
        }
        //�����͂�������
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enegeronly == true)
        {
            if (isEnergized == false)
            {
                turn_on_power = false;
                Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;
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
    }


    private void OnCollisionStay(Collision c)
    {
        //�G�i�W�[�I�����[���ǂ����Ŕ���OFF
        if (enegeronly == true)
        {
            //�^�O��Conductor�ƐڐG���Â��Ă鎞
            if (c.gameObject.CompareTag("Conductor"))
            {
                //�Ȃ����Ă铱�̂�energization(�ʓd�m�F�p�ϐ�)�ŏ�����
                isEnergized = c.gameObject.GetComponent<NewConductor>().GetEnergization();

                //Debug.Log(c.gameObject.name);
                //�Ȃ����Ă��铱�̂�energizasion��true�Ȃ炱��obj��counductorhit��true�ɂ���
                if (isEnergized == true)
                {
                    //counductor_hit��true�ɂ���(�A���ŐM����΂���l�ɍŏ��̈�x�̂ݓK�p)
                    if (turn_on_power == false)
                    {
                        audioSource.PlayOneShot(sound1);
                        turn_on_power = true;
                        Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = true;
                    }

                }
                else 
                {
                    //counductor_hit��true�ɂ���(�A���ŐM����΂���l�ɍŏ��̈�x�̂ݓK�p)
                    if (turn_on_power == true)
                    {
                        turn_on_power = false;
                        Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;
                    }
                }
            }
        }
    }


    private void OnCollisionExit(Collision c)
    {
        //�G�i�W�[�I�����[���ǂ����Ŕ���OFF
        if (enegeronly == true)
        {
            //�^�O��Conductor�Ɨ��ꂽ�Ƃ�
            if (c.gameObject.CompareTag("Conductor"))
            {
                turn_on_power = false;
                Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;

            }
        }
    }

    public void else_Switch_on()
    {
        //counductor_hit��true�ɂ���(�A���ŐM����΂���l�ɍŏ��̈�x�̂ݓK�p)
        if (turn_on_power == false)
        {
            audioSource.PlayOneShot(sound1);
            turn_on_power = true;
            Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = true;
        }

    }

    public void else_Switch_off()
    {
        //OFF��
        turn_on_power = false;
        Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;


    }
}

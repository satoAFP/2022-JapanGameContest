using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapcipSlect : MonoBehaviour
{
    [SerializeField]
    public Material[] mat = new Material[3];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    public bool Onblock = false;//���g�̃}�b�v�`�b�v�Ƀu���b�N������Ă���Ƃ���true

    public bool Onplayer = false;//���g�̃}�b�v�`�b�v�Ƀv���C���[������Ă���Ƃ���true

    public bool Onobj = false;   //���g�̃}�b�v�`�b�v�ɃI�u�W�F�N�g������Ă���Ƃ���true

    //�Z���N�gON,OFF�Ń}�e���A������
    private bool now_select;

    BoxCastRayTest script;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
        now_select = false;

        script = GameObject.Find("fps_camera").GetComponent<BoxCastRayTest>();
    }

    // Update is called once per frame
    void Update()
    {
        //�N���b�N�̖��m�ȋN�_���Ȃ�����Update�ŏ����i�}�E�X���O�ꂽ�Ƃ��Ȃǁj
        //����Ȃ�Ray�̊O�ꂽ���ɌĂяo�����֐����g������
        if (now_select == true && Onplayer==false)
        {
            now_select = false;
            mats[0] = mat[1];

            GetComponent<Renderer>().materials = mats;
        }
        //�v���C���[�������߂��Ƀu���b�N�u���Ȃ�����
        else if(now_select == true && Onplayer ==true && script.grab==true)
        {
            now_select = false;
            mats[0] = mat[2];

            GetComponent<Renderer>().materials = mats;
        }
        else
        {
            if (mats[0] == mat[1])
            {
                mats[0] = mat[0];

                GetComponent<Renderer>().materials = mats;
            }
            else if (mats[0] == mat[2])
            {
                mats[0] = mat[0];

                GetComponent<Renderer>().materials = mats;
            }
        }

    }

    //�v���C���[���}�b�v�`�b�v�ɐN�����Ă����A���̃}�b�v�`�b�v�ɒu���Ȃ��悤�ɂ���
    public void OnTriggerStay(Collider other)
    {
        //�ڐG�����I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("Player"))
        {
           // Debug.Log("���F�L�b�`����");
            Onplayer = true;
        }
        //if (other.CompareTag("Noset"))
        //{
        //    Debug.Log("���F�L�b�`����");
        //    Onplayer = true;
        //}
    }

    //�v���C���[���N�������}�b�v�`�b�v�𗣂ꂽ�Ƃ��A�u���Ȃ���������
    public void OnTriggerExit(Collider other)
    {
        //�ڐG�����I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("Player"))
        {
           // Debug.Log("�t�H�[�N�Ȃ̂ɂ��������I");
            Onplayer = false;
        }
    }

    

    public void ChangeMaterial()
    {
        now_select = true;
    }
}

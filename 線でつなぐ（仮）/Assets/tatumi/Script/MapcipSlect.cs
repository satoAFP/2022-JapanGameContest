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
            //�}�b�v�`�b�v�̏�Ƀu���b�N���u���Ă������ꍇ�A�}�b�v�`�b�v�̐F��ς��Ȃ�
            if (Onblock==true)
            {
               
            }
            else if(Onobj == true)
            {
                Debug.Log("����");
                mats[0] = mat[2];
            }
            else
            {
                mats[0] = mat[1];
            }
          
            GetComponent<Renderer>().materials = mats;
        }
        //�v���C���[�������߂��Ƀu���b�N�u���Ȃ�����
        else if(now_select == true && Onplayer ==true && script.grab==true)
        {
            now_select = false;
            //�}�b�v�`�b�v�̏�Ƀu���b�N���u���Ă������ꍇ�A�}�b�v�`�b�v�̐F��ς��Ȃ�
            if (Onblock == true)
            {
                Debug.Log("53��");
            }
            else if(Onobj==true)
            {
                Debug.Log("���F");
                mats[0] = mat[2];
            }
            else
            {
                mats[0] = mat[2];
            }

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

        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        //�ڐG�����I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("Player"))
        {
           // Debug.Log("���F�L�b�`����");
            Onplayer = true;
        }
        //�d�����d�C�u���b�N
        if (other.CompareTag("Conductor"))
        {
            Onobj = true;
        }
        //�F�p�u���b�N
        if (other.gameObject.tag == "ColorInput")
        {
            Onobj = true;
        }
        //�F�p�d��
        if (other.gameObject.tag == "ColorOutput")
        {
            Onobj = true;
        }
        //���F�I�u�W�F�N�g
        if (other.gameObject.tag == "ColorMix")
        {
            Onobj = true;
        }
        ////���p�u���b�N
        //if (other.gameObject.tag == "MusicInput")
        //{
        //    Onobj = true;
        //}
        //���p�d��
        if (other.gameObject.tag == "MusicOutput")
        {
            Onobj = true;
        }
        //�������I�u�W�F�N�g
        if (other.gameObject.tag == "MusicMix")
        {
            Onobj = true;
        }

        //���C�g�I�u�W�F�N�g���}�b�v�`�b�v�̏�ɂ��鎞�A�u���b�N��u���Ȃ��悤�ɂ���
        //�i�}�b�v�`�b�v�̏�ɏ���Ă���Ƃ��̂݁I�I�I�I�j
        if (other.CompareTag("Power_Supply"))
        {
          //  Debug.Log("�o�i�i");
            Onobj = true;
        }

        //���C�g�I�u�W�F�N�g(�o���H)���}�b�v�`�b�v�̏�ɂ��鎞�A�u���b�N��u���Ȃ��悤�ɂ���
        //�i�}�b�v�`�b�v�̏�ɏ���Ă���Ƃ��̂݁I�I�I�I�j
        if (other.CompareTag("Noset"))
        {
            //Debug.Log("���F�L�b�`����");
            Onobj = true;
        }

      
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

        //�d�C�u���b�N�������痣�ꂽ�Ƃ��}�b�v�`�b�v�̐F�����ɖ߂�
        if (other.CompareTag("Conductor"))
        {
            Debug.Log("�f�G����");
            Onobj = false;
        }

        //��]�u���b�N�������痣�ꂽ�Ƃ��}�b�v�`�b�v�̐F�����ɖ߂�
        if (other.CompareTag("Rotate"))
        {
         //   Debug.Log("�f�G����");
            Onobj = false;
        }

        //�F�p�u���b�N�������痣�ꂽ�Ƃ��}�b�v�`�b�v�̐F�����ɖ߂�
        if (other.gameObject.tag == "ColorInput")
        {
            Onobj = false;
        }

        //���p�u���b�N�������痣�ꂽ�Ƃ��}�b�v�`�b�v�̐F�����ɖ߂�
        if (other.gameObject.tag == "MusicInput")
        {
            Onobj = false;
        }
    }

    

    public void ChangeMaterial()
    {
        now_select = true;
    }
}

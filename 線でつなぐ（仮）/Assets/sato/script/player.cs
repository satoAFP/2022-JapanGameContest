using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class player : MonoBehaviour
{
    //�C���X�y�N�^�[�ݒ�
    [SerializeField, Header("��l���̈ړ���"), Header("��l���̃X�e�[�^�X"), Range(0, 10)]     float move_power;
    [SerializeField, Header("�}�E�X���x"), Range(100, 300)]          float mouse_power;
    [SerializeField, Header("�W�����v��"), Range(0, 10)]             float jump_power;
    [SerializeField, Header("�ǂ���鑬�x"), Range(0.01f, 0.1f)]    float climbing_speed;
    [SerializeField, Header("�}�E�X�㉺�̌��E"), Range(0, 0.5f)]     float mouse_max_y;

    //�Q�[���I�u�W�F�N�g�̎擾
    [SerializeField, Header("��l���̃J�����Z�b�g"), Header("�Q�[���I�u�W�F�N�g�̎擾")] GameObject my_camera;
    [SerializeField, Header("�ʏ�J����")] GameObject camera;
    [SerializeField, Header("climbing_check_head")]     GameObject head;
    [SerializeField, Header("climbing_check_leg")]      GameObject leg;


    //���̃X�N���v�g�Ƃ���肷��ϐ�
    [System.NonSerialized] public bool Ground_check = true;                               //���n���Ă��邩�ǂ����̔���
    [System.NonSerialized] public bool Move_check = false;                                //�ړ����Ă��邩�ǂ����̔���


    //�J�[�\���̈ړ��ݒ�
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);


    //�v���C�x�[�g�ϐ�
    private Vector3 velocity;                                       //���W�b�g�{�f�B�̗�
    private Rigidbody rb;                                           //���W�b�h�{�f�B���擾���邽�߂̕ϐ�
    private float mem_camera_rotato_y = 0;                          //�J������Y����]�L��
    private Transform camTransform;                                 //camera��transform
    private Vector3 startMousePos;                                  //�}�E�X����̎n�_
    private Vector3 presentCamRotation;                             //�J������]�̎n�_���
    private Vector3 cursol_pos_check;                               //�J�[�\���̍��W�L��
    private Vector3 vertual_cursol_pos = new Vector3(1000, 0, 0);   //���ۂ̃J�[�\���̈ړ��ʕ��̍��W
    private bool cursol_reset = false;                              //�J�[�\���̍��W�����Z�b�g���ꂽ�Ƃ�true
    private bool cursol_pop = false;                                //�J�[�\�����o�������邩�ǂ���
    private bool climbing_check_head = false;                       //�ǂ̂ڂ肪�o���鍂��������
    private bool climbing_check_leg = false;                        //���܂ŕǂ̂ڂ肷�邩����
    private bool camera_change = true;                              //�J�����̐؂�ւ�
    private bool fade_check = false;                                //�t�F�[�h���邩�ǂ����؂�ւ�
    private bool fade_updown = true;                                //���߃��x�����オ�邩�����邩
    private bool first_camera_pos_set = true;                       //��ԍŏ��̃J�����̈ʒu�̏������������s
    private bool first_scene_move = true;                           //�V�[�����؂�ւ�������������s


    //�A���ŉ�����Ȃ����߂̔���
    private bool key_check_E = true;
    private bool key_check_C = true;
    private bool key_check_Space = true;



    // Start is called before the first frame update
    void Start()
    {
        //���W�b�h�{�f�B���擾
        rb = GetComponent<Rigidbody>();
        
        //�J�[�\���������āA�����Ƀ��b�N
        Cursor.visible = false;
        SetCursorPos(1024, 576);
        //my_camera.transform.rotation = Quaternion.identity;

        //�J�����֌W������
        camTransform = this.gameObject.transform;
        presentCamRotation.x = camTransform.transform.eulerAngles.x;
        presentCamRotation.y = camTransform.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���̍X�V--------------------------------------------------------------------------------------------
        //��l�����ǂ����Ƃ��̔���̍X�V
        climbing_check_head = head.GetComponent<climbing_check>().check;
        climbing_check_leg = leg.GetComponent<climbing_check>().check;

        //--------------------------------------------------------------------------------------------


        //�J�[�\���̍��W�����Z�b�g���ꂽ�Ƃ��A�ړ��ʂ����Z�b�g����Ȃ��悤
        if (cursol_reset)
        {
            cursol_pos_check = Input.mousePosition;
            cursol_reset = false;
        }


        //�J�[�\���̕\���A��\��
        if (Input.GetKey(KeyCode.E))
        {
            if (key_check_E)
            {
                if (cursol_pop)
                {
                    cursol_pop = false;
                    Cursor.visible = false;
                }
                else
                {
                    cursol_pop = true;
                    Cursor.visible = true;
                    SetCursorPos(1024, 576);
                }
            }
            key_check_E = false;
        }
        else{ key_check_E = true; }


        //���j���[�\�����͓����Ȃ�
        if (!cursol_pop)
        {
            //�}�E�X����-----------------------------------------------------------------------------------------
            CameraRotationMouseControl();



            //���E�㉺�̈ړ�����---------------------------------------------------------------------------------
            if (Input.GetKey(KeyCode.W))
            {
                velocity = gameObject.transform.rotation * new Vector3(0, 0, move_power);
                Move(velocity * Time.deltaTime);
                Move_check = true;

                if (!climbing_check_head)
                {
                    if (climbing_check_leg)
                    {
                        this.gameObject.transform.position += new Vector3(0, climbing_speed, 0);
                    }
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity = gameObject.transform.rotation * new Vector3(-move_power, 0, 0);
                Move(velocity * Time.deltaTime);
                Move_check = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity = gameObject.transform.rotation * new Vector3(0, 0, -move_power);
                Move(velocity * Time.deltaTime);
                Move_check = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity = gameObject.transform.rotation * new Vector3(move_power, 0, 0);
                Move(velocity * Time.deltaTime);
                Move_check = true;
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
                Move_check = false;
        }


        //�n�ʂ̒��n���Ă��邩�ǂ�������
        //���n���Ă���Ƃ�
        if (Ground_check)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (key_check_Space)
                {
                    rb.AddForce(new Vector3(0, jump_power * 100, 0)); //��Ɍ������ė͂�������
                    key_check_Space = false;
                }
            }
            else
                key_check_Space = true;
        }


        //�O���[�X�P�[���J�����؂�ւ�
        if (Input.GetKey(KeyCode.C))
        {
            if (key_check_C)
            {
                //�t�F�[�h�I��
                fade_check = true;

                //�J�����؂�ւ�
                if (camera_change)
                {
                    camera.GetComponent<PostEffect>().enabled = true;
                    camera_change = false;
                }
                else
                {
                    camera.GetComponent<PostEffect>().enabled = false;
                    camera_change = true;
                }
            }
            key_check_C = false;
        }
        else { key_check_C = true; }





        //�J�[�\���̍��W�L��
        cursol_pos_check = Input.mousePosition;
    }



    //�ړ������֐�
    private void Move(Vector3 vec)
    {
        this.gameObject.transform.position += vec;
    }

    

    //�J�����R���g���[���֐�
    private void CameraRotationMouseControl()
    {
        //�J�����̏��������Œ�
        if (first_camera_pos_set)
        {
            cursol_pos_check.x = 0;
            cursol_pos_check.y = 0;
        }

        //���ۂ̃J�[�\���̈ړ��ʌv�Z
        vertual_cursol_pos.x += Input.mousePosition.x - cursol_pos_check.x;
        vertual_cursol_pos.y += Input.mousePosition.y - cursol_pos_check.y;
        
        //�}�E�X�̈ړ���0�ɏ�����
        if (first_camera_pos_set)
        {
            startMousePos = vertual_cursol_pos;
            first_camera_pos_set = false;
        }

        //(�ړ��J�n���W - ���ۂ̃J�[�\���̍��W) / �𑜓x �Ő��K��
        float x = (-startMousePos.x + vertual_cursol_pos.x) / Screen.width;
        float y = mem_camera_rotato_y;

        
        
        //Y���̉�]�͈��l(mouse_max_y)�Ŏ~�܂�
        if (((startMousePos.y - vertual_cursol_pos.y) / Screen.height) <= mouse_max_y &&
            ((startMousePos.y - vertual_cursol_pos.y) / Screen.height) >= -mouse_max_y)
        {
            //(�ړ��J�n���W - ���ۂ̃J�[�\���̍��W) / �𑜓x �Ő��K��
            y = (startMousePos.y - vertual_cursol_pos.y) / Screen.height;
            mem_camera_rotato_y = y;
        }
        else
        {
            //Y���̈ړ����E�܂ŗ����Ƃ��A��������vertual_cursol_pos.y���i�܂Ȃ��悤�ɌŒ�
            if (((startMousePos.y - vertual_cursol_pos.y) / Screen.height) > mouse_max_y)
            {
                vertual_cursol_pos.y = -(mouse_max_y * Screen.height - startMousePos.y);
            }
            if (((startMousePos.y - vertual_cursol_pos.y) / Screen.height) < -mouse_max_y)
            {
                vertual_cursol_pos.y = -(-mouse_max_y * Screen.height - startMousePos.y);
            }
        }

        //��]�J�n�p�x �{ �}�E�X�̕ω��� * 90
        float eulerX = y * mouse_power;
        float eulerY = x * mouse_power;

        
        if (!cursol_pop)
        {
            if (cursol_pos_check.x > 950 || cursol_pos_check.x < 35 ||
                cursol_pos_check.y > 540 || cursol_pos_check.y < 40)
            {
                SetCursorPos(1024, 576);
                cursol_reset = true;
            }
        }
        
        //��l���ƃJ�����ɂ��ꂼ��A��]�ʑ��
        camTransform.rotation = Quaternion.Euler(0, eulerY, 0);
        my_camera.transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        
    }
}

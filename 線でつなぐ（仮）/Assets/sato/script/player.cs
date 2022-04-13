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
    [SerializeField, Header("�ǂ���鑬�x"), Range(0.01f, 0.05f)]    float climbing_speed;
    [SerializeField, Header("�}�E�X�㉺�̌��E"), Range(0, 0.5f)]     float mouse_max_y;
    [SerializeField, Header("�t�F�[�h�̎���"), Range(0.5f, 3.0f)]    float fade_time;

    //�Q�[���I�u�W�F�N�g�̎擾
    [SerializeField, Header("��l���̃J�����Z�b�g"), Header("�Q�[���I�u�W�F�N�g�̎擾")] GameObject my_camera;
    [SerializeField, Header("�ʏ�J����")] GameObject common_camera;
    [SerializeField, Header("�O���[�J����")] GameObject gray_camera;
    [SerializeField, Header("fade�pimage")] GameObject fade;
    [SerializeField, Header("climbing_check_head")]     GameObject head;
    [SerializeField, Header("climbing_check_leg")]      GameObject leg;


    //�J�[�\���̈ړ��ݒ�
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);


    //�v���C�x�[�g�ϐ�
    private Vector3 velocity;                                       //���W�b�g�{�f�B�̗�
    private Rigidbody rb;                                           //���W�b�h�{�f�B���擾���邽�߂̕ϐ�
    private bool isGround = true;                                   //���n���Ă��邩�ǂ����̔���
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


    //�A���ŉ�����Ȃ����߂̔���
    private bool key_check_E = true;
    private bool key_check_C = true;



    // Start is called before the first frame update
    void Start()
    {
        //���W�b�h�{�f�B���擾
        rb = GetComponent<Rigidbody>();
        
        //�J�����֌W������
        camTransform = this.gameObject.transform;
        startMousePos = Input.mousePosition;
        presentCamRotation.x = camTransform.transform.eulerAngles.x;
        presentCamRotation.y = camTransform.transform.eulerAngles.y;

        //�J�[�\���������āA�����Ƀ��b�N
        Cursor.visible = false;
        SetCursorPos(1000, 600);
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
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity = gameObject.transform.rotation * new Vector3(0, 0, -move_power);
                Move(velocity * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity = gameObject.transform.rotation * new Vector3(move_power, 0, 0);
                Move(velocity * Time.deltaTime);
            }
        }


        //�n�ʂ̒��n���Ă��邩�ǂ�������
        //���n���Ă���Ƃ�
        if (isGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isGround = false;
                rb.AddForce(new Vector3(0, jump_power * 100, 0)); //��Ɍ������ė͂�������
            }
        }


        //�J�[�\���\��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        //�J�[�\���̍��W�L��
        cursol_pos_check = Input.mousePosition;



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
                    common_camera.SetActive(false);
                    gray_camera.SetActive(true);
                    camera_change = false;
                }
                else
                {
                    common_camera.SetActive(true);
                    gray_camera.SetActive(false);
                    camera_change = true;
                }
            }
            key_check_C = false;
        }
        else { key_check_C = true; }

        //�t�F�[�h����
        if(fade_check)
        {
            //�t�F�[�h���s
            if (fade_updown)
                fade.GetComponent<Image>().color += new Color(0, 0, 0, 0.1f);
            else
                fade.GetComponent<Image>().color -= new Color(0, 0, 0, 0.1f);

            //���߃��x���̏グ�����؂�ւ�
            if (fade.GetComponent<Image>().color.a >= fade_time)
            {
                fade_updown = false;
            }
            else if (fade.GetComponent<Image>().color.a <= 0)
            {
                fade_updown = true;
                fade_check = false;
            }
        }
    }



    void OnCollisionEnter(Collision col)
    {
        //Ground�^�O�̃I�u�W�F�N�g�ɐG�ꂽ�Ƃ�
        if (col.gameObject.tag == "Ground") 
        {
            //isGround��true�ɂ���
            isGround = true; 
        }
    }



    //�ړ������֐�
    private void Move(Vector3 vec)
    {
        this.gameObject.transform.position += vec;
    }



    //�J�����R���g���[���֐�
    private void CameraRotationMouseControl()
    {
        //���ۂ̃J�[�\���̈ړ��ʌv�Z
        vertual_cursol_pos.x += Input.mousePosition.x - cursol_pos_check.x;
        //(�ړ��J�n���W - ���ۂ̃J�[�\���̍��W) / �𑜓x �Ő��K��
        float x = (startMousePos.x + vertual_cursol_pos.x) / Screen.width;
        float y = mem_camera_rotato_y;
        
        //���ۂ̃J�[�\���̈ړ��ʌv�Z
        vertual_cursol_pos.y += Input.mousePosition.y - cursol_pos_check.y;
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
            //�����J�[�\�����q�̍��W������o����J�[�\���̈ʒu�����Z�b�g
            if (Input.mousePosition.x > 950 || Input.mousePosition.x < 35 ||
                Input.mousePosition.y > 540 || Input.mousePosition.y < 40)
            {
                SetCursorPos(1000, 600);
                cursol_reset = true;
            }
        }
        Debug.Log("" + eulerX);
        //��l���ƃJ�����ɂ��ꂼ��A��]�ʑ��
        camTransform.rotation = Quaternion.Euler(0, eulerY, 0);
        my_camera.transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_move : MonoBehaviour
{
    //�C���X�y�N�^�[�ݒ�
    [SerializeField, Header("��̓������x"), Range(1, 10)] int ani_speed;
    [SerializeField, Header("��̏㉺�̑��x"), Range( 0.001f, 0.02f)] float updown_speed;
    [SerializeField, Header("true = �E��@false = ����")] bool hand_check;

    //�Q�[���I�u�W�F�N�g�̎擾
    [SerializeField, Header("�J����"), Header("�Q�[���I�u�W�F�N�g�̎擾")] GameObject camera;
    [SerializeField, Header("��Ɏ��u���b�N")] GameObject catch_block;
    [SerializeField, Header("camera_all")] GameObject camera_all;

    //�A�j���[�V�����擾
    [SerializeField, Header("hand_pos")] Animator up_anim;

    private Vector3[] move_ani_pos_right = new Vector3[10]; //�ړ����̉E��̃A�j���[�V����
    private Vector3[] move_ani_pos_left = new Vector3[10];  //�ړ����̍���̃A�j���[�V����
    private Vector3[] grab_ani_pos_right = new Vector3[10]; //�͂񂾎��̉E��̃A�j���[�V����
    private Vector3[] grab_ani_pos_left = new Vector3[10];  //�͂񂾎��̍���̃A�j���[�V����
    private int[] ani_count = new int[2];                   //���݂̃A�j���[�V�����ԍ�
    private bool[] ani_check = new bool[2];                 //�Đ������t�Đ�����
    private int frame = 0;                                  //�J�n������̃t���[��
    private Vector3 now_pos;                                //���݂̍��W
    private float move_amount_y = 0.0f;                     //�s�����N�����Ǝ肪�o�Ă���ړ���
    private bool move_check = false;                        //��l�����ړ��������擾����悤
    private bool grab_check = false;                        //��l�������������Ă锻��擾
    private Animator wolk_anim;                             //��𓮂����A�j���[�V����
    private GameObject grab_block = null;                   //���������̏��
    private Vector3 grab_size;                              //�������I�u�W�F�N�g�̃T�C�Y�L��

    // Start is called before the first frame update
    void Start()
    {
        now_pos = this.gameObject.transform.localPosition;

        //�A�j���[�V����
        wolk_anim = gameObject.GetComponent<Animator>();

        //��̃A�j���[�V�������W����
        for (int i = 0; i < 10; i++) 
        {
            //�ړ����̉E��̃A�j���[�V����
            move_ani_pos_right[i].x = 0.5f + (0.01f * i);
            move_ani_pos_right[i].y = 0.7f - (0.005f * i);
            move_ani_pos_right[i].z = 0.5f - (0.01f * i);
            //�ړ����̍���̃A�j���[�V����
            move_ani_pos_left[i].x = -0.5f - (0.01f * i);
            move_ani_pos_left[i].y = 0.7f - (0.005f * i);
            move_ani_pos_left[i].z = 0.5f - (0.01f * i);

            //�͂񂾎��̉E��̃A�j���[�V����
            grab_ani_pos_right[i].x = 0.5f + (0.01f * i);
            grab_ani_pos_right[i].y = 0.7f - (0.005f * i);
            grab_ani_pos_right[i].z = 0.5f - (0.01f * i);
            //�͂񂾎��̍���̃A�j���[�V����
            grab_ani_pos_left[i].x = -0.5f - (0.01f * i);
            grab_ani_pos_left[i].y = 0.7f - (0.005f * i);
            grab_ani_pos_left[i].z = 0.5f - (0.01f * i);
        }
        

        //�z��̏�����
        ani_count[0] = 4;
        ani_count[1] = 5;
        ani_check[0] = true;
        ani_check[1] = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�~�������̍X�V
        move_check = transform.root.gameObject.GetComponent<player>().Move_check;
        grab_check = camera.GetComponent<BoxCastRayTest>().grab;
        grab_block = camera.GetComponent<BoxCastRayTest>().Target;
        if (grab_block != null)
        {
            //grab_block.transform.parent = camera_all.gameObject.transform;
            //grab_block.transform.localPosition = new Vector3(0.5f, -0.25f, 1.0f);
        }


        //�ړ����̎�̓���--------------------------------------------------------
        if (move_check)
        {
            //�E��ƍ��肻�ꂼ��̓���
            wolk_anim.SetBool("move_ani_start", true);

            if (!grab_check)
                up_anim.SetBool("hand_move", true);
            else
                up_anim.SetBool("hand_move", false);

        }
        else
        {
            wolk_anim.SetBool("move_ani_start", false);

            up_anim.SetBool("hand_move", false);

            
        }
        //------------------------------------------------------------------------
        if(grab_check)
        {
            wolk_anim.SetBool("catch", true);
            //catch_block.SetActive(true);
        }
        else
        {
            wolk_anim.SetBool("catch", false);
            //catch_block.SetActive(false);
        }


        //���𓮂����Ƃ��̎�̓���------------------------------------------------
        //if (move_check)
        //{
        //    //�E��ƍ��肻�ꂼ��̓���
        //    if (hand_check)
        //        hand_move_right(now_pos.y);
        //    else
        //        hand_move_left(now_pos.y);

        //    now_pos = this.gameObject.transform.localPosition;

        //    move_amount_y += updown_speed;
        //    if (move_amount_y <= 0.2f)
        //        now_pos.y += updown_speed;
        //    else
        //        move_amount_y = 0.2f;
        //}
        //else
        //{
        //    move_amount_y -= updown_speed;
        //    if (move_amount_y >= 0)
        //        now_pos.y -= updown_speed;
        //    else
        //        move_amount_y = 0;
        //}
        //------------------------------------------------------------------------


        //���݂̌v�Z��̍��W���
        this.gameObject.transform.localPosition = now_pos;

        //�t���[���̉��Z
        frame++;
    }

    //�ړ����̉E��̓���
    private void hand_move_right(float y)
    {
        //���W�̍X�V
        move_ani_pos_right[ani_count[0]].y = y;
        this.gameObject.transform.localPosition = move_ani_pos_right[ani_count[0]];

        //0�`9�Ԃ̃A�j���[�V�������s���������邽�߂̏���
        if (ani_count[0] == 9)
            ani_check[0] = false;
        else if (ani_count[0] == 0)
            ani_check[0] = true;

        //�w�肵���t���[�����ɃA�j���[�V��������i�߂�
        if (frame % ani_speed == 0)
        {
            if (ani_check[0])
                ani_count[0]++;
            else
                ani_count[0]--;
        }
    }

    //�ړ����̍���̓���
    private void hand_move_left(float y)
    {
        //���W�̍X�V
        move_ani_pos_left[ani_count[1]].y = y;
        this.gameObject.transform.localPosition = move_ani_pos_left[ani_count[1]];

        //0�`9�Ԃ̃A�j���[�V�������s���������邽�߂̏���
        if (ani_count[1] == 9)
            ani_check[1] = false;
        else if (ani_count[1] == 0)
            ani_check[1] = true;

        //�w�肵���t���[�����ɃA�j���[�V��������i�߂�
        if (frame % ani_speed == 0)
        {
            if (ani_check[1])
                ani_count[1]++;
            else
                ani_count[1]--;
        }
    }


    //�͂�ł���Ƃ��̉E��̓���
    private void hand_grab_left(float y)
    {
        //���W�̍X�V
        move_ani_pos_left[ani_count[1]].y = y;


    }


}

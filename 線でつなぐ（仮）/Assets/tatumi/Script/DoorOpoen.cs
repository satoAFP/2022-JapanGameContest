using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpoen : MonoBehaviour
{
    //�M�~�b�N�m�N���A�󋵊Ǘ��pflag
    [SerializeField, Header("���݂̒ʓd��")]
    public bool[] ClearTaskflag;

    [SerializeField, Header("��Ԑ�̃X�e�[�W�ԍ�")] 
    public int stage_num;

    [SerializeField, Header("�h�A�̃C���X�g")]
    public GameObject[] door_illustration;

    //�M�~�b�N�̏ڍה��ʁiflag���,num=true,false���������ĂȂ����j
    private bool taskflag = false;
    private int num;

    [SerializeField, Header("���[�v�Q�[�g�Ƃ��Ďg����(true)")] public bool warp_door;
    [SerializeField, Header("�S�[�����̃��[�v�Q�[�g�Ƃ��Ďg����(true)")] public bool goal_warp_door;
    [SerializeField, Header("�h�A�̃A�j���[�V����")] public Animator door;
    [SerializeField, Header("�h�A�m�u�̃A�j���[�V����")] public Animator doorknob;
    [SerializeField, Header("�V�[���ړ��p�̃p�l���̏o��")] public GameObject scene_move_panel;

    private bool[] stage_clear_check = new bool[6];
    private bool SoundClear = false;

    public Material[] mat = new Material[1];//�ύX�������}�e���A�����Z�b�g
    [SerializeField, Header("0=����,1=����")]
    Material[] mats;

    //�����擾
    [SerializeField]
    private AudioClip soundopen,soundclose,soundlock;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
        audioSource = GetComponent<AudioSource>();
        
        //�X�e�[�W�̃N���A�󋵎擾
        stage_clear_check = GameObject.Find("stage_clear_check").GetComponent<stage_clear>().Stage_clear;

        //�o����X�e�[�W�̔��̃C���X�g���点��
        if (stage_clear_check[stage_num])
        {
            for (int i = 0; i < door_illustration.Length; i++)
            {
                door_illustration[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�S�v�f��v�����f
        IEnumerable<bool> results = (IEnumerable<bool>)ClearTaskflag.Distinct();
       
        //�z���true,false���������Ă邩�����itrue or false �݂̂Ȃ�1,�ǂ���������Ȃ�2�j
        num = results.Count();

        //bool�^�Ŕ���ł���悤�ɕϊ�
        foreach (bool Check in results)
        {
            //����i���g���ʁj
            taskflag = Check;

            //�J�M�߁A�J��
            if(taskflag!=Check)
            {
                audioSource.PlayOneShot(soundlock);
            }
        }
       
        mats[0] = mat[1];
        //ray�������ĂȂ��Ȃ猳�ɖ߂�
        GetComponent<Renderer>().materials = mats;
    }

    public void RayOpenDoor()
    {
        if (!warp_door)
        {
            //�h�A���J���邩�ǂ������ăA�j���[�V�������Đ�
            if (num == 1 && taskflag == true)
            {
                door.SetBool("open", true);
                doorknob.SetBool("open", true);

                //��x�̂�
                if (SoundClear == false)
                audioSource.PlayOneShot(soundopen);
                SoundClear = true;

                //�Ō�̕����ŃS�[�������[�v����
                if (goal_warp_door)
                {
                    door.SetBool("open", true);
                    doorknob.SetBool("open", true);
                    scene_move_panel.SetActive(true);
                    
                }
            }
            else
            {
                door.SetBool("open", false);
                doorknob.SetBool("open", false);
                audioSource.PlayOneShot(soundclose);
            }

            
        }
    }

    public void RayOpenWarpDoor()
    {
        if (warp_door)
        {
            if (stage_clear_check[stage_num])
            {
                //�X�e�[�W���邽�߂̔��J������
                door.SetBool("open", true);
                doorknob.SetBool("open", true);
                scene_move_panel.SetActive(true);
                if (!SoundClear)
                    audioSource.PlayOneShot(soundopen);
                SoundClear = true;

            }
        }
    }

    public void RayTargetDoor()
    {
        //�}�e���A���ύX(1=����0=����)

        mats[0] = mat[0];
        //�����I
        GetComponent<Renderer>().materials = mats;

    }
}

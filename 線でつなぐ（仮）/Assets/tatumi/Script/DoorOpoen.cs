using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpoen : MonoBehaviour
{
    public bool[] ClearTaskflag;
    private bool taskflag = false;
    private int num,now_material=0;

    [SerializeField, Header("���[�v�Q�[�g�Ƃ��Ďg����(true)�A�X�e�[�W�̃h�A�Ƃ��Ďg����(false)")] public bool warp_door;
    [SerializeField, Header("�h�A�̃A�j���[�V����")] public Animator door;
    [SerializeField, Header("�h�A�m�u�̃A�j���[�V����")] public Animator doorknob;

    public Material[] mat = new Material[1];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;


    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        //�S�v�f��v�����f
        IEnumerable<bool> results = (IEnumerable<bool>)ClearTaskflag.Distinct();
       
        //num = 1 : 
        num = results.Count();

        //bool�^�Ŕ���ł���悤�ɕϊ�
        foreach (bool Check in results)

        taskflag = Check;

        mats[0] = mat[1];

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
            }
            else
            {
                door.SetBool("open", false);
                doorknob.SetBool("open", false);
            }
        }
    }

    public void RayOpenWarpDoor()
    {
        if (warp_door)
        {
            //�X�e�[�W���邽�߂̔��J������
            door.SetBool("open", true);
            doorknob.SetBool("open", true);
        }
    }

    public void RayTargetDoor()
    {
        //�}�e���A���ύX(1=����0=����)

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

        Debug.Log("k");

    }
}

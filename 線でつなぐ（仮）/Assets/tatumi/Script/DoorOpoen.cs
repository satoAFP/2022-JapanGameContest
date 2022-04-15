using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpoen : MonoBehaviour
{
    public bool[] ClearTaskflag;
    private bool taskflag = false;
    private int num,now_material=0;

    
    public Animator anim;

    public Material[] mat = new Material[2];//�ύX�������}�e���A�����Z�b�g
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
       
        num = results.Count();

        //bool�^�Ŕ���ł���悤�ɕϊ�
        foreach (bool Check in results)

        taskflag = Check;

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

    }

    public void RayOpenDoor()
    {
        //�h�A���J���邩�ǂ������ăA�j���[�V�������Đ�
        if (num == 1 && taskflag == true)
            anim.SetBool("Open", true);
        else
            anim.SetBool("Open", false);
    }

    public void RayTargetDoor()
    {
        //�}�e���A���ύX(1=����0=����)

        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;
      
    }
}

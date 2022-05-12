using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    public Material[] mat = new Material[2];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    [SerializeField]
    private bool Rotationflag,vertical;

    [SerializeField]
    public bool Nosetline = false;//�V�����_�[�̏�ɒu���Ȃ��u���b�N


    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        if(mat[0]!=mats[0])
        {
            //���C���������ĂȂ��Ƃ��͏����F�ɂ�������
            mats[0] = mat[0];

            GetComponent<Renderer>().materials = mats;
        }

        if (Rotationflag == true)
        {
            if (vertical == true)
            {
                if (Mathf.Round(this.transform.localEulerAngles.y) == 0 || this.transform.localEulerAngles.y == 180.0f)
                    this.GetComponent<Conductor_Script>().SetEnergization(true);
                else
                    this.GetComponent<Conductor_Script>().SetEnergization(false);
            }
            else if (vertical == false)
            {
                if (Mathf.Round(this.transform.localEulerAngles.y) == 90.0f || this.transform.localEulerAngles.y == 270.0f)
                    this.GetComponent<Conductor_Script>().SetEnergization(true);
                else
                    this.GetComponent<Conductor_Script>().SetEnergization(false);
            }
        }

    }

    //�֐����iUpdate�͂��������߁j
    public void ChangeMaterial()
    {
        //�}�e���A���ύX(1=����0=����)
        
        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;
       
    }

   
}

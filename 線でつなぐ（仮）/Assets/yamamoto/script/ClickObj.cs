using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : Base_Enegization
{
    public Material[] mat = new Material[2];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    [SerializeField]
    private bool Rotationflag,vertical,inputON;

    [SerializeField]
    public bool Setlineblock = false;//�V�����_�[�̏�ɒu����u���b�N



    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        
         //���C���������ĂȂ��Ƃ��͏����F�ɂ�������
          mats[0] = mat[0];

         GetComponent<Renderer>().materials = mats;
        

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

        inputON = false;
    }

    //�֐����iUpdate�͂��������߁j
    public void ChangeMaterial()
    {
        //�}�e���A���ύX(1=����0=����)
        
        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;
        inputON = true;
       
    }

    public void SetColor(Color32 a)
    {
        for(int i=0;i!=2;i++)
        {
            mat[i].color = a;
            if(i==1)
            {
                if (inputON == true)
                {
                    //�A�E�g���C���Ή�
                    mat[i].SetColor("_MainColor", a);
                    Debug.Log(a);
                }
            }
        }

       
    }

   
}

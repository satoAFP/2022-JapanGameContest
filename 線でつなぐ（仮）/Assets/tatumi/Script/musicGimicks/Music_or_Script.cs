using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_or_Script : Base_Enegization
{

    //music�ɂ͍������Ȃ����ߕs�v
    //private GameObject child;
    //[SerializeField]
    //private List<GameObject> obj_list = new List<GameObject>();
    [SerializeField]
    private int music_num=-1;

    private GameObject ResetObj;
    
    //�E�F����(�����ȉ���)
    //public void Decolorization(int decolor,GameObject gameObject)
    //{
    //    Debug.Log(decolor);
    //    //�q�I�u�W�F�N�g�ɉ��ԍ���n���w�߂��o��
    //    child.GetComponent<Music_or_Child_Script>().SetColCulation(music_num);

    //    //������͂Ȃ��݂���

    //}

    // Start is called before the first frame update
    void Start()
    {
        //�q�I�u�W�F�N�g���擾
       // child = transform.GetChild(0).gameObject;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (energization == true)
            GetComponent<Renderer>().material.color = new Color32(71, 214, 255, 1);
        else
            GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 1);
    }

    public void OnCollisionStay(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A���̐F�ɕύX
        if (collision.gameObject.tag == "MusicOutput")
        {
            //ColorOutoput��energization��true�Ȃ炱���ɓ���
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                energization = true;
            }
            else
            {
                music_num = -1;
                energization = false;
            }
        }
        else if(music_num!=-1&&collision.gameObject.tag == "Power_Supply")
        {
            ResetObj = collision.gameObject;
            collision.gameObject.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A�ݒ肳�ꂽ�l�����̃I�u�W�F�N�g�̕ϐ�����������ƂŒE�F
        if (collision.gameObject.tag == "MusicOutput")
        {

            music_num = -1;
            energization = false;
            ResetObj.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
            //�q�I�u�W�F�N�g�ɐF�������w�߂��o��(�q���͂�(ry)
            // child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }

   
}

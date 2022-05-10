using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Music_or_Script : Base_Enegization
{

    //music�p�����ϐ��g
    [SerializeField]
    private GameObject[] musics=new GameObject[10];
    [SerializeField]
    private string[] musics_name=new string[10];
    [SerializeField]
    private int music_num=-1;

    private GameObject ResetObj;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string OutColor_name;

    // Start is called before the first frame update
    void Start()
    {
        //�q�I�u�W�F�N�g���擾
        // child = transform.GetChild(0).gameObject;

        OutColor_name = "OutM&C";

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
        //���O��v�m�F
        string result = musics_name.SingleOrDefault(value => value == collision.gameObject.name);

        //�������O�̕�������������
        if (result == null)
        {
            //���A�C�e����T���ĂԂ�����
            for (int i = 0; i != 10; i++)
            {
                if (musics[i] == null)
                {
                    //�V�K�Ȃ����
                    musics[i] = collision.gameObject;
                    musics_name[i] = collision.gameObject.name;
                    break;
                }
            }
        }

        //�L�^�̗L���Ɍ��炸���X�V
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
            else if (collision.gameObject.name.Contains(OutColor_name) == true)
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
            else if (music_num != -1 && collision.gameObject.tag == "Power_Supply")
            {
                ResetObj = collision.gameObject;
                collision.gameObject.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
            }

        //�����m�F(2�ȏ�Ȃ�������ꉻ�m�F)
        //�S�v�f��v�����f
        IEnumerable<string> results = (IEnumerable<string>)musics_name.Distinct();

        //��ޕʂɉ���ނ��邩
        int num = results.Count();

        if(num>1)
        {
            //2��ވȏ�̉��y��F�������ꍇ������
            music_num = -1;
        }

        /*��������----------------------------------------------------------
        int nums[10];

        //2��ވȏ�̏ꍇ
        if(num>1)
        {
            int k=0;

            for(int i=0;i!=10i++)
            {
                if (musics != null)
                {
                    nums[k] = musics[i].GetComponent<OutputMusic_Script>().Remusic_num();
                    k++;
                }
            }
        }

        //nums�ɒ��o�������������ɑ΂��g�p-------------------------------------*/
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
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            music_num = -1;
            energization = false;
            ResetObj.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
        }

        //�L�^�𔲂�
        for(int i=0;i!=10;i++)
        {
            //����̏ꍇ
            if(musics_name[i]== collision.gameObject.name)
            {
                musics_name[i] = null;
                musics[i] = null;
            }
        }
    }

   
}

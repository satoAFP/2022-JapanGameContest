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
    private int[] musics_nums=new int[10];
    [SerializeField]
    private string[] musics_name = new string[10];
    [SerializeField]
    private int music_num=-1,Powernum=-1;

    private GameObject ResetObj;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string OutColor_name;

    private bool MCmode;

    // Start is called before the first frame update
    void Start()
    {
        //�q�I�u�W�F�N�g���擾
        // child = transform.GetChild(0).gameObject;

        OutColor_name = "OutM&C";

        for(int i=0;i!=10;i++)
        {
            musics_nums[i] = -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (MCmode == false)
        {
            if (energization == true)
                GetComponent<Renderer>().material.color = new Color32(71, 214, 255, 1);
            else
                GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 1);
        }
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
                    if(collision.gameObject.tag == "Power_Supply")
                    {
                        Powernum = i;
                    }
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
               // music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                //energization = true;
                MCmode = false;
            }
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //ColorOutoput��energization��true�Ȃ炱���ɓ���
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                //music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                //energization = true;
                MCmode = true;
            }
            else
            {
                music_num = -1;
                //energization = false;
                MCmode = true;
            }
        }
       
        //�����m�F(2�ȏ�Ȃ�������ꉻ�m�F)
        //���g�X�V
        for(int i=0;i!=10;i++)
        {
            if (musics[i] != null)
            {
                if (musics_name[i].Contains(OutColor_name) == true || musics[i].gameObject.tag == "MusicOutput")
                {
                    musics_nums[i] = musics[i].gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                    if (musics_nums[i] != -1)
                        music_num = musics_nums[i];
                }
            }
            else
                break;
        }

        //�S�v�f��v�����f
        IEnumerable<int> results = (IEnumerable<int>)musics_nums.Distinct();

        //��ޕʂɉ���ނ��邩
        int num = results.Count();

        if(num>2)
        {
            //2��ވȏ�̉��y��F�������ꍇ������
            music_num = -1;
            Debug.Log("2�퍬�����Ă��");
        }
        else if(num==1)
        {
            //�Ȃʂ��Ȃ��i�o�O����j
        }
        else
        {
            //ResetObj = musics[Powernum];
            musics[Powernum].GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
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
            MCmode = false;
            //�q�I�u�W�F�N�g�ɐF�������w�߂��o��(�q���͂�(ry)
            // child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            music_num = -1;
            energization = false;
            ResetObj.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
            MCmode = true;
        }

        //�L�^�𔲂�
        for(int i=0;i!=10;i++)
        {
            //����̏ꍇ
            if(musics_name[i]== collision.gameObject.name)
            {
                musics_name[i] = null;
                musics[i] = null;
                musics_nums[i] = -1;
                if (collision.gameObject.tag == "Power_Supply")
                {
                    Powernum = -1;
                }
            }
        }
    }

   
}

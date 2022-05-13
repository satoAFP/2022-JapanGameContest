using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Music_or_Script : Base_Enegization
{

    //music用合成変数枠
    [SerializeField]
    private GameObject[] musics=new GameObject[10];
    [SerializeField]
    private int[] musics_nums=new int[10];
    [SerializeField]
    private string[] musics_name = new string[10];
    [SerializeField]
    private int music_num=-1,Powernum=-1;

    private GameObject ResetObj;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string OutColor_name;

    private bool MCmode;

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトを取得
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
        //名前一致確認
        string result = musics_name.SingleOrDefault(value => value == collision.gameObject.name);

        //同じ名前の物が無かった時
        if (result == null)
        {
            //穴アイテルやつ探してぶっこむ
            for (int i = 0; i != 10; i++)
            {
                if (musics[i] == null)
                {
                    //新規なら入力
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
       

        //記録の有無に限らず情報更新
        //タグColorOutputオブジェクトから色を取得し、その色に変更
        if (collision.gameObject.tag == "MusicOutput")
        {
            //ColorOutoputのenergizationがtrueならここに入る
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
               // music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                //energization = true;
                MCmode = false;
            }
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //ColorOutoputのenergizationがtrueならここに入る
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
       
        //長さ確認(2以上なら内部同一化確認)
        //中身更新
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

        //全要素一致か判断
        IEnumerable<int> results = (IEnumerable<int>)musics_nums.Distinct();

        //種類別に何種類あるか
        int num = results.Count();

        if(num>2)
        {
            //2種類以上の音楽を認識した場合初期化
            music_num = -1;
            Debug.Log("2種混ざってるよ");
        }
        else if(num==1)
        {
            //なぬもない（バグ回避）
        }
        else
        {
            //ResetObj = musics[Powernum];
            musics[Powernum].GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
        }

        /*合成処理----------------------------------------------------------
        int nums[10];

        //2種類以上の場合
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

        //numsに抽出完了合成処理に対し使用-------------------------------------*/
    }

    public void OnCollisionExit(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、設定された値をこのオブジェクトの変数から引くことで脱色
        if (collision.gameObject.tag == "MusicOutput")
        {
            music_num = -1;
            energization = false;
            ResetObj.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
            MCmode = false;
            //子オブジェクトに色を消す指令を出す(子供はつか(ry)
            // child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            music_num = -1;
            energization = false;
            ResetObj.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
            MCmode = true;
        }

        //記録を抜く
        for(int i=0;i!=10;i++)
        {
            //同一の場合
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

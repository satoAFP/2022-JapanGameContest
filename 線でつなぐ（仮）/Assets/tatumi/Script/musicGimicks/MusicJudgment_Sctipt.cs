using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJudgment_Sctipt : Base_Enegization
{
    //ゲームクリアとなる音番号,現在の番号
    [SerializeField, Header("クリアSE番号と現在のSE番号")]
    private int clearMusic,muisc_num;

    //消える対象（ぴかぴか処理用）
    [SerializeField, Header("光る対象指定")]
    private GameObject[] objs;

    //色も同居するか
    [SerializeField, Header("色同居確認flag")]
    private bool MCmode;

    // Update is called once per frame
    void Update()
    {
        //同居しなければ直接いじる
        if (MCmode == false)
        {
            objs[0].SetActive(true);
            objs[1].SetActive(false);

            if (clearMusic == muisc_num)
            {
                //ここにクリアの証的なコード
                objs[0].SetActive(false);
                objs[1].SetActive(true);
            }
        }
        else
        {
            //同居してればenegeからいじる
            if (clearMusic == muisc_num)
            {
                //ここにクリアの証的なコード
                energization = true;
            }
            else
                energization = false;
        }
    }

    public void now_music(int a)
    {
        //音楽番号受け取り
        muisc_num = a;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MC_Judgment : MonoBehaviour
{
    //二種のスクリプト取得
    MusicJudgment_Sctipt Music_script;
    ColorJudgment_Sctipt Color_script;

    //二つのenegeの状態を取得
    [SerializeField]
    private bool[] clearflag = new bool[2];

    //Obj自体の発行を表示・非表示で再現
    [SerializeField]
    private GameObject[] objs;

    //2種類のenegeの通電状態確認用
    [SerializeField, Header("通電確認bool")]
    public bool OK;

    // Start is called before the first frame update
    void Start()
    {
        //二つのscript取得
        Music_script = this.gameObject.GetComponent<MusicJudgment_Sctipt>();
        Color_script = this.gameObject.GetComponent<ColorJudgment_Sctipt>();
    }

    // Update is called once per frame
    void Update()
    {
        //フラグ更新
        clearflag[0]=Music_script.GetEnergization();
        clearflag[1]=Color_script.GetEnergization();

        //全要素一致か判断
        IEnumerable<bool> results = (IEnumerable<bool>)clearflag.Distinct();

        //true,falseどちらか一種類かどうか
        int num = results.Count();

        //bool型で判定できるように変換
        foreach (bool Check in results)
        {
            //一種類のタイプを判定
            OK = Check;
          
            //片方通電ならOFF
            if(num==2)
            {
                objs[0].SetActive(true);
                objs[1].SetActive(false);
            }
            //両方一致
            else if (num == 1)
            {
               //しかし通電してないからOFF
                if (OK == false)
                {
                    objs[0].SetActive(true);
                    objs[1].SetActive(false);
                   
                }
                //ON!!!
                else
                {
                    //ここにクリアの証的なコード
                    objs[0].SetActive(false);
                    objs[1].SetActive(true);
                }
            }
        }
    }
}
